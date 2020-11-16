using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using TravelMate.ModelFolder.IdentityModel;
using TravelMate.ModelFolder.ProfileModel;

namespace TravelMate.Pages
{
    public class UserProfileModel : PageModel
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserProfileModel(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<IActionResult> OnPut(Profile profile)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(profile.Email);
                //if (user != null && (await _signInManager.IsSignedIn(user))
            }
            return Page();
        }
    }
}
