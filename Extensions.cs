using GameHeavenAPI.Dtos;
using GameHeavenAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI
{
    public static class Extensions
    {
        public static UserDto AsDto(this User user)
        {
            return new UserDto
            {
                Birthday = user.Birthday,
                Email = user.Email,
                FirstName = user.FirstName,
                Id = user.Id,
                IsActive = user.IsActive,
                JoinDate = user.JoinDate,
                LastName = user.LastName,
                UserName = user.UserName
            };
        }
    }
}
