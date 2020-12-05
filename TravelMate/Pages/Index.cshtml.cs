using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.Pages
{
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;

        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public JsonResult OnGetCountryChartData()
        {
            var countryData = _context.CoronaListCountries.ToList();
            return new JsonResult(countryData);
        }
        public JsonResult OnGetGlobalChartData()
        {
            var globalData = _context.GlobalContexts.ToList();
            return new JsonResult(globalData);
        }
    }
}
