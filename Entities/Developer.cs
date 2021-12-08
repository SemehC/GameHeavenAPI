using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class Developer
    {
        public int Id { get; init; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTimeOffset JoinDate { get; init; }
        public ApplicationUser User { get; set; }
        public string CoverPath { get; set; }
        public string WebsiteLink { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
