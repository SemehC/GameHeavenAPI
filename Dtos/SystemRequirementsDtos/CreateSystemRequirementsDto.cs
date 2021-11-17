using System.ComponentModel.DataAnnotations;

namespace GameHeavenAPI.Dtos.SystemRequirementsDtos
{
    public class CreateSystemRequirementsDto
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
