using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Hasło")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Potwierzenie hasła")]
        [Compare("Password", ErrorMessage = "Hasło nie pasuję.")]
        public string ConfirmPassword { get; set; }

        [Required]
        [Display(Name = "Imię")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
    }
}
