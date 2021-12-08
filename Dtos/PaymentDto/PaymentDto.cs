using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace GameHeavenAPI.Dtos.PaymentDto
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public ApplicationUser Payer { get; set; }
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public List<GameDto> Games { get; set; }
        public bool Paid { get; set; }
    }
}
