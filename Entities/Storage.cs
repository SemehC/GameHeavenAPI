using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class Storage : PcPart
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}
