using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using TravelMate.InterfaceFolder;
using TravelMate.ModelFolder.CountryModel;
using TravelMate.ModelFolder.IdentityModel;
using TravelMate.ModelFolder.RegistrationModel;

namespace TravelMate.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMailService _mailService;
        private readonly IWebHostEnvironment _env;
        public AccountController(UserManager<ApplicationUser> userManager,
                                SignInManager<ApplicationUser> signInManager,
                                IMailService mailService,
                                IWebHostEnvironment env)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mailService = mailService;
            _env = env;
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
        /// <summary>
        /// Sets default controller route to index page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///    This Action return Register View once clicked on Register button
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Register()
        {
            var model = new Registration
            {
                CountryDropDownList = GetCountryItems()
            };
            return View(model);
        }
        /// <summary>
        /// This Action return Login View once clicked on Login button
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Login()
        {
            return View("Login");
        }
        /// <summary>
        /// This Action registers users if all the required field is valid. An email is sent for confirmation.
        /// </summary>
        /// <param name="registration"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Register")]
        public async Task<IActionResult> RegisterUser(Registration registration)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(registration.Email);
                if(user != null)
                {
                    ViewBag.ErrorMessage = $"There is already a {user} associated with this Email.";
                    return View("Error");
                }
                var nameOfUser = await _userManager.FindByNameAsync(registration.UserName);
                if(nameOfUser != null)
                {
                    ViewBag.ErrorMessage = $"The User name {nameOfUser} is already Taken.";
                    return View("Error");
                }
                var newUser = new ApplicationUser
                {
                    Name = registration.Name,
                    DateOfBirth = registration.DateOfBirth,
                    PhoneNumber = registration.Mobile,
                    Email = registration.Email,
                    Country = registration.Country,
                    Gender = registration.Gender,
                    UserName = registration.UserName
                };
                var newApplicationUser = await _userManager.CreateAsync(newUser, registration.Password);
                if (newApplicationUser.Succeeded)
                {
                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(newUser);
                    var confirmationLink = Url.Action("ConfirmEmail", "Account", new { email = newUser.Id, token }, Request.Scheme);
                    await _mailService.SendEmailAsync(registration.Email, "Account Confirmation", confirmationLink);
                    return View("Login");
                }
                foreach (var error in newApplicationUser.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return View("Register");
        }
        /// <summary>
        /// If the user is successfully registered, an Email is sent to confirm user for validation
        /// returns error if either userid or token is invalid
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if(userId == null || token == null)
            {
                return View("Register");
            }
            var newApplicationUser = await _userManager.FindByIdAsync(userId);
            if(userId == null)
            {
                ViewBag.ErrorMessage = $"The user ID {userId} is Invalid";
                return View("Error");
            }
            var result = await _userManager.ConfirmEmailAsync(newApplicationUser, token);
            if (result.Succeeded)
            {
                return View("Login");
            }
            ViewBag.ErrorMessage = "Email canot be confirmed";
            return View("Error");
        }
        /// <summary>
        /// Logs in user if fulfills required parameters to either index page or last viewed page before login.
        /// </summary>
        /// <param name="Login"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        [ActionName("Login")]
        public async Task<IActionResult> LoginUser(Logins Login, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByNameAsync(Login.UserName);
                if(user != null && !user.EmailConfirmed
                    && (await _userManager.CheckPasswordAsync(user, Login.Password)))
                {
                    ModelState.AddModelError(string.Empty, "Email not Confirmed yet.");
                    return View("Login");
                }
                var loginUser = await _signInManager.PasswordSignInAsync(Login.UserName, Login.Password, Login.RememberMe, false);
                if (loginUser.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl))
                    {
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToPage("/Index");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid Login attempt.");
            }
            return View("Login");
        }
        [HttpPost]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View("ForgotPassword");
        }
        /// <summary>
        /// Send same error message regardless if the email is registered or not
        /// </summary>
        /// <param name="forgotPasswordModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordModel forgotPasswordModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordModel.Email);
                if(user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                    var forgotPasswordLink = Url.Action("ResetPassword", "Account", new { email = user.Email, token }, Request.Scheme);
                    await _mailService.SendEmailAsync(forgotPasswordModel.Email, "Reset Password", forgotPasswordLink);
                }
                ViewBag.ErrorMessage = "The reset password link has been sent to the email provided. Please check your Inbox.";
                return View("Error");
            }
            ViewBag.ErrorMessage = "The reset password link has been sent to the email you provided. Please check your Inbox.";
            return View("Error");
        }
        [HttpGet]
        public IActionResult ResetPassword(string token, string email)
        {
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View("ResetPassword");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="resetPassword"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPassword.Email);
                if(user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, resetPassword.Token, resetPassword.Password);
                    if (result.Succeeded)
                    {
                        ViewBag.ErrorMessage = "Password has been reset.";
                        return View("Error");
                    }
                    foreach(var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                    return View(resetPassword);
                }
            }
            return View(resetPassword);
        }
    }
}

