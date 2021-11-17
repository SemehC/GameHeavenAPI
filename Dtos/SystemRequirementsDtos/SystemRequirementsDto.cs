using GameHeavenAPI.Dtos.CPUDtos;
using GameHeavenAPI.Dtos.DirectXDtos;
using GameHeavenAPI.Dtos.GPUDtos;
using GameHeavenAPI.Dtos.OsDtos;

namespace GameHeavenAPI.Dtos.SystemRequirementsDtos
{
    public class SystemRequirementsDto
    {
        public int Id { get; set; }
        public int Storage { get; set; }
        public OsDto Os { get; set; }
        public GPUDto GPU { get; set; }
        public CPUDto CPU { get; set; }
        public DirectXDto DirectX { get; set; }
        public int Ram { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
