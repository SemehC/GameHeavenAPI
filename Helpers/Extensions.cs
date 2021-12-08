using GameHeavenAPI.Dtos;
using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Dtos.FranchiseDtos;
using GameHeavenAPI.Dtos.GenreDtos;
using GameHeavenAPI.Dtos.PlatformDtos;
using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Dtos.StatusDtos;
using GameHeavenAPI.Dtos.OsDtos;
using GameHeavenAPI.Dtos.CPUDtos;
using GameHeavenAPI.Dtos.GPUDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GameHeavenAPI.Dtos.DirectXDtos;
using GameHeavenAPI.Dtos.SystemRequirementsDtos;
using GameHeavenAPI.Dtos.CartDtos;
using GameHeavenAPI.Dtos.PaymentDto;

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
                .Include(game => game.Publisher)
                .Include(game => game.MinimumSystemRequirements)
                .Include(game => game.MinimumSystemRequirements.GPU)
                .Include(game => game.MinimumSystemRequirements.CPU)
                .Include(game => game.MinimumSystemRequirements.DirectX)
                .Include(game => game.MinimumSystemRequirements.Os)
                .Include(game => game.RecommendedSystemRequirements)
                .Include(game => game.RecommendedSystemRequirements.GPU)
                .Include(game => game.RecommendedSystemRequirements.CPU)
                .Include(game => game.RecommendedSystemRequirements.DirectX)
                .Include(game => game.RecommendedSystemRequirements.Os)
                .Include(game => game.Genres)
                .Include(game => game.Franchise)
                .Include(game => game.Platforms)
                .Include(game => game.Status);
        }
        public static IQueryable<Developer> CompleteDeveloper(this ApplicationDbContext applicationDbContext)
        {
            return applicationDbContext.Developers.Include(developer => developer.User);
        }    
        public static IQueryable<Publisher> CompletePublisher(this ApplicationDbContext applicationDbContext)
        {
            return applicationDbContext.Publishers.Include(publisher => publisher.User);
        }
        public static DirectXDto AsDto(this DirectXVersion directX)
        {
            return new DirectXDto
            {
                Id = directX.Id,
                Name = directX.Name
            };
        }
        public static SystemRequirementsDto AsDto(this MinimumSystemRequirements minimumSystemRequirements)
        {
            return new SystemRequirementsDto
            {
                CPU = minimumSystemRequirements.CPU?.AsDto(),
                GPU = minimumSystemRequirements.GPU?.AsDto(),
                DirectX = minimumSystemRequirements.DirectX?.AsDto(),
                Os = minimumSystemRequirements.Os?.AsDto(),
                AdditionalNotes = minimumSystemRequirements.AdditionalNotes,
                Ram = minimumSystemRequirements.Ram,
                Storage = minimumSystemRequirements.Storage,
                Id = minimumSystemRequirements.Id
            };
        }
        public static SystemRequirementsDto AsDto(this RecommendedSystemRequirements recommendedSystemRequirements)
        {
            return new SystemRequirementsDto
            {
                CPU = recommendedSystemRequirements.CPU?.AsDto(),
                GPU = recommendedSystemRequirements.GPU?.AsDto(),
                DirectX = recommendedSystemRequirements.DirectX?.AsDto(),
                Os = recommendedSystemRequirements.Os?.AsDto(),
                AdditionalNotes = recommendedSystemRequirements.AdditionalNotes,
                Ram = recommendedSystemRequirements.Ram,
                Storage = recommendedSystemRequirements.Storage,
                Id = recommendedSystemRequirements.Id
            };
        }
        public static OsDto AsDto(this Os os)
        {
            return new OsDto
            {
                Id = os.Id,
                Name = os.Name
            };
        }
        public static CartDto AsDto(this GamesCart gamesCart)
        {
            return new CartDto
            {
                Id = gamesCart.Id,
                Games = gamesCart.Games != null ? gamesCart.Games.Select(game => game.AsDto()).ToList() : new List<GameDto>(),
                User = gamesCart.User
            };
        }
        public static PaymentDto AsDto(this Payment payment)
        {
            return new PaymentDto
            {
                PaymentId = payment.PaymentId,
                Amount = payment.Amount,
                Date = payment.Date,
                Games = payment.Games.Select(game => game.AsDto()).ToList(),
                Payer = payment.Payer,
                Paid = payment.Paid
                
            };
        }
        public static CPUDto AsDto(this CPU cpu)
        {
            return new CPUDto
            {
                Price = cpu.Price,
                Name = cpu.Name,
                Description = cpu.Description,
                Id = cpu.Id,
            };
        }
        public static GPUDto AsDto(this GPU gpu)
        {
            return new GPUDto
            {
                Id = gpu.Id,
                Description = gpu.Description,
                Name = gpu.Name,
                Price = gpu.Price
            };
        }
        public static StatusDto AsDto(this Status status)
        {
            return new StatusDto
            {
                Id = status != null ? status.Id : 0,
                Name = status?.Name
            };
        }
        public static PlatformDto AsDto(this Platform platform)
        {
            return new PlatformDto
            {
                Id = platform.Id,
                Name = platform.Name
            };
        }
        public static GenreDto AsDto(this Genre genre)
        {
            return new GenreDto
            {
                Description = genre.Description,
                Id = genre.Id,
                Name = genre.Name
            };
        }
        public static FranchiseDto AsDto(this Franchise franchise)
        {
            return new FranchiseDto
            {
                Id = franchise.Id,
                Name = franchise.Name,
                CoverPath = franchise.CoverPath,
            };
        }
        public static DeveloperDto AsDto(this Developer developer)
        {
            return new DeveloperDto
            {
                Description = developer.Description,
                Name = developer.Name,
                CoverPath = developer.CoverPath,
                FacebookLink = developer.FacebookLink,
                TwitterLink = developer.TwitterLink,
                WebsiteLink = developer.WebsiteLink,
                User = developer.User,
                Id = developer.Id,
            };
        }
        public static PublisherDto AsDto(this Publisher publisher)
        {
            return new PublisherDto
            {
                Description = publisher?.Description,
                Name = publisher?.Name,
                CoverPath = publisher?.CoverPath,
                FacebookLink = publisher?.FacebookLink,
                TwitterLink = publisher?.TwitterLink,
                WebsiteLink = publisher?.WebsiteLink,
                User = publisher?.User,
                Id = publisher != null ? publisher.Id : 0,
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
                Genres = game.Genres?.Select(genre=> genre.AsDto()).ToList(),
                Developers = game.Developers?.Select(developer => developer.AsDto()).ToList(),
                Discount = game.Discount,
                Franchise = game.Franchise?.AsDto(),
                ImagesPath = game.ImagesPath,
                CoverPath = game.CoverPath,
                VideosPath = game.VideosPath,
                Price = game.Price,
                Publisher = game.Publisher.AsDto(),
                MinimumSystemRequirements = game.MinimumSystemRequirements?.AsDto(),
                RecommendedSystemRequirements = game.RecommendedSystemRequirements?.AsDto(),
                ReleaseDate = game.ReleaseDate,
                Status = game.Status.AsDto(),
                Platforms = game.Platforms?.Select(platform => platform.AsDto()).ToList(),
            };

        }
    }
}
