using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.Oses
{
    public interface IOsRepository
    {
        Task<IEnumerable<Os>> GetOsesAsync();
        Task<Os> GetOsByIdAsync(int id);
        Task<Os> CreateOsAsync(Os Os);
        Task DeleteOsAsync(int id);
        Task UpdateOsAsync(Os Os);
    }
}
