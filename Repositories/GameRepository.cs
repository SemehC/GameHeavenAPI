using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public class GameRepository : IGameRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public GameRepository(ApplicationDbContext appDbContext)
        {
            this._applicationDbContext = appDbContext;
        }

        public async Task<Game> CreateGameAsync(Game game)
        {
            return (await _applicationDbContext.Games.AddAsync(game)).Entity;
        }

        public async Task DeleteGameAsync(int id)
        {
            var game = _applicationDbContext.Games.Where(game => game.Id == id).FirstOrDefault();
            if(game is not null)
            {
                _applicationDbContext.Remove(game);
                await _applicationDbContext.SaveChangesAsync();
            }
            await Task.FromException(new NullReferenceException());
        }

        public Task DeleteGameById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Game> GetGameByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGamesByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Game>> GetGames()
        {
            throw new NotImplementedException();
        }

        public Task UpdateGame(Game game)
        {
            throw new NotImplementedException();
        }
    }
}
