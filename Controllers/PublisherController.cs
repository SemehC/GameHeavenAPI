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
        private readonly IPublishersRepository repository;

        public PublisherController(IPublishersRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public IEnumerable<GetPublisherDto> GetPublishers()
        {
            var publishers = repository.GetPublishers().ToList();
            var publisherDtos = new List<GetPublisherDto>();
            for (int i = 0; i < publishers.Count; i++)
            {
                publisherDtos.Add(new GetPublisherDto
                {
                    PublisherDescription = publishers[i].PublisherDescription,
                    PublisherEmail = publishers[i].PublisherEmail,
                    PublisherName = publishers[i].PublisherName
                });
            }
            return publisherDtos;
        }


        [HttpPost("new")]
        public async Task<ServerResponse<IEnumerable<IdentityError>>> AddPublisher(CreatePublisherDto dto)
        {
            var r = await repository.CreatePublisher(new Publisher
            {
                PublisherDescription = dto.PublisherDescription,
                PublisherEmail = dto.PublisherEmail,
                PublisherName = dto.PublisherName,
                PublisherPassword = dto.PublisherPassword,

            });
            return r;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPublisherDto>> GetPublisher(int id)
        {
            var publisher = await repository.GetPublisherAsync(id);
            if (publisher is not null)
            {
                return new GetPublisherDto
                {
                    PublisherDescription = publisher.PublisherDescription,
                    PublisherEmail = publisher.PublisherEmail,
                    PublisherName = publisher.PublisherName,
                };
            }
            return NotFound();


        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePublisher(int id)
        {
            var res = await repository.GetPublisherAsync(id);
            if (res is null)
            {
                return NotFound();
            }
            await repository.DeletePublisher(id);
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePublisher(int id, UpdatePublisherDto updatePublisherDto)
        {
            var publisherToUpdate = await repository.GetPublisherAsync(id);
            if (publisherToUpdate is null)
            {
                return NotFound();
            }
            //TODO: Handle password encryption
            if (ModelState.IsValid)
            {
                publisherToUpdate.PublisherName = updatePublisherDto.PublisherName;
                publisherToUpdate.PublisherDescription = updatePublisherDto.PublisherDescription;
                publisherToUpdate.PublisherEmail = updatePublisherDto.PublisherEmail;
                if(updatePublisherDto.PublisherPassword != "")
                    publisherToUpdate.PublisherPassword = updatePublisherDto.PublisherPassword;
            }
            await repository.UpdatePublisher(publisherToUpdate);
            return NoContent();

        }
    }
}
