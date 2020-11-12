using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace TravelMate.ModelFolder.IdentityModel
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string DateOfBirth { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public int Gender { get; set; }
    }
}
