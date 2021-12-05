using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.CartDtos
{
    public class UpdateCartDto
    {
        [Required]
        public string UserId { get; set; }
        [Required]
        public int GameId { get; set; }
    }
}
