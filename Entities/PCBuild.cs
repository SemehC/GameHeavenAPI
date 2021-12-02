using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class PCBuild
    {
        public int Id { get; set; }
        public PCSpecifications PCSpecifications { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
