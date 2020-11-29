using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.ModelFolder.GlobalCoronaModel
{
    public class GlobalCases
    {
        public int Id { get; set; }
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
    }
}
