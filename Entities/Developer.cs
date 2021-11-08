using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class Developer
    {
        public Guid DeveloperId { get; init; }
        public string DeveloperName { get; set; }
        public string DeveloperEmail { get; set; }
        public string   DeveloperDescription { get; set; }
        public string DeveloperPassword { get; set; }
        public DateTimeOffset JoinDate { get; init; }
    }
}
