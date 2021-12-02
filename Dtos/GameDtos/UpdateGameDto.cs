using GameHeavenAPI.Dtos.StatusDtos;
using GameHeavenAPI.Dtos.SystemRequirementsDtos;
using GameHeavenAPI.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Dtos.GameDtos
{
    public class UpdateGameDto
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public double? Price { get; set; }
        public float? Discount { get; set; }
        public DateTime? ReleaseDate { get; set; }
        public int? PublisherId { get; set; }
        public int? StatusId { get; set; }
        public IFormFile Cover { get; set; }
        public IList<IFormFile>? Images { get; set; }
        public IList<IFormFile> Videos { get; set; }
        public IList<int>? PlatformIds { get; set; }
        public IList<int>? GenresIds { get; set; }
        public int? FranchiseId { get; set; }
        public UpdateSystemRequirementsDto? UpdatedRecommendedSystemRequirements { get; set; }
        public UpdateSystemRequirementsDto? UpdatedMinimumSystemRequirements { get; set; }
        public IList<int>? DeveloperIds { get; set; }
    }
}
