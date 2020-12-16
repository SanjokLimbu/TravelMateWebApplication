using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.Pages.Shared
{
    public class DisplayDataModel : PageModel
    {
        private readonly AppDbContext _Context;
        public List<CoronaListCountryContext> CovidDataForpage;
        public List<GlobalCasesContext> GlobalDataForPage;
        public List<CoronaListCountryContext> CovidDataToDisplay()
        {
            var countryCovidData = _Context.CoronaListCountries.ToList();
            return countryCovidData;
        }
        public List<GlobalCasesContext> GlobalDataToDisplay()
        {
            var globalCovidData = _Context.GlobalContexts.ToList();
            return globalCovidData;
        }
        public DisplayDataModel(AppDbContext context)
        {
            _Context = context;
        }
        public void OnGet()
        {
            CovidDataForpage = CovidDataToDisplay();
            GlobalDataForPage = GlobalDataToDisplay();
        }
    }
}
