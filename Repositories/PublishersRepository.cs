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

        public IEnumerable<GetPublisherDto> getPublishers()
        {
            var r = AppDbContext.publisher.AsParallel();

            return r.ToList().Select(x => new GetPublisherDto
            {
                PublisherDescription = x.PublisherDescription,
                PublisherEmail = x.PublisherEmail,
                PublisherName = x.PublisherName,

            });
        }

        public async Task<ServerResponse<IEnumerable<IdentityError>>> createPublisher(Publisher pub)
        {
            var x = await AppDbContext.publisher.AddAsync(pub);
            AppDbContext.SaveChanges();
            var resp = new ServerResponse<IEnumerable<IdentityError>>();

            resp.Success = true;

            return resp;
        }
        
        public  async Task<GetPublisherDto> getPublisher(Guid PublisherId)
        {
            var res = await AppDbContext.publisher.Where(x => x.PublisherId == PublisherId).SingleAsync();
            if (res == null)
            {
                return null;
            }
            return new GetPublisherDto
            {
                PublisherDescription = res.PublisherDescription,
                PublisherEmail = res.PublisherEmail,
                PublisherName = res.PublisherName,

            };
        }
        public async Task<ActionResult<GetPublisherDto>> DeletePublisher(Guid PublisherId)
        {
            var result = await AppDbContext.publisher.FirstOrDefaultAsync(e => e.PublisherId == PublisherId);
            if (result != null)
            {
                AppDbContext.publisher.Remove(result);
                await AppDbContext.SaveChangesAsync();
            }

            return null;
        }
        

        public async Task<ActionResult<Publisher>> updatePublisher(Publisher pub)
        {
            var res = await AppDbContext.publisher.FirstOrDefaultAsync(p => p.PublisherId == pub.PublisherId);
            if (res != null)
            {
                res.PublisherPassword = pub.PublisherPassword;
                res.PublisherName = pub.PublisherName;
                res.PublisherEmail = pub.PublisherEmail;
                res.PublisherDescription = pub.PublisherDescription;
                await AppDbContext.SaveChangesAsync();
                return res;

            }
            return null;
        }
    }
}
