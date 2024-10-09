using System.ComponentModel.DataAnnotations;

namespace Sklep_internetowy.Models
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
