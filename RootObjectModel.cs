using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Implementation;
using PoliticsAndWarAPIAccess.API.Models;
using System.Collections.Generic;

namespace PoliticsAndWarAPIAccess.API.Models
{
    public class RootObjectModel
    {
        [JsonProperty("data")]
        public List<ObjectModel> data { get; set; }
    }
}
