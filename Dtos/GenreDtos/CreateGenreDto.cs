using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.GenreDtos
{
    public class CreateGenreDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
    }
}
