using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GameHeavenAPI.Entities
{
    public class Payment
    {
        public int PaymentId { get; set; }
        public ApplicationUser Payer { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public  List<Game> Games { get; set; }
        public bool Paid { get;  set; }
    }
}
