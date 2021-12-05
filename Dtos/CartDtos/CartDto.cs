using GameHeavenAPI.Dtos.GameDtos;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.CartDtos
{
    public class CartDto : Response
    {
        public int Id { get; set; }
        public IdentityUser User{ get; set; }
        public List<GameDto> Games { get; set; }
    }
}
