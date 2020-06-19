using Newtonsoft.Json;
using System.Collections.Generic;

namespace PoliticsAndWarAPIAccess.API.Models
{
    public class RootObjectModel
    {
        [JsonProperty("data")]
        public List<ObjectModel> data { get; set; }
    }
}
