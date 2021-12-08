using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories
{
    public interface IGameRepository
    {
        Task<IEnumerable<Game>> GetGamesAsync();
        Task<Game> GetGameByIdAsync(int id);
        Task<IEnumerable<Game>> GetGamesByNameAsync(string name);
        Task<Game> CreateGameAsync(Game game);
        Task DeleteGameAsync(int id);
        Task UpdateGameAsync(Game game);
        Task<IList<Game>> GetUserOwnedGamesAsync(ApplicationUser existingUser);
    }
}
