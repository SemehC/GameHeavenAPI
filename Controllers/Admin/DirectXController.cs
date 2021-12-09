using GameHeavenAPI.Dtos.DirectXDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.DirectX;
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

    public class DirectXController : ControllerBase
    {
        private readonly IDirectXRepository _platoformRepository;

        public DirectXController(IDirectXRepository DirectXRepository)
        {
            _platoformRepository = DirectXRepository;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<DirectXDto>> GetDirectXsAsync()
        {
            return (await _platoformRepository.GetDirectXsAsync()).Select(DirectX=> DirectX.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<DirectXDto>> GetDirectXAsync(int id)
        {
            var foundDirectX = await _platoformRepository.GetDirectXByIdAsync(id);
            if (foundDirectX is null)
            {
                return NotFound();
            }
            return foundDirectX.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<DirectXDto>> CreateDirectXAsync(CreateDirectXDto createDirectXDto)
        {
            DirectXVersion createdDirectX = new()
            {
                Name = createDirectXDto.Name,
            };
            var insertedDirectX = await _platoformRepository.CreateDirectXAsync(createdDirectX);
            return CreatedAtAction(nameof(GetDirectXAsync), new { id = insertedDirectX.Id }, insertedDirectX.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDirectXAsync(int id, UpdateDirectXDto updateDirectXDto)
        {
            var requestedDirectX = await _platoformRepository.GetDirectXByIdAsync(id);
            if (requestedDirectX is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedDirectX.Name = updateDirectXDto.Name;
                await _platoformRepository.UpdateDirectXAsync(requestedDirectX);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDirectXAsync(int id)
        {
            var requestedGame = await _platoformRepository.GetDirectXByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _platoformRepository.DeleteDirectXAsync(id);
            return NoContent();
        }
    }
}
