using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace GameHeavenAPI.Entities
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        [JsonIgnore]
        public IList<Game> Games { get; set; }
    }
}
