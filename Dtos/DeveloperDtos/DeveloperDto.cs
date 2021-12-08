using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.DeveloperDtos
{
    public class DeveloperDto : RequestResults
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CoverPath { get; set; }
        public ApplicationUser User { get; set; }
        public string Description { get; set; }
        public string WebsiteLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }

    }
}
