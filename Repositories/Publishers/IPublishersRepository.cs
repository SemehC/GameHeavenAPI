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

        Task<ServerResponse<IEnumerable<IdentityError>>> CreatePublisher(Publisher pub);
        IEnumerable<Publisher> GetPublishers();
        Task<Publisher> GetPublisherAsync(int id);
        Task DeletePublisher(int id);
        Task UpdatePublisher(Publisher publisher);
    }
}