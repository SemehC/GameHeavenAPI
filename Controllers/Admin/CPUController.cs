using GameHeavenAPI.Dtos.CPUDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.CPUs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]
    public class CPUController : ControllerBase
    {
        private readonly ICPURepository _CPURepCPUitory;

        public CPUController(ICPURepository CPURepCPUitory)
        {
            _CPURepCPUitory = CPURepCPUitory;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IEnumerable<CPUDto>> GetCPUsAsync()
        {
            return (await _CPURepCPUitory.GetCPUsAsync()).Select(CPU => CPU.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        [AllowAnonymous]

        public async Task<ActionResult<CPUDto>> GetCPUAsync(int id)
        {
            var foundCPU = await _CPURepCPUitory.GetCPUByIdAsync(id);
            if (foundCPU is null)
            {
                return NotFound();
            }
            return foundCPU.AsDto();
        }

        [HttpPost]

        public async Task<ActionResult<CPUDto>> CreateCPUAsync(CreateCPUDto createCPUDto)
        {
            CPU createdCPU = new()
            {
                Name = createCPUDto.Name,
                Description = createCPUDto.Description,
                Price = createCPUDto.Price,
        };
            var insertedCPU = await _CPURepCPUitory.CreateCPUAsync(createdCPU);
            return CreatedAtAction(nameof(GetCPUAsync), new { id = insertedCPU.Id }, insertedCPU.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCPUAsync(int id, UpdateCPUDto updateCPUDto)
        {
            var requestedCPU = await _CPURepCPUitory.GetCPUByIdAsync(id);
            if (requestedCPU is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedCPU.Name = updateCPUDto.Name;
                requestedCPU.Description = updateCPUDto.Description;
                requestedCPU.Price = updateCPUDto.Price;
                await _CPURepCPUitory.UpdateCPUAsync(requestedCPU);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCPUAsync(int id)
        {
            var requestedGame = await _CPURepCPUitory.GetCPUByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _CPURepCPUitory.DeleteCPUAsync(id);
            return NoContent();
        }
    }
}
