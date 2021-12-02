using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.Requests
{
    public class UserForgotPasswordRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public string Config { get; set; }
    }
}
