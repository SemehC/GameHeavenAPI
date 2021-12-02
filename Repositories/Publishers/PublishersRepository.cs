using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace GameHeavenAPI.Repositories
{
    public class PublishersRepository : IPublishersRepository
    {

        private readonly ApplicationDbContext _appDbContext;

        public PublishersRepository(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IList<Publisher>> GetPublishersAsync()
        {
            return await _appDbContext.CompletePublisher().ToListAsync();
        }

        public async Task<Publisher> GetPublisherAsync(int PublisherId)
        {
            var res = await _appDbContext.CompletePublisher().FirstOrDefaultAsync(publisher => publisher.Id == PublisherId);
            if (res == null)
            {
                return null;
            }
            return res;
        }
        public async Task DeletePublisherAsync(int PublisherId)
        {
            var result = await _appDbContext.Publishers.FirstOrDefaultAsync(e => e.Id == PublisherId);
            if (result != null)
            {
                _appDbContext.Publishers.Remove(result);
                await _appDbContext.SaveChangesAsync();
            }
        }
        

        public async Task UpdatePublisherAsync(Publisher pub)
        {
            var res = await _appDbContext.Publishers.FirstOrDefaultAsync(p => p.Id == pub.Id);
            if (res != null)
            {
                res.Name = pub.Name;
                res.Description = pub.Description;
                await _appDbContext.SaveChangesAsync();
            }
        }

        public async Task<Publisher> CreatePublisherAsync(Publisher pub)
        {
            var createdPublisher = (await _appDbContext.Publishers.AddAsync(pub)).Entity;
            await _appDbContext.SaveChangesAsync();
            return createdPublisher;
        }

        public async Task<Publisher> GetPublisherByUserAsync(string id)
        {
            var res = await _appDbContext.CompletePublisher().FirstOrDefaultAsync(publisher => publisher.User.Id == id);
            if (res == null)
            {
                return null;
            }
            return res;
        }
    }
}
