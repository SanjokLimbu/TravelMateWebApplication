using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;
using TravelMate.ModelFolder.GlobalCoronaModel;
using System;
using Microsoft.EntityFrameworkCore;

namespace TravelMate.Service
{
    public class GetGlobalCovidData : IGetGlobalCovidData
    {
        private readonly AppDbContext _context;
        public GetGlobalCovidData(AppDbContext context)
        {
            _context = context;
        }
        public async Task GetData()
        {
            string dataUrl = "https://api.covid19api.com/summary";
            string response = await ApiInitialization.GetClient.GetStringAsync(dataUrl);
            if (response != null)
            {
                var settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore
                };
                var globalCovidData = JsonConvert.DeserializeObject<GlobalDetails>(response, settings);
                var countryCovidData = JsonConvert.DeserializeObject<CoronaListCountry>(response, settings);
                var countryQuery = _context.CoronaListCountries.First(); // retrieve entity
                if(countryCovidData != null)
                {
                    foreach (var data in countryCovidData.Countries)
                    {
                        countryQuery.Country = data.Country;
                        countryQuery.NewConfirmed = data.NewConfirmed;
                        countryQuery.TotalConfirmed = data.TotalConfirmed;
                        countryQuery.NewDeaths = data.NewDeaths;
                        countryQuery.TotalDeaths = data.TotalDeaths;
                        countryQuery.NewRecovered = data.NewRecovered;
                        countryQuery.TotalRecovered = data.TotalRecovered;
                        countryQuery.Date = data.Date;
                    }
                    _context.Entry(countryQuery).State = EntityState.Unchanged;
                    _context.Update(countryQuery);
                }
                if( globalCovidData != null)
                {
                    var globalQuery = _context.GlobalContexts.First();
                    globalQuery.NewConfirmed = globalCovidData.Global.NewConfirmed;
                    globalQuery.TotalConfirmed = globalCovidData.Global.TotalConfirmed;
                    globalQuery.NewDeaths = globalCovidData.Global.NewDeaths;
                    globalQuery.TotalDeaths = globalCovidData.Global.TotalDeaths;
                    globalQuery.NewRecovered = globalCovidData.Global.NewRecovered;
                    globalQuery.TotalRecovered = globalCovidData.Global.TotalRecovered;
                    _context.Entry(globalQuery).State = EntityState.Modified;
                }
                _context.SaveChanges();
            }
        }
    }
}
