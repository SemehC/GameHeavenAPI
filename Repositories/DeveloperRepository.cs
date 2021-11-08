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

        public  IEnumerable<GetDeveloperdto> getDevelopers()
        {
            var res =  AppDbContext.developers.AsParallel();

            return res.ToList().Select(x => new GetDeveloperdto
            {
               DeveloperDescription = x.DeveloperDescription,
               DeveloperEmail = x.DeveloperEmail,
               DeveloperName = x.DeveloperName
            });
        }
        public async Task<ServerResponse<IEnumerable<IdentityError>>> createDeveloper(Developer pub)
        {
            var x = await AppDbContext.developers.AddAsync(pub);
            AppDbContext.SaveChanges();
            var resp = new ServerResponse<IEnumerable<IdentityError>>();

            resp.Success = true;

            return resp;
        }
    }
}
