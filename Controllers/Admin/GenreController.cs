using GameHeavenAPI.Dtos.GenreDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.Genres;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]
    public class GenreController : ControllerBase
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            _genreRepository = genreRepository;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<GenreDto>> GetGenresAsync()
        {
            return (await _genreRepository.GetGenresAsync()).Select(genre=> genre.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<GenreDto>> GetGenreAsync(int id)
        {
            var foundGenre = await _genreRepository.GetGenreByIdAsync(id);
            if (foundGenre is null)
            {
                return NotFound();
            }
            return foundGenre.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<GenreDto>> CreateGenreAsync(CreateGenreDto createGenreDto)
        {
            Genre createdGenre = new()
            {
                Name = createGenreDto.Name,
                Description = createGenreDto.Description
            };
            var insertedGenre = await _genreRepository.CreateGenreAsync(createdGenre);
            return CreatedAtAction(nameof(GetGenreAsync), new { id = insertedGenre.Id }, insertedGenre.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGenreAsync(int id, UpdateGenreDto updateGenreDto)
        {
            var requestedGenre = await _genreRepository.GetGenreByIdAsync(id);
            if (requestedGenre is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedGenre.Description = updateGenreDto.Description;
                requestedGenre.Name = updateGenreDto.Name;
                await _genreRepository.UpdateGenreAsync(requestedGenre);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGenreAsync(int id)
        {
            var requestedGame = await _genreRepository.GetGenreByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _genreRepository.DeleteGenreAsync(id);
            return NoContent();
        }
    }
}
