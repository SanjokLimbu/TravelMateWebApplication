using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TravelMate.ModelFolder.RegistrationModel
{
    public class Registration
    {
        [Required]
        [MaxLength(30, ErrorMessage = "USERNAME CANNOT EXCEED 30 LETTERS.")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string DateOfBirth { get; set; }
        [Required]
        [MaxLength(15, ErrorMessage = "USERNAME CANNOT EXCEED 15 LETTERS.")]
        public string UserName { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Mobile { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public string Country { get; set; }
        public List<SelectListItem> CountryDropDownList { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Display(Name = "Confrim Password")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "PASSWORDS DO NOT MATCH.")]
        public string ConfirmPassword { get; set; }
    }
}
