using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class User
    {
        public Guid Id { get; init; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTimeOffset JoinDate { get; init; }
        public DateTime Birthday { get; set; }
        public Boolean IsActive { get; set; } = false;

    }
}
