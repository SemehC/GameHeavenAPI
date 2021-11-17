﻿using GameHeavenAPI.Dtos.StatusDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories.Statuses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace GameHeavenAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IStatusRepository _platoformRepository;

        public StatusController(IStatusRepository StatusRepository)
        {
            _platoformRepository = StatusRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<StatusDto>> GetStatusesAsync()
        {
            return (await _platoformRepository.GetStatusesAsync()).Select(Status=> Status.AsDto()).ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StatusDto>> GetStatusAsync(int id)
        {
            var foundStatus = await _platoformRepository.GetStatusByIdAsync(id);
            if (foundStatus is null)
            {
                return NotFound();
            }
            return foundStatus.AsDto();
        }

        [HttpPost]
        public async Task<ActionResult<StatusDto>> CreateStatusAsync(CreateStatusDto createStatusDto)
        {
            Status createdStatus = new()
            {
                Name = createStatusDto.Name,
            };
            var insertedStatus = await _platoformRepository.CreateStatusAsync(createdStatus);
            return CreatedAtAction(nameof(GetStatusAsync), new { id = insertedStatus.Id }, insertedStatus.AsDto());
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateStatusAsync(int id, UpdateStatusDto updateStatusDto)
        {
            var requestedStatus = await _platoformRepository.GetStatusByIdAsync(id);
            if (requestedStatus is null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                requestedStatus.Name = updateStatusDto.Name;
                await _platoformRepository.UpdateStatusAsync(requestedStatus);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStatusAsync(int id)
        {
            var requestedGame = await _platoformRepository.GetStatusByIdAsync(id);
            if (requestedGame is null)
            {
                return NotFound();
            }
            await _platoformRepository.DeleteStatusAsync(id);
            return NoContent();
        }
    }
}
