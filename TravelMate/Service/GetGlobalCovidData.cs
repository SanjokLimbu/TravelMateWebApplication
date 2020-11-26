using Newtonsoft.Json;
using System.Threading.Tasks;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;
using TravelMate.ModelFolder.GlobalCoronaModel;

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
            var globalCovidData = JsonConvert.DeserializeObject<GlobalDetails>(response);
            var countryCovidData = JsonConvert.DeserializeObject<CoronaListCountry>(response);
            foreach (var data in countryCovidData.Countries)
            {
                _context.CoronaListCountries.Update(new CoronaListCountryContext()
                {
                    Country = data.Country,
                    NewConfirmed = data.NewConfirmed,
                    TotalConfirmed = data.TotalConfirmed,
                    NewDeaths = data.NewDeaths,
                    TotalDeaths = data.TotalDeaths,
                    NewRecovered = data.NewRecovered,
                    TotalRecovered = data.TotalRecovered,
                    Date = data.Date
                });
            }
            _context.GlobalContexts.Update(new GlobalCasesContext()
            {
                NewConfirmed = globalCovidData.Global.NewConfirmed,
                TotalConfirmed = globalCovidData.Global.TotalConfirmed,
                NewDeaths = globalCovidData.Global.NewDeaths,
                TotalDeaths = globalCovidData.Global.TotalDeaths,
                NewRecovered = globalCovidData.Global.NewRecovered,
                TotalRecovered = globalCovidData.Global.TotalRecovered
            });
            _context.SaveChanges();
        }
    }
}
