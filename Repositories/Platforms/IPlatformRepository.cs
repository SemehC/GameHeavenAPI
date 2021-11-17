
using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Platforms
{
    public interface IPlatformRepository
    {
        Task<IEnumerable<Platform>> GetPlatformsAsync();
        Task<Platform> GetPlatformByIdAsync(int id);
        Task<Platform> CreatePlatformAsync(Platform platform);
        Task DeletePlatformAsync(int id);
        Task UpdatePlatformAsync(Platform platform);
    }
}
