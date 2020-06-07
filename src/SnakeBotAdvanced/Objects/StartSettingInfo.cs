using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Objects
{
    public class StartSettingInfo
    {
        [JsonProperty("color")]
        public string Color { get; set; } = "#FF0000";

        [JsonProperty("secondary_color")]
        public string SecondaryColor { get; set; } = "#00FF00";

        [JsonProperty("name")]
        public string Name { get; set; } = "Сообразим на троих";
        
        [JsonProperty("taunt")]
        public static string Taunt = "Why are you gay?";
        
        [JsonProperty("head_type")]
        public static string HeadType = "sand-worm";
        
        [JsonProperty("tail_type")]
        public static string TailType = "fat-rattle";
        
        [JsonProperty("head_url")]
        public static string HeadUrl = "";
    }
}
