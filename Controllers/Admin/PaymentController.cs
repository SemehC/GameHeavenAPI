using GameHeavenAPI.Dtos.PaymentDto;
using GameHeavenAPI.Entities;
using GameHeavenAPI.Repositories;
using GameHeavenAPI.Repositories.GameCarts;
using GameHeavenAPI.Repositories.Payments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Controllers
{
    [Route("admin/[controller]")]
    [ApiController]

    public class PaymentController : ControllerBase
    {
        private readonly IPaymentRepository paymentRepository;
        private readonly ICartRepository cartRepository;

        public PaymentController(ICartRepository cartRepository, IPaymentRepository paymentRepository)
        {
            this.cartRepository = cartRepository;
            this.paymentRepository = paymentRepository;
        }
        [HttpGet]
        public async Task<IEnumerable<PaymentDto>> GetGamesAsync()
        {
            return (await paymentRepository.GetPayments()).Select(payment => payment.AsDto()).ToList();
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment(CreatePaymentDto createPaymentDto)
        {
            var cart = await cartRepository.GetCartByUserIdAsync(createPaymentDto.PayerId);
            Payment payment = new()
            {
                Amount = createPaymentDto.Amount,
                Date = System.DateTime.Now,
                Games = (List<Game>)cart.Games,
                Payer = cart.User,
                Paid = false
            };
            var insertedPayment = await paymentRepository.CreatePaymentAsync(payment);
            var response = new
            {
                PaymentId = insertedPayment.PaymentId,
                GameIds = insertedPayment.Games.Select(game => game.Id).ToArray()
            };
            return Ok(response);
        }

        [HttpPost]
        [Route("PaymentDone")]
        public async Task<IActionResult> PaymentDone(PaymentDto paymentDto)
        {
            var paymentId = paymentDto.PaymentId;
            var payment = await paymentRepository.GetPaymentByIdAsync(paymentId);
            if (payment is not null)
            {
                GamesCart cart = await cartRepository.GetCartByUserIdAsync(payment.Payer.Id);
                await paymentRepository.AddGamesToUser(cart);
                cart.Games = new List<Game>();
                payment.Paid = true;
                await cartRepository.UpdateCartAsync(cart);
                await paymentRepository.UpdatePayment(payment);
                return Ok(new Response
                {
                    Success = true,
                    Messages = new List<string>()
                {
                    "Payment Done",
                }
                });
            }
            return BadRequest(new Response
            {
                Success = false,
                Errors = new List<string>()
                {
                    "Payment Not Found",
                }
            });
        }
    }
}
