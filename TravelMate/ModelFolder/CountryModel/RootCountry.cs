using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TravelMate.ModelFolder.CountryModel
{
    public class RootCountry
    {
        [JsonProperty("countries")]
        public List<CountryDetails> Countries { get; set; }
    }
}
