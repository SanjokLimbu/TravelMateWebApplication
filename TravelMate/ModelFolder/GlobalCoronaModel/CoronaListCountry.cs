using Newtonsoft.Json;
using System.Collections.Generic;

namespace TravelMate.ModelFolder.GlobalCoronaModel
{
    public class CoronaListCountry
    {
        [JsonProperty("Countries")]
        public List<CoronaListCountryDetails> Countries { get; set; }
    }
}
