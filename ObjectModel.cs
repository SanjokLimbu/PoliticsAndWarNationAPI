using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using PoliticsAndWarAPIAccess.API.Models;


namespace PoliticsAndWarAPIAccess.API.Models
{
    public class ObjectModel
    {
        [JsonProperty("nation_id")]
        public int nation_id { get; set; }
        [JsonProperty("nation")]
        public string nation { get; set; }
        [JsonProperty("alliance_id")]
        public int alliance_id { get; set; }
        [JsonProperty("alliance")]
        public string alliance { get; set; }
        [JsonProperty("soldiers")]
        public int soldiers { get; set; }
        [JsonProperty("tanks")]
        public int tanks { get; set; }
        [JsonProperty("aircraft")]
        public int aircraft { get; set; }
        [JsonProperty("ships")]
        public int ships { get; set; }
    }
}
