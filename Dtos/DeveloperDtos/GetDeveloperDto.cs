using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.DeveloperDtos
{
    public class GetDeveloperDto
    {
        [Required]
        public string DeveloperName { get; set; }
        [Required]
        public string DeveloperDescription { get; set; }
        [Required]
        public string DeveloperEmail { get; set; }
        
    }
}
