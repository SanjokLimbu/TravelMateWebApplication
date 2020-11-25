using Newtonsoft.Json;

namespace TravelMate.ModelFolder.GlobalCoronaModel
{
    public class CoronaListCountry
    {
        [JsonProperty("Countries")]
        public CoronaListCountryDetails Countries { get; set; }
    }
}
