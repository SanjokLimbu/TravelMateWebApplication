using System.Linq;
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
            if(response != null)
            {
                var globalCovidData = JsonConvert.DeserializeObject<GlobalDetails>(response);
                var countryCovidData = JsonConvert.DeserializeObject<CoronaListCountry>(response);
                foreach (var data in countryCovidData.Countries)
                {
                    var countryQuery = _context.CoronaListCountries.Select(rows => rows); // Selects all rows from table
                    foreach (var query in countryQuery)
                    {
                        query.Country = data.Country;
                        query.NewConfirmed = data.NewConfirmed;
                        query.TotalConfirmed = data.TotalConfirmed;
                        query.NewDeaths = data.NewDeaths;
                        query.TotalDeaths = data.TotalDeaths;
                        query.NewRecovered = data.NewRecovered;
                        query.TotalRecovered = data.TotalRecovered;
                        query.Date = data.Date;
                    }
                    _context.Entry(countryQuery).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    _context.Update(countryQuery);
                }
                _context.SaveChanges();
            }
        }
    }
}
