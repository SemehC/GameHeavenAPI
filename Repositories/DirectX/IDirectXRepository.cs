using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.DirectX
{
    public interface IDirectXRepository
    {
        Task<IEnumerable<DirectXVersion>> GetDirectXsAsync();
        Task<DirectXVersion> GetDirectXByIdAsync(int id);
        Task<DirectXVersion> CreateDirectXAsync(DirectXVersion DirectX);
        Task DeleteDirectXAsync(int id);
        Task UpdateDirectXAsync(DirectXVersion DirectX);
    }
}
