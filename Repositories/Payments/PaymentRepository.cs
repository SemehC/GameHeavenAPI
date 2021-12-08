using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<ApplicationUser> userManager;

        public PaymentRepository(ApplicationDbContext applicationDbContext, UserManager<ApplicationUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            this.userManager = userManager;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            var paymentDone =  (await _applicationDbContext.Payments.AddAsync(payment)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return paymentDone;

        }
        public async Task AddGamesToUser(GamesCart cart)
        {
            cart.User.Games = (List<Game>)cart.Games;
            await userManager.UpdateAsync(cart.User);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<Payment> GetPaymentByIdAsync(int id)
        {
            return await _applicationDbContext.Payments.Include(payment => payment.Games).Include(payment => payment.Payer).FirstOrDefaultAsync(payment => payment.PaymentId == id);
        }

        public async Task<IEnumerable<Payment>> GetPayments()
        {
            return await _applicationDbContext.Payments.Include(payment => payment.Games).Include(payment => payment.Payer).ToListAsync();
        }

        public async Task UpdatePayment(Payment payment)
        {
            _applicationDbContext.Payments.Update(payment);
            await _applicationDbContext.SaveChangesAsync();
            await Task.CompletedTask;
        }
    }
}
