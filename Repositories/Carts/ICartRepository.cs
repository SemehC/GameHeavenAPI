using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.GameCarts
{
    public interface ICartRepository
    {
        Task<GamesCart> CreateCartAsync(GamesCart gamesCart);
        Task<GamesCart> GetCartByUserIdAsync(string id);
        Task UpdateCartAsync(GamesCart cart);
    }
}
