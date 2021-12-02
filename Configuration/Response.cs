using System.Collections.Generic;

namespace GameHeavenAPI
{
    public class Response
    {
        public bool Success { get; set; }
        public List<string> Errors { get; set; }
        public IList<string> Messages { get; set; }
    }
}