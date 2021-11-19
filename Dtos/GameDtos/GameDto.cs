using GameHeavenAPI.Dtos.DeveloperDtos;
using GameHeavenAPI.Dtos.FranchiseDtos;
using GameHeavenAPI.Dtos.GenreDtos;
using GameHeavenAPI.Dtos.PlatformDtos;
using GameHeavenAPI.Dtos.PublisherDtos;
using GameHeavenAPI.Dtos.StatusDtos;
using GameHeavenAPI.Dtos.SystemRequirementsDtos;
using GameHeavenAPI.Entities;
using System;
using System.Collections.Generic;

namespace GameHeavenAPI.Dtos.GameDtos
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public float Discount { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public StatusDto Status { get; set; }
        public bool Approved { get; set; }
        public string ImagesPath { get; set; }
        public string CoverPath { get; set; }
        public string VideosPath { get; set; }
        public IList<DeveloperDto> Developers { get; set; }
        public PublisherDto Publisher { get; set; }
        public IList<GenreDto> Genres { get; set; }
        public IList<PlatformDto> Platforms { get; set; }
        public FranchiseDto Franchise { get; set; }
        public SystemRequirementsDto RecommendedSystemRequirements { get; set; }
        public SystemRequirementsDto MinimumSystemRequirements { get; set; }

    }
}