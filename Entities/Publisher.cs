﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class Publisher
    {
        public Guid PublisherId { get; init; }
        public string PublisherName { get; set;  }
        public string PublisherEmail { get; set; }
        public string PublisherDescription { get; set; }
        public string PublisherPassword { get; set; }
        public DateTimeOffset JoinDate { get; init; }
        public String UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
