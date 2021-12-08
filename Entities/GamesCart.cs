using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class GamesCart
    {
        public int Id { get; set; }
        public ApplicationUser User { get; set; }
        public IList<Game> Games { get; set; }
    }
}
