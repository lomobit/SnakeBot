using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using SnakeBotAdvanced.Objects;

namespace SnakeGame.Objects
{
    public class Snake
    {
        [JsonProperty("taunt")]
        public string Taunt { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }
        
        [JsonProperty("health_points")]
        public int HealthPoints { get; set; }

        [JsonProperty("coords")]
        public List<MyPoint> Coords { get; set; }
    }
}
