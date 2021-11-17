using Microsoft.AspNetCore.Http;

namespace GameHeavenAPI.Dtos.FranchiseDtos
{
    public class CreateFranchiseDto
    {
        public string Name { get; set; }
        public IFormFile Cover { get; set; }
    }
}
