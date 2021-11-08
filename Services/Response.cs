using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameHeavenAPI.Services
{
    public class ServerResponse<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public List<string> Message { get; set; } = null;

       
    }
}
