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
    public class DisplayDataModel : PageModel, ICountriesCovidData
    {
        private readonly AppDbContext _Context;
        private List<CoronaListCountryContext> CovidForpage;
        private GlobalCasesContext GlobalForPage;

        public List<CoronaListCountryContext> CovidDataToDisplay()
        {
            throw new NotImplementedException();
        }

        public GlobalCasesContext GlobalDataToDisplay()
        {
            throw new NotImplementedException();
        }

        public DisplayDataModel(AppDbContext context)
        {
            _Context = context;
        }
    }
}
