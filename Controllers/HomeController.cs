using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Linq;

namespace Sklep_Internetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var products = _context.Products.ToList(); // pobieranie prodfuktu z bazy 
            return View(products);
        }
    }
}
