using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public GameRepository(ApplicationDbContext appDbContext)
        {
            _applicationDbContext = appDbContext;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            var createdGame = (await _applicationDbContext.Games.AddAsync(game)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return createdGame;
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = await _applicationDbContext.CompleteGames()
                .FirstOrDefaultAsync(game => game.Id == id);
            _applicationDbContext.Games.Remove(game);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Game> GetGameByIdAsync(int id)
        {
            return await _applicationDbContext.CompleteGames()
                .FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<IEnumerable<Game>> GetGamesByNameAsync(string name)
        {
            return await _applicationDbContext.CompleteGames()
                .Where(game => game.Name.Contains(name))
                .ToListAsync();
        }

        public async Task<IEnumerable<Game>> GetGamesAsync()
        {
            return await _applicationDbContext.CompleteGames()
                .ToListAsync();
        }

        public async Task UpdateGameAsync(Game game)
        {
            var gameToBeUpdated = await _applicationDbContext.Games.FirstOrDefaultAsync(gameInDb => gameInDb.Id == game.Id);
            if (gameToBeUpdated is not null)
            {
                gameToBeUpdated.Discount = game.Discount;
                gameToBeUpdated.Description = game.Description;
                gameToBeUpdated.Approved = game.Approved;
                gameToBeUpdated.Developers = game.Developers;
                gameToBeUpdated.Publisher = game.Publisher;
                gameToBeUpdated.Name = game.Name;
                gameToBeUpdated.MinimumSystemRequirements = game.MinimumSystemRequirements;
                gameToBeUpdated.RecommendedSystemRequirements = game.RecommendedSystemRequirements;
                gameToBeUpdated.ReleaseDate = game.ReleaseDate;
                gameToBeUpdated.Price = game.Price;
                gameToBeUpdated.Franchise = game.Franchise;
                gameToBeUpdated.ImagesPath = game.ImagesPath;
                gameToBeUpdated.VideosPath = game.VideosPath;
                gameToBeUpdated.CoverPath = game.CoverPath;
                gameToBeUpdated.Status = game.Status;
                _applicationDbContext.Games.Update(gameToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }

        }

        public async Task<IList<Game>> GetUserOwnedGamesAsync(ApplicationUser existingUser)
        {
            return await _applicationDbContext.CompleteGames().Where(game => game.Users.Contains(existingUser)).ToListAsync();
        }
    }
}
