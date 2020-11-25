using Newtonsoft.Json;
using System.Collections.Generic;

namespace TravelMate.ModelFolder.GlobalCoronaModel
{
    public class GlobalDetails
    {
        [JsonProperty("Global")]
        public GlobalCases Global { get; set; }
    }
}
