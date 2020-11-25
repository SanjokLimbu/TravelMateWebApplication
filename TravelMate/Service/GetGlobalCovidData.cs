using Newtonsoft.Json;
using System.Threading.Tasks;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;
using TravelMate.ModelFolder.GlobalCoronaModel;

namespace TravelMate.Service
{
    public class GetGlobalCovidData
    {
        private AppDbContext _context;
        public GetGlobalCovidData(AppDbContext context)
        {
            _context = context;
        }
        //Empty Constructor to instantiate class in TimedHostedServices
        public GetGlobalCovidData()
        {
                
        }
        public async Task GetData()
        {
            string dataUrl = "https://api.covid19api.com/summary";
            string response = await ApiInitialization.GetClient.GetStringAsync(dataUrl);
            var globalCovidData = JsonConvert.DeserializeObject<GlobalDetails>(response);
            _context.GlobalContexts.Add(new GlobalCasesContext()
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
