using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Controllers
{
    [Route("publishers")]
    [ApiController]
    public class PublisherController : ControllerBase
    {
        public IPublishersRepository repository;

        public PublisherController(IPublishersRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<GetPublisherDto> getPublishers()
        {
            return repository.getPublishers();
        }


        [HttpPost("new")]
        public async Task<ServerResponse<IEnumerable< IdentityError>>> addPublisher(CreatePublisherDto dto)
        {
           var r = await repository.createPublisher(new Entities.Publisher
            {
                PublisherId = Guid.NewGuid(),
                PublisherDescription = dto.PublisherDescription,
                PublisherEmail = dto.PublisherEmail,
                PublisherName = dto.PublisherName,
                PublisherPassword = dto.PublisherPassword,

            });
            return r;
        }
        [HttpGet("{id}")]
        public async Task<GetPublisherDto> GetPublisher(Guid PublisherId)
        {

            return await repository.getPublisher(PublisherId);


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<GetPublisherDto>> DeletePublisher(Guid PublisherId)
        {
            var res = await repository.getPublisher(PublisherId);
            if (res == null)
            {
                return null;
            }
            return await repository.DeletePublisher(PublisherId);
        }
        [HttpPut("{id}")]
        public  async Task<ActionResult<Publisher>> updatePublisher( Guid PublisherId , Publisher publisher)
        {
            try
            {
                if (PublisherId != publisher.PublisherId)
                {
                    return BadRequest("Publisher ID mismatch");
                }
                var publisherToUpdate = await repository.getPublisher(PublisherId);
                if(publisherToUpdate == null)
                {
                    return NotFound($"Publisher with ID : {PublisherId} not Found");

                }
                return await repository.updatePublisher(publisher);

            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error Updating Data");
            }
        }
    }
}
