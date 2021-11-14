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
        public IEnumerable<PublisherDto> GetPublishers()
        {
            return repository.GetPublishers().Select(publisher=>publisher.AsDto()).ToList();
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
        public async Task<ActionResult<PublisherDto>> GetPublisher(int id)
        {
            var publisher = await repository.GetPublisherAsync(id);
            if (publisher is not null)
            {
                return publisher.AsDto();
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
