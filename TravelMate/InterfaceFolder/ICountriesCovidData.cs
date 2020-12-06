using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.InterfaceFolder
{
    public interface ICountriesCovidData
    {
        List<CoronaListCountryContext> CovidDataToDisplay();
        List<GlobalCasesContext> GlobalDataToDisplay();
    }
}
