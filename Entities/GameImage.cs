using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Entities
{
    public class GameImage
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public Game Game { get; set; }
        public string Alt { get; set; }

    }
}
