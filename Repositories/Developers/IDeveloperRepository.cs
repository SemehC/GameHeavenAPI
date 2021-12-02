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
        Task<Developer> CreateDeveloperAsync(Developer developer);
        Task<IList<Developer>> GetDevelopersAsync();
        Task<Developer> GetDeveloperAsync(int id);
        Task<Developer> GetDeveloperByUserIdAsync(string id);
        Task DeleteDeveloperAsync(int id);
        Task UpdateDeveloperAsync(Developer developer);

    }
}
