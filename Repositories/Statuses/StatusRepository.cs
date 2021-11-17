using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.Statuses
{
    public class StatusRepository : IStatusRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public StatusRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Status> CreateStatusAsync(Status Status)
        {
            var createdStatus = (await _applicationDbContext.Statuses.AddAsync(Status)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdStatus;
        }

        public async Task DeleteStatusAsync(int id)
        {
            var Status = await _applicationDbContext.Statuses
                .FirstOrDefaultAsync(Status => Status.Id == id);
            _applicationDbContext.Statuses.Remove(Status);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Status> GetStatusByIdAsync(int id)
        {
            return await _applicationDbContext.Statuses
                    .FirstOrDefaultAsync(Status => Status.Id == id);
        }

        public async Task<IEnumerable<Status>> GetStatusesAsync()
        {
            return await _applicationDbContext.Statuses
                .ToListAsync();
        }

        public async Task UpdateStatusAsync(Status Status)
        {
            var StatusToBeUpdated = await _applicationDbContext.Statuses.FirstOrDefaultAsync(StatusInDb => StatusInDb.Id == Status.Id);
            if (StatusToBeUpdated is not null)
            {
                StatusToBeUpdated.Name = Status.Name;
                _applicationDbContext.Statuses.Update(StatusToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
