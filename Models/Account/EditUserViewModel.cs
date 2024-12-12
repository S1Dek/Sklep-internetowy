using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Sklep_Internetowy.Models.Account
{
    public class EditUserViewModel
    {
        public string Id { get; set; }
        public User User { get; set; }
        public IEnumerable<string> AssignedRoles { get; set; }
        public IEnumerable<IdentityRole<int>> AllRoles { get; set; }
        public IEnumerable<string> SelectedRoles { get; set; }
    }
}