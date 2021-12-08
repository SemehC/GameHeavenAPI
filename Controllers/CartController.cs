using GameHeavenAPI.Dtos.CartDtos;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Repositories.GameCarts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GameHeavenAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartRepository cartRepository;
        private readonly IGameRepository gameRepository;
        private readonly UserManager<ApplicationUser> userManager;

        public CartController(ICartRepository cartRepository, IGameRepository gameRepository, Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
        {
            this.cartRepository = cartRepository;
            this.gameRepository = gameRepository;
            this.userManager = userManager;
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(id);
            if (cart == null)
            {
                var gamesCart = new GamesCart
                {
                    User = await userManager.FindByIdAsync(id),
                    Games = new List<Game>()
                };
                cart = await cartRepository.CreateCartAsync(gamesCart);
               
            }
            return Ok(cart);
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<IActionResult> Post(UpdateCartDto addToCartDto)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(addToCartDto.UserId);
            if (cart == null)
            {
                var gamesCart = new GamesCart
                {
                    User = await userManager.FindByIdAsync(addToCartDto.UserId),
                    Games = new List<Game>()
                };
                cart = await cartRepository.CreateCartAsync(gamesCart);
            }
            var game = await gameRepository.GetGameByIdAsync(addToCartDto.GameId);
            if (cart.Games.Contains(game))
            {
                var oldCart = cart.AsDto();
                oldCart.Errors = new List<string>
                {
                    "Game is already added to cart"
                };
                oldCart.Success = false;
                return Ok(oldCart);
            }
            cart.Games.Add(game);
            await cartRepository.UpdateCartAsync(cart);
            var cartDto = cart.AsDto();
            cartDto.Messages = new List<string>
                {
                    "Game added to cart successfully"
                };
            cartDto.Success = true;
            return Ok(cartDto);
        }
        

        // DELETE api/<CartController>/5
        [HttpPost]
        [Route("RemoveFromCart")]
        public async Task<IActionResult> Delete(UpdateCartDto removeFromCartDto)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(removeFromCartDto.UserId);
            var game = await gameRepository.GetGameByIdAsync(removeFromCartDto.GameId);
            cart.Games.Remove(game);
            await cartRepository.UpdateCartAsync(cart);
            var cartDto = cart.AsDto();
            cartDto.Messages = new List<string>
                {
                    "Game removed from cart successfully"
                };
            cartDto.Success = true;
            return Ok(cartDto);
        }
    }
}
