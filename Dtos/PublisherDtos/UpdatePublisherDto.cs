using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.PublisherDtos
{
    public record UpdatePublisherDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
        public string WebsiteLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
