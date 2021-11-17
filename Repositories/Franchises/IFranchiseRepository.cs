using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.Franchises
{
    public interface IFranchiseRepository
    {
        Task<IEnumerable<Franchise>> GetFranchisesAsync();
        Task<Franchise> GetFranchiseByIdAsync(int id);
        Task<Franchise> CreateFranchiseAsync(Franchise franchise);
        Task DeleteFranchiseAsync(int id);
        Task UpdateFranchiseAsync(Franchise franchise);
    }
}
