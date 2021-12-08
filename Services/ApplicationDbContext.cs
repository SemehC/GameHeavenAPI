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
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<PcPart> PcParts { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<MinimumSystemRequirements> MinimumSystemRequirements { get; set; }
        public DbSet<RecommendedSystemRequirements> RecommendedSystemRequirements { get; set; }
        public DbSet<PCSpecifications> PCSpecifications { get; set; }
        public DbSet<GamesCart> GamesCarts { get; set; }
        public DbSet<PcPartsCart> PcPartsCarts { get; set; }
        public DbSet<PCBuild> PCBuilds { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Os> Oses { get; set; }
        public DbSet<DirectXVersion> DirectXVersions { get; set; }
        public DbSet<CPU> CPUs { get; set; }
        public DbSet<GPU> GPUs { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
    }
}
