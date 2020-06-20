using Newtonsoft.Json;


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
        [JsonProperty("score")]
        public float score { get; set; }
        [JsonProperty("cities")]
        public int Cities { get; set; }
        [JsonProperty("v_mode")]
        public bool v_mode { get; set; }
        [JsonProperty("alliance_position")]
        public int alliance_position { get; set; }
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
