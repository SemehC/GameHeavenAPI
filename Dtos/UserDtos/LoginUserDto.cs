using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.UserDtos
{
    public record LoginUserDto
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
