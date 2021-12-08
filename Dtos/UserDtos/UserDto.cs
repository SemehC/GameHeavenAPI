using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameHeavenAPI.Dtos
{
    public class UserDto
    {
        public ApplicationUser UserProperties { get; set; }
        public IList<string> Roles { get; set; }
        public PublisherDto Publisher { get; set; }
        public DeveloperDto Developer { get; set; }
        public List<GameDto> OwnedGames { get; set; }
    }
}
