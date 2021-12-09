using GameHeavenAPI.Dtos.PublisherDtos;
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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

    public class PublisherController : ControllerBase
    {
        private readonly IPublishersRepository _repository;
        private readonly UserManager<ApplicationUser> _userManager;
        public PublisherController(IPublishersRepository repository, UserManager<ApplicationUser> userManager)
        {
            _repository = repository;
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> GetPublishers()
        {
            return Ok((await _repository.GetPublishersAsync()).Select(Publisher => Publisher.AsDto()).ToList());
        }

        [HttpPost]
        [Authorize(Roles = nameof(Roles.User))]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> AddPublisher([FromForm] CreatePublisherDto dto)
        {
            string path = $"Uploads/Publishers/{dto.Name}";
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var filePath = $"{path}/{dto.Cover.FileName}";
            var encodedPath = HttpUtility.UrlEncode(filePath);
            var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
            using var stream = new FileStream(Path.Combine(path, dto.Cover.FileName), FileMode.Create);
            await dto.Cover.CopyToAsync(stream);
            var Publisher = await _repository.CreatePublisherAsync(new Publisher
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
            var response = Publisher.AsDto();
            response.Success = true;
            return Ok(response);
        }
        [HttpGet("{id}")]
        [AllowAnonymous]
        [Authorize(Roles = nameof(Roles.User))]
        public async Task<ActionResult<PublisherDto>> GetPublisherAsync(int id)
        {
            var Publisher = await _repository.GetPublisherAsync(id);
            if (Publisher is not null)
            {
                var p = Publisher.AsDto();
                p.Success = true;
                return p;
            }
            return NotFound();


        }
        [HttpGet("GetPublisherByUserId/{id}")]

        public async Task<ActionResult<PublisherDto>> GetPublisherByUserIdAsync(string id)
        {
            var Publisher = await _repository.GetPublisherByUserAsync(id);
            if (Publisher is not null)
            {
                return Publisher.AsDto();
            }
            return NotFound();


        }
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Roles.User))]

        public async Task<ActionResult> DeletePublisherAsync(int id)
        {
            var res = await _repository.GetPublisherAsync(id);
            if (res is null)
            {
                return NotFound();
            }
            if (Directory.Exists($"Uploads/Publishers/{res.Name}"))
            {
                Directory.Delete($"Uploads/Publishers/{res.Name}", true);
            }
            await _repository.DeletePublisherAsync(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        [Consumes("multipart/form-data")]
        [Authorize(Roles = nameof(Roles.User))]
        public async Task<ActionResult> UpdatePublisherAsync(int id, [FromForm] UpdatePublisherDto updatePublisherDto)
        {
            var PublisherToUpdate = await _repository.GetPublisherAsync(id);
            if (PublisherToUpdate is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (updatePublisherDto.Name is not null)
                {
                    if (!updatePublisherDto.Name.Equals(PublisherToUpdate.Name))
                    {
                        Directory.Move($"Uploads/Publishers/{PublisherToUpdate.Name}", $"Uploads/Publishers/{updatePublisherDto.Name}");
                        PublisherToUpdate.Name = updatePublisherDto.Name;
                        var fileName = Path.GetFileName(HttpUtility.UrlDecode(PublisherToUpdate.CoverPath));
                        var encodedPath = HttpUtility.UrlEncode($"Uploads/Publishers/{updatePublisherDto.Name}/{fileName}");
                        var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
                        PublisherToUpdate.CoverPath = coverLink;

                    }
                }
                if (updatePublisherDto.Cover is not null)
                {
                    var path = $"Uploads/Publishers/{PublisherToUpdate.Name}";

                    if (Directory.Exists(path))
                    {
                        Directory.Delete(path, true);
                    }
                    Directory.CreateDirectory(path);
                    var filePath = $"{path}/{updatePublisherDto.Cover.FileName}";
                    var encodedPath = HttpUtility.UrlEncode(filePath);
                    var coverLink = $"https://localhost:5001/GetImage/{encodedPath}";
                    using var stream = new FileStream(Path.Combine(path, updatePublisherDto.Cover.FileName), FileMode.Create);
                    await updatePublisherDto.Cover.CopyToAsync(stream);
                    PublisherToUpdate.CoverPath = coverLink;
                }
                PublisherToUpdate.Name = updatePublisherDto.Name;
                PublisherToUpdate.Description = updatePublisherDto.Description;
                PublisherToUpdate.WebsiteLink = updatePublisherDto.WebsiteLink;
                PublisherToUpdate.FacebookLink = updatePublisherDto.FacebookLink;
                PublisherToUpdate.TwitterLink = updatePublisherDto.TwitterLink;
                PublisherToUpdate.User = updatePublisherDto.UserId != null ? await _userManager.FindByIdAsync(updatePublisherDto.UserId) : PublisherToUpdate.User;
            }
            await _repository.UpdatePublisherAsync(PublisherToUpdate);
            return Ok(new Response
            {
                Success = true,
                Messages = new List<string>()
                {
                    "Publisher successfully",
                }
            });

        }
    }
}
