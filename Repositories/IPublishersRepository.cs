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

        Task<ServerResponse<IEnumerable<IdentityError>>> createPublisher(Publisher pub);
        IEnumerable<GetPublisherDto> getPublishers();
        Task<GetPublisherDto> getPublisher(Guid PublisherId);
        Task<ActionResult<GetPublisherDto>> DeletePublisher(Guid PublisherId);
        Task<ActionResult<Publisher>> updatePublisher(Publisher publisher);
    }
}