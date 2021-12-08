using GameHeavenAPI.Dtos.GameDtos;
using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.CartDtos
{
    public class CartDto : Response
    {
        public int Id { get; set; }
        public ApplicationUser User{ get; set; }
        public List<GameDto> Games { get; set; }
    }
}
