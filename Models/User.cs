using Microsoft.AspNetCore.Identity;

namespace Sklep_Internetowy.Models
{
    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
    }
}
