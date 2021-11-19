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

namespace GameHeavenAPI.Controllers
{
    [Route("games")]
    [ApiController]
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
        public async Task<IEnumerable<GameDto>> GetGamesAsync()
        {
            return (await _gameRepository.GetGamesAsync()).Select(game => game.AsDto()).ToList();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGameAsync(int id)
        {
            var foundGame = await _gameRepository.GetGameByIdAsync(id);
            if (foundGame is null)
            {
                return NotFound();
            }
            return foundGame.AsDto();
        }
        [HttpPost("new")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<GameDto>> CreateGameAsync([FromForm] CreateGameDto createGameDto)
        {
            string coverPath = $"Uploads/{createGameDto.Name}/Cover";
            string imagesPath = $"Uploads/{createGameDto.Name}/Images";
            string videosPath = $"Uploads/{createGameDto.Name}/Videos";

            if (!Directory.Exists(coverPath))
            {
                Directory.CreateDirectory(coverPath);
                using var stream = new FileStream(Path.Combine(coverPath, createGameDto.Cover.FileName), FileMode.Create);
                await createGameDto.Cover.CopyToAsync(stream);
            }
            if (!Directory.Exists(imagesPath))
            {
                Directory.CreateDirectory(imagesPath);
                foreach (var image in createGameDto.Images)
                {
                    using var stream = new FileStream(Path.Combine(imagesPath, image.FileName), FileMode.Create);
                    await image.CopyToAsync(stream);
                }
            }
            if (!Directory.Exists(videosPath))
            {
                Directory.CreateDirectory(videosPath);
                foreach (var video in createGameDto.Videos)
                {
                    using var stream = new FileStream(Path.Combine(videosPath, video.FileName), FileMode.Create);
                    await video.CopyToAsync(stream);
                }
            }

            Game createdGame = new()
            {
                Name = createGameDto.Name,
                Description = createGameDto.Description,
                Price = createGameDto.Price,
                Developers = createGameDto.DeveloperIds != null ? _developerRepository.GetDevelopers().Join(createGameDto.DeveloperIds,
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
                CoverPath = coverPath,
                VideosPath = videosPath,
                ImagesPath = imagesPath,
                Platforms = (await _platformRepository.GetPlatformsAsync()).Join(createGameDto.PlatformIds, platform => platform.Id, receivedPlatformId => receivedPlatformId, (platform, receivedPlatformId) => platform).ToList(),
                Genres = (await _genreRepository.GetGenresAsync()).Join(createGameDto.GenresIds, genre => genre.Id, receivedGenreId => receivedGenreId, (genre, receivedGenreId) => genre).ToList(),
            };
            

            var insertedGame = await _gameRepository.CreateGameAsync(createdGame);
            return CreatedAtAction(nameof(GetGameAsync), new { id = insertedGame.Id }, insertedGame.AsDto());

        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult> UpdateGameAsync(int id, [FromForm] UpdateGameDto updateGameDto)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            if (updateGameDto.Images is not null)
            {
                var path = requestedGame.ImagesPath;

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                foreach (var image in updateGameDto.Images)
                {
                    using var stream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create);
                    await image.CopyToAsync(stream);
                }
            }
            if (updateGameDto.Videos is not null)
            {
                var path = requestedGame.VideosPath;

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                foreach (var video in updateGameDto.Videos)
                {
                    using var stream = new FileStream(Path.Combine(path, video.FileName), FileMode.Create);
                    await video.CopyToAsync(stream);
                }
            }
            if (updateGameDto.Cover is not null)
            {
                var path = requestedGame.CoverPath;

                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                using var stream = new FileStream(Path.Combine(path, updateGameDto.Cover.FileName), FileMode.Create);
                await updateGameDto.Cover.CopyToAsync(stream);
            }


            requestedGame.Discount = updateGameDto.Discount != null ? (float)updateGameDto.Discount : requestedGame.Discount;
            requestedGame.Description = updateGameDto.Description ?? requestedGame.Description;
            requestedGame.Price = updateGameDto.Price != null ? (double)updateGameDto.Price : requestedGame.Price;
            if (updateGameDto.Name is not null)
            {
                if (!updateGameDto.Name.Equals(requestedGame.Name))
                {
                    Directory.Move($"Uploads/{requestedGame.Name}", $"Uploads/{updateGameDto.Name}/Images");
                    requestedGame.Name = updateGameDto.Name;
                    requestedGame.ImagesPath = $"Uploads/{updateGameDto.Name}/Images";
                    requestedGame.VideosPath = $"Uploads/{updateGameDto.Name}/Videos";
                    requestedGame.CoverPath = $"Uploads/{updateGameDto.Name}/Cover";
                }
            }
            requestedGame.ReleaseDate = updateGameDto.ReleaseDate ?? requestedGame.ReleaseDate;
            requestedGame.Developers = updateGameDto.DeveloperIds != null ? _developerRepository.GetDevelopers().Join(updateGameDto.DeveloperIds,
                                                                         developer => developer.Id,
                                                                         developerId => developerId,
                                                                         (developer, developerId) => developer).ToList() : requestedGame.Developers;
            requestedGame.Publisher = updateGameDto.PublisherId != null ? await _publishersRepository.GetPublisherAsync((int)updateGameDto.PublisherId) : requestedGame.Publisher;
            requestedGame.MinimumSystemRequirements = updateGameDto.MinimumSystemRequirements != null ? new MinimumSystemRequirements
            {
                GPU = await _gpuRepository.GetGPUByIdAsync(updateGameDto.MinimumSystemRequirements.GPUId),
                CPU = await _cpuRepository.GetCPUByIdAsync(updateGameDto.MinimumSystemRequirements.CPUId),
                DirectX = await _directXRepository.GetDirectXByIdAsync(updateGameDto.MinimumSystemRequirements.DirectXId),
                Os = await _osRepository.GetOsByIdAsync(updateGameDto.MinimumSystemRequirements.OsId),
                Ram = updateGameDto.MinimumSystemRequirements.Ram,
                Storage = updateGameDto.MinimumSystemRequirements.Storage,
                AdditionalNotes = updateGameDto.MinimumSystemRequirements.AdditionalNotes,
            } : requestedGame.MinimumSystemRequirements;
            requestedGame.RecommendedSystemRequirements = updateGameDto.RecommendedSystemRequirements != null ? new RecommendedSystemRequirements
            {
                GPU = await _gpuRepository.GetGPUByIdAsync(updateGameDto.RecommendedSystemRequirements.GPUId),
                CPU = await _cpuRepository.GetCPUByIdAsync(updateGameDto.RecommendedSystemRequirements.CPUId),
                DirectX = await _directXRepository.GetDirectXByIdAsync(updateGameDto.RecommendedSystemRequirements.DirectXId),
                Os = await _osRepository.GetOsByIdAsync(updateGameDto.RecommendedSystemRequirements.OsId),
                Ram = updateGameDto.RecommendedSystemRequirements.Ram,
                Storage = updateGameDto.RecommendedSystemRequirements.Storage,
                AdditionalNotes = updateGameDto.RecommendedSystemRequirements.AdditionalNotes,
            } : requestedGame.RecommendedSystemRequirements;

            requestedGame.Status = updateGameDto.StatusId != null ? await _statusRepository.GetStatusByIdAsync((int)updateGameDto.StatusId) : requestedGame.Status;
            requestedGame.Franchise = updateGameDto.FranchiseId != null ? await _franchiseRepository.GetFranchiseByIdAsync((int)updateGameDto.FranchiseId) : requestedGame.Franchise;

            await _gameRepository.UpdateGameAsync(requestedGame);
            return NoContent();
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
