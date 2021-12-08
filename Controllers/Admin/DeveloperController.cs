using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = nameof(Roles.Admin))]

    public class DeveloperController : ControllerBase
    {
        private readonly IDeveloperRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public DeveloperController(IDeveloperRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetDevelopers()
        {
            return Ok((await _repository.GetDevelopersAsync()).Select(Developer => Developer.AsDto()).ToList());
        }

        [HttpPost]
        [Consumes("multipart/form-data")]

        public async Task<IActionResult> AddDeveloper([FromForm] CreateDeveloperDto dto)
        {
            string path = $"Uploads/Developers/{dto.Name}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = $"{path}/{dto.Cover.FileName}";
            var encodedPath = HttpUtility.UrlEncode(filePath);
            var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
            using var stream = new FileStream(Path.Combine(path, dto.Cover.FileName), FileMode.Create);
            await dto.Cover.CopyToAsync(stream);
            var Developer = await _repository.CreateDeveloperAsync(new Developer
            {
                Description = dto.Description,
                Name = dto.Name,
                FacebookLink = dto.FacebookLink,
                JoinDate = DateTimeOffset.Now,
                TwitterLink = dto.TwitterLink,
                WebsiteLink = dto.WebsiteLink,
                CoverPath = coverLink,
                User = await _userManager.FindByIdAsync(dto.UserId),
            });
            var response = Developer.AsDto();
            response.Success = true;
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<DeveloperDto>> GetDeveloperAsync(int id)
        {
            var Developer = await _repository.GetDeveloperAsync(id);
            if (Developer is not null)
            {
                return Developer.AsDto();
            }
            return NotFound();


        }
        [HttpGet("GetDeveloperByUserId/{id}")]
        public async Task<ActionResult<DeveloperDto>> GetDeveloperByUser(string id)
        {
            var Developer = await _repository.GetDeveloperByUserIdAsync(id);
            if (Developer is not null)
            {
                return Developer.AsDto();
            }
            return NotFound();


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteDeveloperAsync(int id)
        {
            var res = await _repository.GetDeveloperAsync(id);
            if (res is null)
            {
                return NotFound();
            }
            if (Directory.Exists($"Uploads/Developers/{res.Name}"))
            {
                Directory.Delete($"Uploads/Developers/{res.Name}", true);
            }
            await _repository.DeleteDeveloperAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]

        public async Task<ActionResult> UpdateDeveloperAsync(int id, [FromForm] UpdateDeveloperDto updateDeveloperDto)
        {
            var DeveloperToUpdate = await _repository.GetDeveloperAsync(id);
            if (DeveloperToUpdate is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (updateDeveloperDto.Name is not null)
                {
                    if (!updateDeveloperDto.Name.Equals(DeveloperToUpdate.Name))
                    {
                        Directory.Move($"Uploads/Developers/{DeveloperToUpdate.Name}", $"Uploads/Developers/{updateDeveloperDto.Name}");
                        DeveloperToUpdate.Name = updateDeveloperDto.Name;
                        var fileName = Path.GetFileName(HttpUtility.UrlDecode(DeveloperToUpdate.CoverPath));
                        var encodedPath = HttpUtility.UrlEncode($"Uploads/Developers/{updateDeveloperDto.Name}/{fileName}");
                        var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
                        DeveloperToUpdate.CoverPath = coverLink;

                    }
                }
                if (updateDeveloperDto.Cover is not null)
                {
                    var path = $"Uploads/Developers/{DeveloperToUpdate.Name}";

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    var filePath = $"{path}/{updateDeveloperDto.Cover.FileName}";
                    var encodedPath = HttpUtility.UrlEncode(filePath);
                    var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
                    using var stream = new FileStream(Path.Combine(path, updateDeveloperDto.Cover.FileName), FileMode.Create);
                    await updateDeveloperDto.Cover.CopyToAsync(stream);
                    DeveloperToUpdate.CoverPath = coverLink;
                }
                DeveloperToUpdate.Name = updateDeveloperDto.Name;
                DeveloperToUpdate.Description = updateDeveloperDto.Description;
                DeveloperToUpdate.WebsiteLink = updateDeveloperDto.WebsiteLink;
                DeveloperToUpdate.FacebookLink = updateDeveloperDto.FacebookLink;
                DeveloperToUpdate.TwitterLink = updateDeveloperDto.TwitterLink;
                DeveloperToUpdate.User = await _userManager.FindByIdAsync(updateDeveloperDto.UserId);
            }
            await _repository.UpdateDeveloperAsync(DeveloperToUpdate);
            return NoContent();

        }
    }
}
