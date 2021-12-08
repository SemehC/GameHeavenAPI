using GameHeavenAPI.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GameHeavenAPI.Repositories.Payments
{
    public interface IPaymentRepository
    {
        Task<Payment> CreatePaymentAsync(Payment payment);
        Task<Payment> GetPaymentByIdAsync(int id);
        Task<IEnumerable<Payment>> GetPayments();
        Task AddGamesToUser(GamesCart cart);
        Task UpdatePayment(Payment payment);
    }
}
