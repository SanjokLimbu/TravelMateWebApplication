using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace TravelMate.ModelFolder.GlobalCoronaModel
{
    public class CoronaListCountryDetails
    {
        [Key]
        public int Id { get; set; }
        [JsonProperty("Country")]
        public string Country { get; set; }
        [JsonProperty("NewConfirmed")]
        public int NewConfirmed { get; set; }
        [JsonProperty("TotalConfirmed")]
        public int TotalConfirmed { get; set; }
        [JsonProperty("NewDeaths")]
        public int NewDeaths { get; set; }
        [JsonProperty("TotalDeaths")]
        public int TotalDeaths { get; set; }
        [JsonProperty("NewRecovered")]
        public int NewRecovered { get; set; }
        [JsonProperty("TotalRecovered")]
        public int TotalRecovered { get; set; }
        [JsonProperty("Date")]
        public string Date { get; set; }
    }
}
