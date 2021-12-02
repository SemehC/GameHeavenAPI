using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.DeveloperDtos
{
    public class UpdateDeveloperDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public IFormFile Cover { get; set; }
        public string UserId { get; set; }
        public string WebsiteLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
