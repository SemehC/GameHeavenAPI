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
            string path = $"Uploads/{createGameDto.Name}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            List<GameImage> imgs = new();

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
                Platforms = (await _platformRepository.GetPlatformsAsync()).Join(createGameDto.PlatformIds, platform => platform.Id, receivedPlatformId => receivedPlatformId, (platform, receivedPlatformId) => platform).ToList(),
                Genres = (await _genreRepository.GetGenresAsync()).Join(createGameDto.GenresIds, genre => genre.Id, receivedGenreId => receivedGenreId, (genre, receivedGenreId) => genre).ToList(),
            };

            foreach (var image in createGameDto.Images)
            {
                var gameImg = new GameImage
                {
                    Path = $"Uploads/{createGameDto.Name}",
                    Alt = "",
                };
                imgs.Add(gameImg);
                using var stream = new FileStream(Path.Combine(gameImg.Path, image.FileName), FileMode.Create);
                await image.CopyToAsync(stream);
            }
            createdGame.Images = imgs;
            var insertedGame = await _gameRepository.CreateGameAsync(createdGame);
            return CreatedAtAction(nameof(GetGameAsync), new { id = insertedGame.Id }, insertedGame.AsDto());

        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameAsync(int id, UpdateGameDto updateGameDto)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedGame.Discount = updateGameDto.Discount;
                requestedGame.Description = updateGameDto.Description;
                requestedGame.Price = updateGameDto.Price;
                requestedGame.Name = updateGameDto.Name;
                requestedGame.ReleaseDate = updateGameDto.ReleaseDate;
                await _gameRepository.UpdateGameAsync(requestedGame);
            }
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
            var path = requestedGame.Images[0].Path;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);

            }
            await _gameRepository.DeleteGameAsync(id);
            return NoContent();
        }
    }
}
