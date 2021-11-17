using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.DirectX
{
    public class DirectXRepository : IDirectXRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public DirectXRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<DirectXVersion> CreateDirectXAsync(DirectXVersion DirectXVersion)
        {
            var createdDirectXVersion = (await _applicationDbContext.DirectXVersions.AddAsync(DirectXVersion)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdDirectXVersion;
        }

        public async Task DeleteDirectXAsync(int id)
        {
            var DirectXVersion = await _applicationDbContext.DirectXVersions
                .FirstOrDefaultAsync(DirectXVersion => DirectXVersion.Id == id);
            _applicationDbContext.DirectXVersions.Remove(DirectXVersion);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<DirectXVersion> GetDirectXByIdAsync(int id)
        {
            return await _applicationDbContext.DirectXVersions
                    .FirstOrDefaultAsync(DirectXVersion => DirectXVersion.Id == id);
        }

        public async Task<IEnumerable<DirectXVersion>> GetDirectXsAsync()
        {
            return await _applicationDbContext.DirectXVersions
                .ToListAsync();
        }

        public async Task UpdateDirectXAsync(DirectXVersion DirectXVersion)
        {
            var DirectXVersionToBeUpdated = await _applicationDbContext.DirectXVersions.FirstOrDefaultAsync(DirectXVersionInDb => DirectXVersionInDb.Id == DirectXVersion.Id);
            if (DirectXVersionToBeUpdated is not null)
            {
                DirectXVersionToBeUpdated.Name = DirectXVersion.Name;
                _applicationDbContext.DirectXVersions.Update(DirectXVersionToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
