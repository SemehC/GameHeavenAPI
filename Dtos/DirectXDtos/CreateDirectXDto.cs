using System.ComponentModel.DataAnnotations;
namespace GameHeavenAPI.Dtos.DirectXDtos
{
    public class CreateDirectXDto
    {
        [Required]
        public string Name { get; set; }
    }
}
