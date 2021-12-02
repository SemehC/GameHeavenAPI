using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly ApplicationDbContext _appDbContext;

        public DeveloperRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<Developer>> GetDevelopersAsync()
        {
            return await _appDbContext.CompleteDeveloper().Include(developer => developer.User).ToListAsync();
        }

        public async Task<Developer> GetDeveloperAsync(int DeveloperId)
        {
            var res = await _appDbContext.CompleteDeveloper().FirstOrDefaultAsync(Developer => Developer.Id == DeveloperId);
            if (res == null)
            {
                return null;
            }
            return res;
        }
        public async Task DeleteDeveloperAsync(int DeveloperId)
        {
            var result = await _appDbContext.CompleteDeveloper().FirstOrDefaultAsync(e => e.Id == DeveloperId);
            if (result != null)
            {
                _appDbContext.Developers.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }


        public async Task UpdateDeveloperAsync(Developer pub)
        {
            var res = await _appDbContext.Developers.FirstOrDefaultAsync(p => p.Id == pub.Id);
            if (res != null)
            {
                res.Name = pub.Name;
                res.Description = pub.Description;
                res.WebsiteLink = pub.WebsiteLink;
                res.TwitterLink = pub.TwitterLink;
                res.FacebookLink = pub.FacebookLink;
                res.CoverPath = pub.CoverPath;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Developer> CreateDeveloperAsync(Developer pub)
        {
            var createdDeveloper = (await _appDbContext.Developers.AddAsync(pub)).Entity;
            await _appDbContext.SaveChangesAsync();
            return createdDeveloper;
        }

        public async Task<Developer> GetDeveloperByUserIdAsync(string id)
        {
            return await _appDbContext.CompleteDeveloper().FirstOrDefaultAsync(d => d.User.Id == id);
        }
    }
}
