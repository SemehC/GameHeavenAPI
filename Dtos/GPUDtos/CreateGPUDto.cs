using System.ComponentModel.DataAnnotations;
namespace GameHeavenAPI.Dtos.GPUDtos
{
    public class CreateGPUDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
