using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Sklep_Internetowy.Models
{
    public class User : IdentityUser<int>
    {
        [Display(Name = "Imię")]
        public string FirstName { get; set; }
        [Display(Name = "Nazwisko")]
        public string LastName { get; set; }
        [Display(Name = "Rola")]
        public string Role { get; set; } = "user";
    }
}
