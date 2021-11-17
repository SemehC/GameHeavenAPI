using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.PlatformDtos
{
    public class CreatePlatformDto
    {
        [Required]
        public string Name { get; set; }
    }
}
