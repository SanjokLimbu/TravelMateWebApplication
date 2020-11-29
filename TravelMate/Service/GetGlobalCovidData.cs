using Microsoft.EntityFrameworkCore;
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
            CoronaListCountryContext coronaList = new CoronaListCountryContext();
            foreach (var data in countryCovidData.Countries)
            {
                coronaList.Country = data.Country;
                coronaList.TotalConfirmed = data.TotalConfirmed;
                coronaList.NewConfirmed = data.NewConfirmed;
                coronaList.TotalDeaths = data.TotalDeaths;
                coronaList.NewDeaths = data.NewDeaths;
                coronaList.TotalRecovered = data.TotalRecovered;
                coronaList.NewRecovered = data.NewRecovered;
                coronaList.Date = data.Date;
            }
            GlobalCasesContext globalCases = new GlobalCasesContext();
            globalCases.NewConfirmed = globalCovidData.Global.NewConfirmed;
            globalCases.TotalConfirmed = globalCovidData.Global.TotalConfirmed;
            globalCases.NewDeaths = globalCovidData.Global.NewDeaths;
            globalCases.TotalDeaths = globalCovidData.Global.TotalDeaths;
            globalCases.NewRecovered = globalCovidData.Global.NewRecovered;
            globalCases.TotalRecovered = globalCovidData.Global.TotalRecovered;
            await _context.SaveChangesAsync();
        }
    }
}
