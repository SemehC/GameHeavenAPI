using GameHeavenAPI.Entities;
using GameHeavenAPI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Payments
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public PaymentRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<Payment> CreatePaymentAsync(Payment payment)
        {
            var paymentDone =  (await _applicationDbContext.Payments.AddAsync(payment)).Entity;
            await _applicationDbContext.SaveChangesAsync();
            return paymentDone;

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
            await Task.CompletedTask;
        }
    }
}
