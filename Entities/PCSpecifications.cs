using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class PCSpecifications
    {
        public int Id { get; set; }
        public MotherBoard MotherBoard { get; set; }
        public Storage Storage { get; set; }
        public Case Case { get; set; }
        public PowerSupply PowerSupply { get; set; }
        public CPU CPU { get; set; }
        public GPU GPU { get; set; }
        public Memory Memory { get; set; }
        public Cooler Cooler { get; set; }
    }
}
