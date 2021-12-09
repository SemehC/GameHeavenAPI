using GameHeavenAPI.Dtos.OsDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.Oses;
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

    public class OsController : ControllerBase
    {
        private readonly IOsRepository _OsRepository;

        public OsController(IOsRepository OsRepository)
        {
            _OsRepository = OsRepository;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<OsDto>> GetOsesAsync()
        {
            return (await _OsRepository.GetOsesAsync()).Select(Os => Os.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<OsDto>> GetOsAsync(int id)
        {
            var foundOs = await _OsRepository.GetOsByIdAsync(id);
            if (foundOs is null)
            {
                return NotFound();
            }
            return foundOs.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<OsDto>> CreateOsAsync(CreateOsDto createOsDto)
        {
            Os createdOs = new()
            {
                Name = createOsDto.Name,
            };
            var insertedOs = await _OsRepository.CreateOsAsync(createdOs);
            return CreatedAtAction(nameof(GetOsAsync), new { id = insertedOs.Id }, insertedOs.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateOsAsync(int id, UpdateOsDto updateOsDto)
        {
            var requestedOs = await _OsRepository.GetOsByIdAsync(id);
            if (requestedOs is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedOs.Name = updateOsDto.Name;
                await _OsRepository.UpdateOsAsync(requestedOs);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteOsAsync(int id)
        {
            var requestedGame = await _OsRepository.GetOsByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _OsRepository.DeleteOsAsync(id);
            return NoContent();
        }
    }
}
