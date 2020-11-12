using Newtonsoft.Json;

namespace TravelMate.ModelFolder.CountryModel
{
    public class CountryDetails
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("code")]
        public int Code { get; set; }
    }
}
