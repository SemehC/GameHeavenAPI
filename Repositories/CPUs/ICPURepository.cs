using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.CPUs
{
    public interface ICPURepository
    {
        Task<IEnumerable<CPU>> GetCPUsAsync();
        Task<CPU> GetCPUByIdAsync(int id);
        Task<CPU> CreateCPUAsync(CPU cpu);
        Task DeleteCPUAsync(int id);
        Task UpdateCPUAsync(CPU cpu);
    }
}
