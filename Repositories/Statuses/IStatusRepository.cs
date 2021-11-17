using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.Statuses
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetStatusesAsync();
        Task<Status> GetStatusByIdAsync(int id);
        Task<Status> CreateStatusAsync(Status status);
        Task DeleteStatusAsync(int id);
        Task UpdateStatusAsync(Status status);
    }
}
