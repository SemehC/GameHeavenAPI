using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameHeavenAPI.Entities
{
    public class ApplicationUser : IdentityUser 
    {
        public List<Game> Games { get; set; }
        public string ProfilePicturePath { get; set; }
        public string CoverPath { get; set; }
        public string FacebookLink { get; set; }
        public string InstagramLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
