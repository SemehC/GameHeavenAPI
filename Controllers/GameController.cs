using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

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
        public async Task<ActionResult<UpdateGameDto>> CreateGameAsync(CreateGameDto createGameDto)
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
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGameAsync(int id, UpdateGameDto updateGameDto)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if(requestedGame is null )
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                requestedGame.Discount = updateGameDto.Discount;
                requestedGame.Description = updateGameDto.Description;
                requestedGame.Price = updateGameDto.Price;
                requestedGame.Name = updateGameDto.Name;
                requestedGame.ReleaseDate = updateGameDto.ReleaseDate;
                requestedGame.Status = updateGameDto.Status;
                await _gameRepository.UpdateGameAsync(requestedGame);
            }
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGameAsync(int id)
        {
            var requestedGame = await _gameRepository.GetGameByIdAsync(id);
            if(requestedGame is null)
            {
                return NotFound();
            }
            await _gameRepository.DeleteGameAsync(id);
            return NoContent();
        }
    }
}
