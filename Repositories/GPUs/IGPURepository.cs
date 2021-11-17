using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.GPUs
{
    public interface IGPURepository
    {
        Task<IEnumerable<GPU>> GetGPUsAsync();
        Task<GPU> GetGPUByIdAsync(int id);
        Task<GPU> CreateGPUAsync(GPU GPU);
        Task DeleteGPUAsync(int id);
        Task UpdateGPUAsync(GPU GPU);
    }
}
