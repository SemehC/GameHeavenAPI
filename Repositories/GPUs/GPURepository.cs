using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.GPUs
{
    public class GPURepository:IGPURepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GPURepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GPU> CreateGPUAsync(GPU GPU)
        {
            var createdGPU = (await _applicationDbContext.GPUs.AddAsync(GPU)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdGPU;
        }

        public async Task DeleteGPUAsync(int id)
        {
            var GPU = await _applicationDbContext.GPUs
                .FirstOrDefaultAsync(GPU => GPU.Id == id);
            _applicationDbContext.GPUs.Remove(GPU);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<GPU> GetGPUByIdAsync(int id)
        {
            return await _applicationDbContext.GPUs
                    .FirstOrDefaultAsync(GPU => GPU.Id == id);
        }

        public async Task<IEnumerable<GPU>> GetGPUsAsync()
        {
            return await _applicationDbContext.GPUs
                .ToListAsync();
        }

        public async Task UpdateGPUAsync(GPU GPU)
        {
            var GPUToBeUpdated = await _applicationDbContext.GPUs.FirstOrDefaultAsync(GPUInDb => GPUInDb.Id == GPU.Id);
            if (GPUToBeUpdated is not null)
            {
                GPUToBeUpdated.Name = GPU.Name;
                GPUToBeUpdated.Description = GPU.Description;
                _applicationDbContext.GPUs.Update(GPUToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
