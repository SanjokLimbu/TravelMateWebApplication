using System.ComponentModel.DataAnnotations;

namespace TravelMate.ModelFolder.RegistrationModel
{
    public class Logins
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Display(Name = "Remember Me")]
        public bool RememberMe { get; set; }
    }
}
