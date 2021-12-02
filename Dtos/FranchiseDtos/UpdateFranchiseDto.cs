using Microsoft.AspNetCore.Http;

namespace GameHeavenAPI.Dtos.FranchiseDtos
{
    public class UpdateFranchiseDto
    {
        public string Name { get; set; }
        public IFormFile? Cover { get; set; }
    }
}
