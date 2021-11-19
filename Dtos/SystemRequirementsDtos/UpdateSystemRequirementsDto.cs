using GameHeavenAPI.Dtos.CPUDtos;
using GameHeavenAPI.Dtos.DirectXDtos;
using GameHeavenAPI.Dtos.GPUDtos;
using GameHeavenAPI.Dtos.OsDtos;
using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.SystemRequirementsDtos
{
    public class UpdateSystemRequirementsDto
    {
        public int Storage { get; set; }
        public int OsId { get; set; }
        public int GPUId { get; set; }
        public int CPUId { get; set; }
        public int DirectXId { get; set; }
        public int Ram { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
