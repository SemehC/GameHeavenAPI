using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos
{
    public record UserDto
    {
        public int Id { get; init; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset JoinDate { get; init; }
        public DateTime Birthday { get; set; }
        public bool IsActive { get; set; }
    }
}
