using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Objects
{
    public class MoveSettingInfo
    {
        [JsonProperty("move")]
        public string Move { get; set; }

        [JsonProperty("taunt")]
        public string Taunt { get; set; } = "Why are you gay?";
    }
}
