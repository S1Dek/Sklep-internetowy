using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Linq;

namespace SklepInternetowy.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Strona g³ówna - wyœwietlanie produktów
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Szczegó³y produktu
        public IActionResult ProductDetails(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
    }
}
