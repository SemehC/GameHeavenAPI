using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Controllers
{

    [Route("developers")]
    [ApiController]
    public class DeveloperController : Controller
    {
        public IDeveloperRepository repository;
        public DeveloperController(IDeveloperRepository repository)
        {
            this.repository = repository;
        }
        [HttpGet]
        public IEnumerable<DeveloperDto> GetDevelopers()
        {
            return repository.GetDevelopers().Select(developer => developer.AsDto()).ToList();
        }
        [HttpPost("new")]
        public async Task<ServerResponse<IEnumerable<IdentityError>>> AddDeveloper(CreateDeveloperDto dto)
        {
            var res = await repository.CreateDeveloper(new Developer
            {
                DeveloperDescription = dto.DeveloperDescription,
                DeveloperEmail = dto.DeveloperEmail,
                DeveloperName = dto.DeveloperName,
                DeveloperPassword = dto.DeveloperPassword
            });
            return res;
        }
    }
}
