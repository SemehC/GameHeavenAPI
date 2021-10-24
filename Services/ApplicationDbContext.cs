using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Services
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
       public DbSet<Publisher> publisher { get; set; }
       public DbSet<Developer> developers { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        {
            
        }


    }
}
