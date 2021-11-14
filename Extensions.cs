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
        public static GameDto AsDto(this Game game)
        {
            return new GameDto
            {
                Id = game.Id,
                Name = game.Name,
                Approved = game.Approved,
                Description = game.Description,
                Developers = game.Developers,
                Discount = game.Discount,
                Franchise = game.Franchise,
                Images = game.Images,
                Price = game.Price,
                Publisher = game.Publisher,
                MinimumSystemRequirements = game.MinimumSystemRequirements,
                RecommendedSystemRequirements = game.RecommendedSystemRequirements,
                ReleaseDate = game.ReleaseDate,
                Status = game.Status
            };

        }
    }
}
