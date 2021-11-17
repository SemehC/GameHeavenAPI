using System.ComponentModel.DataAnnotations;
namespace GameHeavenAPI.Dtos.OsDtos
{
    public class CreateOsDto
    {
        [Required]
        public string Name { get; set; }
    }
}
