using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Platforms
{
    public class PlatformRepository : IPlatformRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PlatformRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Platform> CreatePlatformAsync(Platform platform)
        {
            var createdPlatform = (await _applicationDbContext.Platforms.AddAsync(platform)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdPlatform;
        }

        public async Task DeletePlatformAsync(int id)
        {
            var platform = await _applicationDbContext.Platforms
                .FirstOrDefaultAsync(Platform => Platform.Id == id);
            _applicationDbContext.Platforms.Remove(platform);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Platform> GetPlatformByIdAsync(int id)
        {
            return await _applicationDbContext.Platforms
                    .FirstOrDefaultAsync(platform => platform.Id == id);
        }

        public async Task<IEnumerable<Platform>> GetPlatformsAsync()
        {
            return await _applicationDbContext.Platforms
                .ToListAsync();
        }

        public async Task UpdatePlatformAsync(Platform platform)
        {
            var platformToBeUpdated = await _applicationDbContext.Platforms.FirstOrDefaultAsync(platformInDb => platformInDb.Id == platform.Id);
            if (platformToBeUpdated is not null)
            {
                platformToBeUpdated.Name = platform.Name;
                _applicationDbContext.Platforms.Update(platformToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
