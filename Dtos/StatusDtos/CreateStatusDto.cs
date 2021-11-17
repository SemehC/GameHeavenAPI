using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.StatusDtos
{
    public class CreateStatusDto
    {
        [Required]
        public string Name { get; set; }
    }
}
