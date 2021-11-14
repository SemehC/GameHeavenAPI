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
        Task<IEnumerable<Game>> GetGames();
        Task<Game> GetGameByIdAsync(int id);
        Task<IEnumerable<Game>> GetGamesByName(string name);
        Task<Game> CreateGameAsync(Game game);
        Task DeleteGameAsync(int id);
        Task UpdateGame(Game game);
    }
}
