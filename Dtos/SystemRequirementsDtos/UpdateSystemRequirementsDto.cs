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
        public UpdateOsDto Os { get; set; }
        public UpdateGPUDto GPU { get; set; }
        public UpdateCPUDto CPU { get; set; }
        public UpdateDirectXDto DirectX { get; set; }
        public int Ram { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
