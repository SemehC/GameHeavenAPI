using System.Collections.Generic;

namespace GameHeavenAPI.Dtos.PaymentDto
{
    public class CreatePaymentDto
    {
        public string PayerId { get; set; }
        public int Amount { get; set; }
        public List<int> GamesIds { get; set; }
    }
}
