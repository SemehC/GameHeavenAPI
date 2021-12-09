using GameHeavenAPI.Entities;
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
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        public string Name { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        public string WebsiteLink { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        public string FacebookLink { get; set; }
        [StringLength(30, MinimumLength = 3, ErrorMessage = "Maximum 30 characters")]
        public string TwitterLink { get; set; }
    }
}
