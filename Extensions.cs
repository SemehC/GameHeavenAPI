using GameHeavenAPI.Dtos;
using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI
{
    public static class Extensions
    {
        /// <summary>
        /// Includes all tables that the game object depend on. This method elegantly avoids to repeatedly typing .Include()
        /// method in repository methods
        /// </summary>
        /// <param name="applicationDbContext"></param>
        /// <returns></returns>
        public static IQueryable<Game> CompleteGames(this ApplicationDbContext applicationDbContext)
        {
            return applicationDbContext.Games.Include(game => game.Developers)
                .Include(game => game.Publisher);
        }
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
        public static DeveloperDto AsDto(this Developer developer)
        {
            return new DeveloperDto
            {
                DeveloperDescription = developer.DeveloperDescription,
                DeveloperEmail = developer.DeveloperEmail,
                DeveloperName = developer.DeveloperName
            };
        }
        public static PublisherDto AsDto(this Publisher publisher)
        {
            return new PublisherDto
            {
               PublisherDescription = publisher.PublisherDescription,
               PublisherEmail = publisher.PublisherEmail,
               PublisherName = publisher.PublisherName
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
                Developers = game.Developers?.Select(developer => developer.AsDto()).ToList(),
                Discount = game.Discount,
                Franchise = game.Franchise,
                Images = game.Images,
                Price = game.Price,
                Publisher = game.Publisher.AsDto(),
                MinimumSystemRequirements = game.MinimumSystemRequirements,
                RecommendedSystemRequirements = game.RecommendedSystemRequirements,
                ReleaseDate = game.ReleaseDate,
                Status = game.Status
            };

        }
    }
}
