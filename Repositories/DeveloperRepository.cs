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
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext AppDbContext;

        public DeveloperRepository(ApplicationDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public  IEnumerable<Developer> GetDevelopers()
        {
            return AppDbContext.Developers.AsParallel();
        }
        public async Task<ServerResponse<IEnumerable<IdentityError>>> CreateDeveloper(Developer pub)
        {
            var x = await AppDbContext.Developers.AddAsync(pub);
            AppDbContext.SaveChanges();
            var resp = new ServerResponse<IEnumerable<IdentityError>>();

            resp.Success = true;
            resp.Message = new List<string> { x.Entity.Id.ToString() };

            return resp;
        }
    }
}
