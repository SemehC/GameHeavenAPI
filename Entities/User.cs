using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace GameHeavenAPI.Entities
{
    public class User 
    {
        public IdentityUser UserProperties { get; set; }
        public IList<string> Roles { get; set; }
    }
}
