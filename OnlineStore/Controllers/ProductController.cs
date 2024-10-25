using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data;
using OnlineStore.Models;
using System.Linq;

namespace OnlineStore.Controllers
{
    [Authorize] // Wymaga autoryzacji dla wszystkich akcji w kontrolerze
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Przeglądanie produktów - dostęp dla wszystkich zalogowanych użytkowników
        [AllowAnonymous]
        public IActionResult Index()
        {
            var products = _context.Products.ToList();
            return View(products);
        }

        // Szczegóły produktu
        [AllowAnonymous]
        public IActionResult Details(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // Dodawanie nowego produktu - tylko dla adminów i moderatorów
        [Authorize(Policy = "ModeratorOnly")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorOnly")]
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
        //edit produktow
        [Authorize(Policy = "ModeratorOnly")]
        public IActionResult Edit(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [Authorize(Policy = "ModeratorOnly")]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [Authorize(Policy = "AdminOnly")]
        public IActionResult Delete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                _context.Products.Remove(product);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
