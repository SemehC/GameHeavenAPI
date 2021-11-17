using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.Oses
{
    public class OsRepository : IOsRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public OsRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Os> CreateOsAsync(Os Os)
        {
            var createdOs = (await _applicationDbContext.Oses.AddAsync(Os)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdOs;
        }

        public async Task DeleteOsAsync(int id)
        {
            var Os = await _applicationDbContext.Oses
                .FirstOrDefaultAsync(Os => Os.Id == id);
            _applicationDbContext.Oses.Remove(Os);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Os> GetOsByIdAsync(int id)
        {
            return await _applicationDbContext.Oses
                    .FirstOrDefaultAsync(Os => Os.Id == id);
        }

        public async Task<IEnumerable<Os>> GetOsesAsync()
        {
            return await _applicationDbContext.Oses
                .ToListAsync();
        }

        public async Task UpdateOsAsync(Os Os)
        {
            var OsToBeUpdated = await _applicationDbContext.Oses.FirstOrDefaultAsync(OsInDb => OsInDb.Id == Os.Id);
            if (OsToBeUpdated is not null)
            {
                OsToBeUpdated.Name = Os.Name;
                _applicationDbContext.Oses.Update(OsToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
