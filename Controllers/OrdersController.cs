using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Linq;
using System.Security.Claims;

namespace SklepInternetowy.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrdersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Helper: Pobierz ID użytkownika jako int
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        // Zakładka koszyka użytkownika
        public IActionResult Cart()
        {
            var userId = GetUserId(); // Pobierz ID zalogowanego użytkownika jako int

            // Pobierz elementy koszyka użytkownika
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId) // Porównanie int do int
                .ToList();

            return View(cartItems);
        }

        // Dodanie produktu do koszyka
        [HttpPost]
        public IActionResult AddToCart(int productId, int quantity = 1)
        {
            var userId = GetUserId(); // Pobierz ID zalogowanego użytkownika jako int

            var product = _context.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
            {
                return NotFound();
            }

            // Znajdź element koszyka użytkownika lub dodaj nowy
            var cartItem = _context.CartItems.FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);
            if (cartItem == null)
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    UserId = userId, // UserId jako int
                    Quantity = quantity
                };
                _context.CartItems.Add(cartItem);
            }
            else
            {
                cartItem.Quantity += quantity;
            }

            _context.SaveChanges();
            return RedirectToAction("Cart");
        }

        // Usunięcie produktu z koszyka
        [HttpPost]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == cartItemId);
            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }
            return RedirectToAction("Cart");
        }

        // Finalizacja zamówienia
        [HttpPost]
        public IActionResult Checkout()
        {
            var userId = GetUserId(); // Pobierz ID zalogowanego użytkownika jako int

            // Pobierz elementy koszyka użytkownika
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId) // Porównanie int do int
                .Include(c => c.Product)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Cart");
            }

            // Stwórz nowe zamówienie
            var order = new Order
            {
                UserId = userId, // Przypisz UserId jako int
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(c => c.Quantity * c.Product.Price),
                OrderDetails = cartItems.Select(c => new OrderDetail
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);
            _context.CartItems.RemoveRange(cartItems); // Opróżnij koszyk
            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = order.Id });
        }

        // Potwierdzenie zamówienia
        public IActionResult OrderConfirmation(int id)
        {
            var order = _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product)
                .FirstOrDefault();

            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
