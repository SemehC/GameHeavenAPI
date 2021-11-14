using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.PublisherDtos
{
    public record UpdatePublisherDto
    {
        
        public string PublisherName { get; set; }
        public string PublisherDescription { get; set; }
        public string PublisherEmail { get; set; }
        public string PublisherPassword { get; set; }
    }
}
