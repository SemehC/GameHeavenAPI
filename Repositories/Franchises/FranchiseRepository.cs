using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Franchises
{
    public class FranchiseRepository : IFranchiseRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public FranchiseRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Franchise> CreateFranchiseAsync(Franchise Franchise)
        {
            var createdFranchise = (await _applicationDbContext.Franchises.AddAsync(Franchise)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdFranchise;
        }

        public async Task DeleteFranchiseAsync(int id)
        {
            var Franchise = await _applicationDbContext.Franchises
                .FirstOrDefaultAsync(Franchise => Franchise.Id == id);
            _applicationDbContext.Franchises.Remove(Franchise);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Franchise> GetFranchiseByIdAsync(int id)
        {
            return await _applicationDbContext.Franchises
                    .FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<IEnumerable<Franchise>> GetFranchisesAsync()
        {
            return await _applicationDbContext.Franchises
                .ToListAsync();
        }

        public async Task UpdateFranchiseAsync(Franchise franchise)
        {
            var franchiseToBeUpdated = await _applicationDbContext.Franchises.FirstOrDefaultAsync(franchiseInDb => franchiseInDb.Id == franchise.Id);
            if (franchiseToBeUpdated is not null)
            {
                franchiseToBeUpdated.Name = franchise.Name;
                _applicationDbContext.Franchises.Update(franchiseToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
