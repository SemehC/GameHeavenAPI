using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class PcPartsCart
    {
        public int Id { get; set; }
        public IList<PcPart> PcParts { get; set; }
    }
}
