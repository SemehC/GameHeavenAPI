using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.CPUs
{
    public class CPURepository : ICPURepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CPURepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<CPU> CreateCPUAsync(CPU CPU)
        {
            var createdCPU = (await _applicationDbContext.CPUs.AddAsync(CPU)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdCPU;
        }

        public async Task DeleteCPUAsync(int id)
        {
            var CPU = await _applicationDbContext.CPUs
                .FirstOrDefaultAsync(CPU => CPU.Id == id);
            _applicationDbContext.CPUs.Remove(CPU);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<CPU> GetCPUByIdAsync(int id)
        {
            return await _applicationDbContext.CPUs
                    .FirstOrDefaultAsync(CPU => CPU.Id == id);
        }

        public async Task<IEnumerable<CPU>> GetCPUsAsync()
        {
            return await _applicationDbContext.CPUs
                .ToListAsync();
        }

        public async Task UpdateCPUAsync(CPU CPU)
        {
            var CPUToBeUpdated = await _applicationDbContext.CPUs.FirstOrDefaultAsync(CPUInDb => CPUInDb.Id == CPU.Id);
            if (CPUToBeUpdated is not null)
            {
                CPUToBeUpdated.Name = CPU.Name;
                CPUToBeUpdated.Description = CPU.Description;
                _applicationDbContext.CPUs.Update(CPUToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
