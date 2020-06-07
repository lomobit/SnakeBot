using Newtonsoft.Json;
using SnakeGame.Objects;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Objects
{
    public class MoveGettingInfo
    {
        [JsonProperty("you")]
        public Guid You { get; set; }

        [JsonProperty("width")]
        public int Width { get; set; }

        [JsonProperty("height")]
        public int Height { get; set; }

        [JsonProperty("turn")]
        public int Turn { get; set; }

        [JsonProperty("snakes")]
        public Snake[] Snakes { get; set; }

        [JsonProperty("dead_snakes")]
        public Snake[] Dead_Snakes { get; set; }

        [JsonProperty("food")]
        public List<MyPoint> Food { get; set; }
    }
}
