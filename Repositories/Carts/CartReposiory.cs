using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace GameHeavenAPI.Repositories.GameCarts
{
    public class CartReposiory : ICartRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CartReposiory(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<GamesCart> CreateCartAsync(GamesCart gamesCart)
        {
            var cart = (await _applicationDbContext.GamesCarts.AddAsync(gamesCart)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return cart;
        }


        public async Task<GamesCart> GetCartByUserIdAsync(string id)
        {
            return await _applicationDbContext.GamesCarts.Include(cart=>cart.Games).Include(cart=>cart.User)
                    .FirstOrDefaultAsync(cart => cart.User.Id == id);
        }


        public async Task UpdateCartAsync(GamesCart cart)
        {
            var cartToBeUpdated = await _applicationDbContext.GamesCarts.FirstOrDefaultAsync(cartInDb => cartInDb.Id == cart.Id);
            if (cartToBeUpdated is not null)
            {
                cartToBeUpdated.Games = cart.Games;
                _applicationDbContext.GamesCarts.Update(cartToBeUpdated);
                await _applicationDbContext.SaveChangesAsync();
            }
        }
    }
}
