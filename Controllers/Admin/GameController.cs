using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Security;
using System.Security.Permissions;
using GameHeavenAPI.Repositories.CPUs;
using GameHeavenAPI.Repositories.GPUs;
using GameHeavenAPI.Repositories.DirectX;
using GameHeavenAPI.Repositories.Statuses;
using GameHeavenAPI.Repositories.Oses;
using GameHeavenAPI.Repositories.Franchises;
using GameHeavenAPI.Repositories.Genres;
using GameHeavenAPI.Repositories.Platforms;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Web;
using Newtonsoft.Json;

namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPublishersRepository _publishersRepository;
        private readonly IDeveloperRepository _developerRepository;
        private readonly ICPURepository _cpuRepository;
        private readonly IGPURepository _gpuRepository;
        private readonly IDirectXRepository _directXRepository;
        private readonly IStatusRepository _statusRepository;
        private readonly IOsRepository _osRepository;
        private readonly IFranchiseRepository _franchiseRepository;
        private readonly IGenreRepository _genreRepository;
        private readonly IPlatformRepository _platformRepository;

        public GameController(IGameRepository gameRepository,
                              IPublishersRepository publishersRepository,
                              IDeveloperRepository developerRepository,
                              ICPURepository cpuRepository,
                              IGPURepository gpuRepository,
                              IDirectXRepository directXRepository,
                              IStatusRepository statusRepository,
                              IOsRepository osRepository,
                              IFranchiseRepository franchiseRepository,
                              IGenreRepository genreRepository,
                              IPlatformRepository platformRepository
            )
        {
            _gameRepository = gameRepository;
            _publishersRepository = publishersRepository;
            _developerRepository = developerRepository;
            _cpuRepository = cpuRepository;
            _gpuRepository = gpuRepository;
            _directXRepository = directXRepository;
            _statusRepository = statusRepository;
            _osRepository = osRepository;
            _franchiseRepository = franchiseRepository;
            _genreRepository = genreRepository;
            _platformRepository = platformRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IEnumerable<GameDto>> GetGamesAsync()
        {
            return (await _gameRepository.GetGamesAsync()).Select(game => game.AsDto()).ToList();
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<GameDto>> GetGameAsync(int id)
        {
            var foundGame = await _gameRepository.GetGameByIdAsync(id);
            if (foundGame is null)
            {
                return NotFound();
            }
            return foundGame.AsDto();
        }
        [HttpPost]
        [Consumes("multipart/form-data")]
        [DisableRequestSizeLimit]
        [Authorize(Roles = nameof(Roles.User))]
        public async Task<ActionResult<GameDto>> CreateGameAsync([FromForm] CreateGameDto createGameDto)
        {

            string coverDirectoryPath = $"Uploads/Games/{createGameDto.Name}/Cover";
            var filePath = $"{coverDirectoryPath}/{createGameDto.Cover.FileName}";
            var coverPathencoded = HttpUtility.UrlEncode(filePath);
            var coverLink = $"https://localhost:5001/GetImage/{coverPathencoded}";

            if (!Directory.Exists(coverDirectoryPath))
            {
                Directory.CreateDirectory(coverDirectoryPath);
                using var stream = new FileStream(Path.Combine(coverDirectoryPath, createGameDto.Cover.FileName), FileMode.Create);
                await createGameDto.Cover.CopyToAsync(stream);
            }
            Game createdGame = new()
            {
                Name = createGameDto.Name,
                Description = createGameDto.Description,
                Price = createGameDto.Price,
                Developers = createGameDto.DeveloperIds != null ? (await _developerRepository.GetDevelopersAsync()).Join(createGameDto.DeveloperIds,
                                                                             developer => developer.Id,
                                                                             developerId => developerId,
                                                                             (developer, developerId) => developer).ToList() : null,
                Publisher = await _publishersRepository.GetPublisherAsync(createGameDto.PublisherId),
                MinimumSystemRequirements = createGameDto.MinimumSystemRequirements != null ? new MinimumSystemRequirements
                {
                    GPU = await _gpuRepository.GetGPUByIdAsync(createGameDto.MinimumSystemRequirements.GPUId),
                    CPU = await _cpuRepository.GetCPUByIdAsync(createGameDto.MinimumSystemRequirements.CPUId),
                    DirectX = await _directXRepository.GetDirectXByIdAsync(createGameDto.MinimumSystemRequirements.DirectXId),
                    Os = await _osRepository.GetOsByIdAsync(createGameDto.MinimumSystemRequirements.OsId),
                    Ram = createGameDto.MinimumSystemRequirements.Ram,
                    Storage = createGameDto.MinimumSystemRequirements.Storage,
                    AdditionalNotes = createGameDto.MinimumSystemRequirements.AdditionalNotes,
                } : null,
                RecommendedSystemRequirements = createGameDto.RecommendedSystemRequirements != null ? new RecommendedSystemRequirements
                {
                    GPU = await _gpuRepository.GetGPUByIdAsync(createGameDto.RecommendedSystemRequirements.GPUId),
                    CPU = await _cpuRepository.GetCPUByIdAsync(createGameDto.RecommendedSystemRequirements.CPUId),
                    DirectX = await _directXRepository.GetDirectXByIdAsync(createGameDto.RecommendedSystemRequirements.DirectXId),
                    Os = await _osRepository.GetOsByIdAsync(createGameDto.RecommendedSystemRequirements.OsId),
                    Ram = createGameDto.RecommendedSystemRequirements.Ram,
                    Storage = createGameDto.RecommendedSystemRequirements.Storage,
                    AdditionalNotes = createGameDto.RecommendedSystemRequirements.AdditionalNotes,
                } : null,
                Status = await _statusRepository.GetStatusByIdAsync(createGameDto.StatusId),
                Franchise = createGameDto.FranchiseId != null ? await _franchiseRepository.GetFranchiseByIdAsync((int)createGameDto.FranchiseId) : null,
                ReleaseDate = (createGameDto.ReleaseDate != null ? (System.DateTime)createGameDto.ReleaseDate : null),
                Approved = false,
                Discount = 0,
                CoverPath = coverLink,
                Platforms = (await _platformRepository.GetPlatformsAsync()).Join(createGameDto.PlatformIds, platform => platform.Id, receivedPlatformId => receivedPlatformId, (platform, receivedPlatformId) => platform).ToList(),
                Genres = (await _genreRepository.GetGenresAsync()).Join(createGameDto.GenresIds, genre => genre.Id, receivedGenreId => receivedGenreId, (genre, receivedGenreId) => genre).ToList(),
            };

            List<string> imagesPaths = new();
            string videosPath = $"Uploads/Games/{createGameDto.Name}/Videos";

            List<string> videosPaths = new();
            string imagesPath = $"Uploads/Games/{createGameDto.Name}/Images";

            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
                foreach (var image in createGameDto.Images)
                {
                    var imagePath = Path.Combine(imagesPath, image.FileName);
                    var imagePathEncoded = HttpUtility.UrlEncode(imagePath);
                    var imageLink = $"https://localhost:5001/GetImage/{imagePathEncoded}";
                    imagesPaths.Add(imageLink);
                    using var stream = new FileStream(imagePath, FileMode.Create);
                    await image.CopyToAsync(stream);
                }

            }
            if (!Directory.Exists(videosPath))
            {
                Directory.CreateDirectory(videosPath);
                foreach (var video in createGameDto.Videos)
                {
                    var videoPath = Path.Combine(videosPath, video.FileName);
                    var videoPathEncoded = HttpUtility.UrlEncode(videoPath);
                    var videoLink = $"https://localhost:5001/GetVideo/{videoPathEncoded}";
                    videosPaths.Add(videoLink);
                    using var stream = new FileStream(videoPath, FileMode.Create);
                    await video.CopyToAsync(stream);
                }
            }
            createdGame.VideosPath = JsonConvert.SerializeObject(videosPaths);
            createdGame.ImagesPath = JsonConvert.SerializeObject(imagesPaths);
            var insertedGame = await _gameRepository.CreateGameAsync(createdGame);
            return Ok(new Response
            {
                Success = true,
                Messages = new List<string>()
                {
                    "Game created successfully",
                }
            });

        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = nameof(Roles.User))]
        public async Task<ActionResult> UpdateGameAsync(int id, [FromForm] UpdateGameDto updateGameDto)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            if (updateGameDto.Images is not null)
            {
                var path = $"Uploads/Games/{requestedGame.Name}/Images";

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                List<string> imagesPaths = new();
                string imagesPath = $"Uploads/Games/{requestedGame.Name}/Images";
                foreach (var image in updateGameDto.Images)
                {
                    using var stream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create);
                    await image.CopyToAsync(stream);
                    var imagePath = Path.Combine(imagesPath, image.FileName);
                    var imagePathEncoded = HttpUtility.UrlEncode(imagePath);
                    var imageLink = $"https://localhost:5001/GetImage/{imagePathEncoded}";
                    imagesPaths.Add(imageLink);
                }
                requestedGame.ImagesPath = JsonConvert.SerializeObject(imagesPaths);
            }
            if (updateGameDto.Videos is not null)
            {
                var path = $"Uploads/Games/{requestedGame.Name}/Videos";

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                List<string> videosPaths = new();
                string videosPath = $"Uploads/Games/{requestedGame.Name}/Videos";
                Directory.CreateDirectory(path);
                foreach (var video in updateGameDto.Videos)
                {
                    var videoPath = Path.Combine(videosPath, video.FileName);
                    var videoPathEncoded = HttpUtility.UrlEncode(videoPath);
                    var videoLink = $"https://localhost:5001/GetVideo/{videoPathEncoded}";
                    videosPaths.Add(videoLink);
                    using var stream = new FileStream(Path.Combine(path, video.FileName), FileMode.Create);
                    await video.CopyToAsync(stream);
                }
                requestedGame.VideosPath = JsonConvert.SerializeObject(videosPaths);
            }
            if (updateGameDto.Cover is not null)
            {
                var path = $"Uploads/Games/{requestedGame.Name}/Cover";

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                string coverDirectoryPath = $"Uploads/Games/{requestedGame.Name}/Cover";
                var filePath = $"{coverDirectoryPath}/{updateGameDto.Cover.FileName}";
                var coverPathencoded = HttpUtility.UrlEncode(filePath);
                var coverLink = $"https://localhost:5001/GetImage/{coverPathencoded}";
                using var stream = new FileStream(filePath, FileMode.Create);
                await updateGameDto.Cover.CopyToAsync(stream);
                requestedGame.CoverPath = coverLink;
            }


            requestedGame.Discount = updateGameDto.Discount != null ? (float)updateGameDto.Discount : requestedGame.Discount;
            requestedGame.Description = updateGameDto.Description ?? requestedGame.Description;
            requestedGame.Price = updateGameDto.Price != null ? (double)updateGameDto.Price : requestedGame.Price;
            if (updateGameDto.Name is not null)
            {
                if (!updateGameDto.Name.Equals(requestedGame.Name))
                {
                    Directory.Move($"Uploads/Games/{requestedGame.Name}", $"Uploads/Games/{updateGameDto.Name}");
                    requestedGame.Name = updateGameDto.Name;
                    List<string> videosPaths = new();
                    string videosPath = $"Uploads/Games/{updateGameDto.Name}/Videos";
                    var videoFiles = Directory.GetFiles(videosPath);
                    foreach (var videoFile in videoFiles)
                    {
                        var videoFileName = Path.GetFileName(videoFile);
                        var videoPath = Path.Combine(videosPath, videoFileName);
                        var encodedVideoPath = HttpUtility.UrlEncode(videoPath);
                        var videoLink = $"https://localhost:5001/GetVideo/{encodedVideoPath}";
                        videosPaths.Add(videoLink);
                    }
                    requestedGame.VideosPath = JsonConvert.SerializeObject(videosPaths);
                    List<string> imagesPaths = new();
                    string imagesPath = $"Uploads/Games/{updateGameDto.Name}/Images";
                    var imageFiles = Directory.GetFiles(imagesPath);
                    foreach (var imageFile in imageFiles)
                    {
                        var imageFileName = Path.GetFileName(imageFile);
                        var imagePath = Path.Combine(imagesPath, imageFileName);
                        var encodedImagePath = HttpUtility.UrlEncode(imagePath);
                        var imageLink = $"https://localhost:5001/GetImage/{encodedImagePath}";
                        imagesPaths.Add(imageLink);
                    }
                    requestedGame.ImagesPath = JsonConvert.SerializeObject(imagesPaths);

                    string coverDirectoryPath = $"Uploads/Games/{updateGameDto.Name}/Cover";
                    var coverFileName = Path.GetFileName(Directory.GetFiles(coverDirectoryPath).First());
                    var coverPath = Path.Combine(coverDirectoryPath, coverFileName);
                    var coverPathencoded = HttpUtility.UrlEncode(coverPath);
                    var coverLink = $"https://localhost:5001/GetImage/{coverPathencoded}";
                    requestedGame.CoverPath = coverLink;

                }
            }
            requestedGame.ReleaseDate = updateGameDto.ReleaseDate ?? requestedGame.ReleaseDate;
            requestedGame.Developers = updateGameDto.DeveloperIds != null ? (await _developerRepository.GetDevelopersAsync()).Join(updateGameDto.DeveloperIds,
                                                                         developer => developer.Id,
                                                                         developerId => developerId,
                                                                         (developer, developerId) => developer).ToList() : requestedGame.Developers;
            requestedGame.Publisher = updateGameDto.PublisherId != null ? await _publishersRepository.GetPublisherAsync((int)updateGameDto.PublisherId) : requestedGame.Publisher;
            requestedGame.MinimumSystemRequirements = updateGameDto.UpdatedMinimumSystemRequirements != null ? new MinimumSystemRequirements
            {
                GPU = await _gpuRepository.GetGPUByIdAsync(updateGameDto.UpdatedMinimumSystemRequirements.GPUId),
                CPU = await _cpuRepository.GetCPUByIdAsync(updateGameDto.UpdatedMinimumSystemRequirements.CPUId),
                DirectX = await _directXRepository.GetDirectXByIdAsync(updateGameDto.UpdatedMinimumSystemRequirements.DirectXId),
                Os = await _osRepository.GetOsByIdAsync(updateGameDto.UpdatedMinimumSystemRequirements.OsId),
                Ram = updateGameDto.UpdatedMinimumSystemRequirements.Ram,
                Storage = updateGameDto.UpdatedMinimumSystemRequirements.Storage,
                AdditionalNotes = updateGameDto.UpdatedMinimumSystemRequirements.AdditionalNotes,
            } : requestedGame.MinimumSystemRequirements;
            requestedGame.RecommendedSystemRequirements = updateGameDto.UpdatedRecommendedSystemRequirements != null ? new RecommendedSystemRequirements
            {
                GPU = await _gpuRepository.GetGPUByIdAsync(updateGameDto.UpdatedRecommendedSystemRequirements.GPUId),
                CPU = await _cpuRepository.GetCPUByIdAsync(updateGameDto.UpdatedRecommendedSystemRequirements.CPUId),
                DirectX = await _directXRepository.GetDirectXByIdAsync(updateGameDto.UpdatedRecommendedSystemRequirements.DirectXId),
                Os = await _osRepository.GetOsByIdAsync(updateGameDto.UpdatedRecommendedSystemRequirements.OsId),
                Ram = updateGameDto.UpdatedRecommendedSystemRequirements.Ram,
                Storage = updateGameDto.UpdatedRecommendedSystemRequirements.Storage,
                AdditionalNotes = updateGameDto.UpdatedRecommendedSystemRequirements.AdditionalNotes,
            } : requestedGame.RecommendedSystemRequirements;

            requestedGame.Status = updateGameDto.StatusId != null ? await _statusRepository.GetStatusByIdAsync((int)updateGameDto.StatusId) : requestedGame.Status;
            requestedGame.Franchise = updateGameDto.FranchiseId != null ? await _franchiseRepository.GetFranchiseByIdAsync((int)updateGameDto.FranchiseId) : requestedGame.Franchise;

            await _gameRepository.UpdateGameAsync(requestedGame);
            return Ok(new Response
            {
                Success = true,
                Messages = new List<string>()
                {
                    "Game updated successfully",
                }
            });
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameAsync(int id)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            var imagesPath = requestedGame.ImagesPath;
            var videosPath = requestedGame.VideosPath;
            var coverPath = requestedGame.CoverPath;
            if (Directory.Exists(imagesPath))
            {
                Directory.Delete(imagesPath, true);
            }
            if (Directory.Exists(coverPath))
            {
                Directory.Delete(coverPath, true);
            }
            if (Directory.Exists(videosPath))
            {
                Directory.Delete(videosPath, true);
            }
            await _gameRepository.DeleteGameAsync(id);
            return NoContent();
        }
    }
}
