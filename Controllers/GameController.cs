using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameHeavenAPI.Controllers
{
    [Route("games")]
    [ApiController]
    public class GameController : ControllerBase
    {
        private readonly IGameRepository _gameRepository;
        private readonly IPublishersRepository _publishersRepository;
        public GameController(IGameRepository gameRepository, IPublishersRepository publishersRepository)
        {
            _gameRepository = gameRepository;
            _publishersRepository = publishersRepository;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GameDto>> GetGameAsync(int id)
        {
            var game = await _gameRepository.GetGameByIdAsync(id);
            if(game is null)
            {
                return NotFound();
            }
            return game.AsDto();
        }
        [HttpPost("new")]
        public async Task<ActionResult<CreateGameDto>> CreateGameAsync(CreateGameDto createGameDto)
        {
            Game createdGame = new()
            {
                Name = createGameDto.Name,
                Description = createGameDto.Description,
                Price = createGameDto.Price,
                Publisher = await _publishersRepository.GetPublisherAsync(createGameDto.PublisherId),
                ReleaseDate = createGameDto.ReleaseDate,
            };
            var insertedGame = await _gameRepository.CreateGameAsync(createdGame);
            return CreatedAtAction(nameof(GetGameAsync), new { id = insertedGame.Id }, insertedGame.AsDto());

        }
    }
}
