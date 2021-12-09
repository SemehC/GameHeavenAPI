using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.UserDtos
{
    public class UpdateUserDto
    {
        public string UserName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public IFormFile Cover { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
