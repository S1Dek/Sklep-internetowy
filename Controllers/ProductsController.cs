using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Linq;

namespace SklepInternetowy.Controllers
{
    [Authorize(Roles = "Moderator,Admin")] // Tylko moderatorzy i administratorzy mają dostęp do tych akcji
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Wyświetlanie listy produktów
        [AllowAnonymous] // Publiczny dostęp do listy produktów
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Szczegóły produktu
        [AllowAnonymous] // Publiczny dostęp do szczegółów produktu
        public IActionResult Details(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Formularz dodawania nowego produktu
        public IActionResult Create()
        {
            return View();
        }

        // Obsługa dodawania nowego produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Formularz edycji produktu
        public IActionResult Edit(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Obsługa edycji produktu
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // Usuwanie produktu
        public IActionResult Delete(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Potwierdzenie usunięcia produktu
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
