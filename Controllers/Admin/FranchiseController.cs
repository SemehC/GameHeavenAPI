using GameHeavenAPI.Dtos.FranchiseDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.Franchises;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;


namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]
    public class FranchiseController : ControllerBase
    {
        private readonly IFranchiseRepository _franchiseRepository;

        public FranchiseController(IFranchiseRepository franchiseRepository)
        {
            _franchiseRepository = franchiseRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<FranchiseDto>> GetFranchisesAsync()
        {
            return (await _franchiseRepository.GetFranchisesAsync()).Select(franchise=> franchise.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<FranchiseDto>> GetFranchiseAsync(int id)
        {
            var foundFranchise = await _franchiseRepository.GetFranchiseByIdAsync(id);
            if (foundFranchise is null)
            {
                return NotFound();
            }
            return foundFranchise.AsDto();
        }

        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<ActionResult<FranchiseDto>> CreateFranchiseAsync([FromForm] CreateFranchiseDto createFranchiseDto)
        {
            string path = $"Uploads/Franchises/{createFranchiseDto.Name}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);

            }
            Franchise createdFranchise = new()
            {
                Name = createFranchiseDto.Name,
                CoverPath = path,
            };
            using var stream = new FileStream(Path.Combine(path, createFranchiseDto.Cover.FileName), FileMode.Create);
            await createFranchiseDto.Cover.CopyToAsync(stream);
            var insertedFranchise = await _franchiseRepository.CreateFranchiseAsync(createdFranchise);
            return CreatedAtAction(nameof(GetFranchiseAsync), new { id = insertedFranchise.Id }, insertedFranchise.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateFranchiseAsync(int id, UpdateFranchiseDto updateFranchiseDto)
        {
            var requestedFranchise = await _franchiseRepository.GetFranchiseByIdAsync(id);
            if (requestedFranchise is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (updateFranchiseDto.Name is not null)
                {
                    if (!updateFranchiseDto.Name.Equals(requestedFranchise.Name))
                    {
                        Directory.Move($"Uploads/Franchises/{requestedFranchise.Name}", $"Uploads/Franchises/{updateFranchiseDto.Name}");
                        requestedFranchise.Name = updateFranchiseDto.Name;
                        requestedFranchise.CoverPath = $"Uploads/Franchises/{updateFranchiseDto.Name}";
                    }
                }
                if(updateFranchiseDto.Cover is not null)
                {
                    var path = requestedFranchise.CoverPath;

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    using var stream = new FileStream(Path.Combine(path, updateFranchiseDto.Cover.FileName), FileMode.Create);
                    await updateFranchiseDto.Cover.CopyToAsync(stream);
                }
                await _franchiseRepository.UpdateFranchiseAsync(requestedFranchise);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteFranchiseAsync(int id)
        {
            var requestedFranchise = await _franchiseRepository.GetFranchiseByIdAsync(id);
            if (requestedFranchise is null)
            {
                return NotFound();
            }
            var path = requestedFranchise.CoverPath;
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);

            }
            await _franchiseRepository.DeleteFranchiseAsync(id);
            return NoContent();
        }
    }
}
