using GameHeavenAPI.Entities;
using System;
using System.Collections.Generic;

namespace GameHeavenAPI
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public float Discount { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Status { get; set; }
        public bool Approved { get; set; }
        public IList<Developer> Developers { get; set; }
        public Publisher Publisher { get; set; }
        public IList<GameImage> Images { get; set; }
        public Franchise Franchise { get; set; }
        public RecommendedSystemRequirements RecommendedSystemRequirements { get; set; }
        public MinimumSystemRequirements MinimumSystemRequirements { get; set; }
    }
}