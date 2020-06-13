using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace PWAPI
{
    public class AllianceModel
    {
        [JsonProperty("id")]
        public int id { get; set; }
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("score")]
        public float score { get; set; }
    }
}
