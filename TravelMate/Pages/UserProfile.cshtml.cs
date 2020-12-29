using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TravelMate.ModelFolder.ContextFolder;
using TravelMate.ModelFolder.CountryModel;
using TravelMate.ModelFolder.IdentityModel;

namespace TravelMate.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public readonly IWebHostEnvironment _env;
        private readonly AppDbContext _context;

        public UserProfileModel(UserManager<ApplicationUser> userManager,
                                IWebHostEnvironment env,
                                AppDbContext context)
        {
            _userManager = userManager;
            _env = env;
            _context = context;
        }
        [BindProperty]
        [Required]
        [MaxLength(30, ErrorMessage = "USERNAME CANNOT EXCEED 30 LETTERS.")]
        public string Name { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }
        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [BindProperty]
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [BindProperty]
        [Required]
        public int Gender { get; set; }
        [BindProperty]
        [Required]
        public string Country { get; set; }
        [BindProperty]
        public IFormFile Images { get; set; }
        public byte[] ProfileImage { get; set; }
        public List<SelectListItem> Countrydropdownlist { get; set; }
        public void OnGet()
        {
            Countrydropdownlist = GetCountryItems();
            var thisuser = _context.Users.Where(name => name.Name == User.Identity.Name).FirstOrDefault();
            ProfileImage = thisuser.ImageData;
        }
        public List<SelectListItem> GetCountryItems()
        {
            string filepath = Path.Combine(_env.ContentRootPath, "CountryList.json");
            string jsonlist = System.IO.File.ReadAllText(filepath);
            var result = JsonConvert.DeserializeObject<RootCountry>(jsonlist);
            List<SelectListItem> _countrydropdownlist = new List<SelectListItem>();
            foreach (var nation in result.Countries)
            {
                _countrydropdownlist.Add(new SelectListItem { Value = nation.Code.ToString(), Text = nation.Name });
            }
            return _countrydropdownlist;
        }
        public async Task<IActionResult> OnPostUpdate()
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(User.Identity.Name);
                user.Name = Name;
                user.DateOfBirth = DateOfBirth;
                user.PhoneNumber = Mobile;
                user.Email = Email;
                user.Country = Country;
                user.Gender = Gender;
                if(Images != null)
                {
                    if(Images.Length > 0)
                    {
                        using var streamReader = Images.OpenReadStream();
                        using var memoryStream = new MemoryStream();
                        streamReader.CopyTo(memoryStream);
                        byte[] uploadedImage = memoryStream.ToArray();
                        user.ImageData = uploadedImage;
                    }
                }
                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    //Repopulate dropdownlist
                    Countrydropdownlist = GetCountryItems();
                    return Page();
                }
            }
            return Page();
        }
    }
}