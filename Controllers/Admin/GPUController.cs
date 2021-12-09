using GameHeavenAPI.Dtos.GPUDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.GPUs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]
    public class GPUController : ControllerBase
    {
        private readonly IGPURepository _GPURepGPUitory;

        public GPUController(IGPURepository GPURepGPUitory)
        {
            _GPURepGPUitory = GPURepGPUitory;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<GPUDto>> GetGPUsAsync()
        {
            return (await _GPURepGPUitory.GetGPUsAsync()).Select(GPU => GPU.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<GPUDto>> GetGPUAsync(int id)
        {
            var foundGPU = await _GPURepGPUitory.GetGPUByIdAsync(id);
            if (foundGPU is null)
            {
                return NotFound();
            }
            return foundGPU.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<GPUDto>> CreateGPUAsync(CreateGPUDto createGPUDto)
        {
            GPU createdGPU = new()
            {
                Name = createGPUDto.Name,
                Description = createGPUDto.Description,
                Price = createGPUDto.Price,
        };
            var insertedGPU = await _GPURepGPUitory.CreateGPUAsync(createdGPU);
            return CreatedAtAction(nameof(GetGPUAsync), new { id = insertedGPU.Id }, insertedGPU.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateGPUAsync(int id, UpdateGPUDto updateGPUDto)
        {
            var requestedGPU = await _GPURepGPUitory.GetGPUByIdAsync(id);
            if (requestedGPU is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedGPU.Name = updateGPUDto.Name;
                requestedGPU.Description = updateGPUDto.Description;
                requestedGPU.Price = updateGPUDto.Price;
                await _GPURepGPUitory.UpdateGPUAsync(requestedGPU);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteGPUAsync(int id)
        {
            var requestedGame = await _GPURepGPUitory.GetGPUByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _GPURepGPUitory.DeleteGPUAsync(id);
            return NoContent();
        }
    }
}
