using System.Collections.Generic;

namespace GameHeavenAPI.Dtos
{
    public class RequestResults
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
        public IList<string> Messages { get; set; }
    }
}
