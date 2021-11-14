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

        private readonly ApplicationDbContext AppDbContext;

        public PublishersRepository(ApplicationDbContext appDbContext)
        {
            AppDbContext = appDbContext;
        }

        public IEnumerable<Publisher> GetPublishers()
        {
            var r = AppDbContext.Publisher.AsParallel();

            return r.ToList();
        }

        public async Task<ServerResponse<IEnumerable<IdentityError>>> CreatePublisher(Publisher pub)
        {
            var x = await AppDbContext.Publisher.AddAsync(pub);
            AppDbContext.SaveChanges();
            var resp = new ServerResponse<IEnumerable<IdentityError>>();

            resp.Success = true;
            resp.Message = new List<string> { x.Entity.Id.ToString() };

            return resp;
        }
        
        public  async Task<Publisher> GetPublisherAsync(int PublisherId)
        {
            var res = await AppDbContext.Publisher.FirstOrDefaultAsync(publisher => publisher.Id == PublisherId);
            if (res == null)
            {
                return null;
            }
            return res;
        }
        public async Task DeletePublisher(int PublisherId)
        {
            var result = await AppDbContext.Publisher.FirstOrDefaultAsync(e => e.Id == PublisherId);
            if (result != null)
            {
                AppDbContext.Publisher.Remove(result);
                await AppDbContext.SaveChangesAsync();
            }
        }
        

        public async Task UpdatePublisher(Publisher pub)
        {
            var res = await AppDbContext.Publisher.FirstOrDefaultAsync(p => p.Id == pub.Id);
            if (res != null)
            {
                res.PublisherPassword = pub.PublisherPassword;
                res.PublisherName = pub.PublisherName;
                res.PublisherEmail = pub.PublisherEmail;
                res.PublisherDescription = pub.PublisherDescription;
                await AppDbContext.SaveChangesAsync();
            }
        }
    }
}
