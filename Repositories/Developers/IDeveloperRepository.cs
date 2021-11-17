using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public interface IDeveloperRepository
    {
        IList<Developer> GetDevelopers();
        Task<ServerResponse<IEnumerable<IdentityError>>> CreateDeveloper(Developer pub);

    }
}
