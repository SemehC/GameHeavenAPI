using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.PublisherDtos
{
    public record GetPublisherDto
    {

        [Required]
        public string PublisherName { get; set; }
        [Required]
        public string PublisherDescription { get; set; }
        [Required]
        public string PublisherEmail { get; set; }
    }
}
