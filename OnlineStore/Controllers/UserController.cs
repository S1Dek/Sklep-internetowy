using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Linq;

namespace OnlineStore.Controllers
{
    [Authorize(Policy = "AdminOnly")]
    public class UserController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }

        // Edir roli
        public IActionResult EditRole(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = new[] { "Admin", "Moderator", "User" }; 
            ViewBag.Roles = roles;
            ViewBag.CurrentRole = user.Role as string; 

            return View(user);
        }

        [HttpPost]
        public IActionResult EditRole(int id, string role)
        {
            var user = _context.Users.Find(id);
            if (user != null)
            {
                user.Role = role; // Zmiana roli
                _context.SaveChanges();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
