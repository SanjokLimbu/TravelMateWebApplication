using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using TravelMate.ModelFolder.ContextFolder;

namespace TravelMate.Pages
{
    [AllowAnonymous]
    public class IndexModel : PageModel
    {
        private readonly AppDbContext _context;
        public List<SelectListItem> Country { get; set; }
        public IndexModel(AppDbContext context)
        {
            _context = context;
        }
        public void OnGet()
        {
            Country = GetCountry();
        }
        public List<SelectListItem> GetCountry()
        {
            var Country = _context.CoronaListCountries.ToList();
            List<SelectListItem> _Country = new List<SelectListItem>();
            foreach (var country in Country)
            {
                _Country.Add(new SelectListItem { Value = country.Id.ToString(), Text = country.Country });
            }
            return _Country;
        }
        public JsonResult OnGetGlobalChartData()
        {
            var globalData = _context.GlobalContexts.ToList();
            return new JsonResult(globalData);
        }
        public JsonResult OnGetSearch(int id)
        {
            var selectedCountry = _context.CoronaListCountries.Where(S => S.Id == id).ToList();
            return new JsonResult(selectedCountry);
        }
    }
}
