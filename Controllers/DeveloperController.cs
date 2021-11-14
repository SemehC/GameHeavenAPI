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
        public IEnumerable<GetDeveloperDto> GetDevelopers()
        {
            var developers = repository.GetDevelopers().ToList();
            var developerDtos = new List<GetDeveloperDto>();
            for (int i = 0; i < developers.Count; i++)
            {
                developerDtos.Add(new GetDeveloperDto
                {
                    DeveloperDescription = developers[i].DeveloperDescription,
                    DeveloperEmail = developers[i].DeveloperEmail,
                    DeveloperName = developers[i].DeveloperName,
                });
            }
            return developerDtos;
        }
        [HttpPost("new")]
        public  async  Task<ServerResponse<IEnumerable<IdentityError>>> AddDeveloper(CreateDeveloperDto dto)
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
