using GameHeavenAPI.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.GameDtos
{
    public class UpdateGameDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public double Price { get; set; }    
        [Required]
        public float Discount { get; set; }
        [Required]
        public DateTime ReleaseDate { get; set; }
        public string Status { get; set; }
    }
}
