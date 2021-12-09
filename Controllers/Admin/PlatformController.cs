using GameHeavenAPI.Dtos.PlatformDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.Platforms;
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
    public class PlatformController : ControllerBase
    {
        private readonly IPlatformRepository _platoformRepository;

        public PlatformController(IPlatformRepository platformRepository)
        {
            _platoformRepository = platformRepository;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<PlatformDto>> GetPlatformsAsync()
        {
            return (await _platoformRepository.GetPlatformsAsync()).Select(platform=> platform.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<PlatformDto>> GetPlatformAsync(int id)
        {
            var foundPlatform = await _platoformRepository.GetPlatformByIdAsync(id);
            if (foundPlatform is null)
            {
                return NotFound();
            }
            return foundPlatform.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<PlatformDto>> CreatePlatformAsync(CreatePlatformDto createPlatformDto)
        {
            Platform createdPlatform = new()
            {
                Name = createPlatformDto.Name,
            };
            var insertedPlatform = await _platoformRepository.CreatePlatformAsync(createdPlatform);
            return CreatedAtAction(nameof(GetPlatformAsync), new { id = insertedPlatform.Id }, insertedPlatform.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePlatformAsync(int id, UpdatePlatformDto updatePlatformDto)
        {
            var requestedPlatform = await _platoformRepository.GetPlatformByIdAsync(id);
            if (requestedPlatform is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedPlatform.Name = updatePlatformDto.Name;
                await _platoformRepository.UpdatePlatformAsync(requestedPlatform);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePlatformAsync(int id)
        {
            var requestedGame = await _platoformRepository.GetPlatformByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _platoformRepository.DeletePlatformAsync(id);
            return NoContent();
        }
    }
}
