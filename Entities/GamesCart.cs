﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class GamesCart
    {
        public int Id { get; set; }
        public User User { get; set; }
        public IList<Game> Games { get; set; }
    }
}