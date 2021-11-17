using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class RecommendedSystemRequirements
    {
        public int Id { get; set; }
        public int Storage { get; set; }
        public Os Os { get; set; }
        public GPU GPU { get; set; }
        public CPU CPU { get; set; }
        public DirectXVersion DirectX { get; set; }
        public int Ram { get; set; }
        public string AdditionalNotes { get; set; }
    }
}
