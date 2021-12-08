using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImagesPath { get; set; }
        public string CoverPath { get; set; }
        public string VideosPath { get; set; }
        public double Price { get; set; }
        public float Discount { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public Status Status { get; set; }
        public bool Approved { get; set; }
        public IList<Genre> Genres { get; set; }
        public IList<Developer>? Developers { get; set; }
        public Publisher Publisher { get; set; }
        public IList<Platform> Platforms { get; set; }
        public Franchise? Franchise { get; set; }
        [JsonIgnore]
        public IList<GamesCart> Carts{ get; set; }
        [JsonIgnore]
        public IList<ApplicationUser> Users{ get; set; }
        [JsonIgnore]
        public IList<Payment> Payments { get; set; }
        public RecommendedSystemRequirements? RecommendedSystemRequirements { get; set; }
        public MinimumSystemRequirements? MinimumSystemRequirements { get; set; }
    }
}
