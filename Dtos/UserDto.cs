using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameHeavenAPI.Dtos
{
    public class UserDto
    {
        public User UserProperties { get; set; }
        public IList<string> Roles { get; set; }
        public Publisher Publisher { get; set; }
        public Developer Developer { get; set; }
    }
}
