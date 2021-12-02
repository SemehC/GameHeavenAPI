using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public interface IPublishersRepository
    {
        Task<Publisher> CreatePublisherAsync(Publisher pub);
        Task<IList<Publisher>> GetPublishersAsync();
        Task<Publisher> GetPublisherAsync(int id);
        Task DeletePublisherAsync(int id);
        Task UpdatePublisherAsync(Publisher publisher);
        Task<Publisher> GetPublisherByUserAsync(string id);
    }
}