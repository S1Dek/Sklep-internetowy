using Microsoft.AspNetCore.Mvc;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Sklep_Internetowy.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var userId = GetUserId(); // Pobierz ID zalogowanego użytkownika
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            return View(cartItems);
        }

        // Dodawanie produktu do koszyka
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            var userId = GetUserId();

            var cartItem = _context.CartItems
                .FirstOrDefault(c => c.ProductId == productId && c.UserId == userId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cartItem = new CartItem
                {
                    ProductId = productId,
                    UserId = userId,
                    Quantity = 1
                };
                _context.CartItems.Add(cartItem);
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        // Usuwanie produktu z koszyka
        public IActionResult RemoveFromCart(int id)
        {
            var cartItem = _context.CartItems.FirstOrDefault(c => c.Id == id);

            if (cartItem != null)
            {
                _context.CartItems.Remove(cartItem);
                _context.SaveChanges();
            }

            return RedirectToAction("Index");
        }

        // Finalizacja zakupu
        public IActionResult Checkout()
        {
            var userId = GetUserId();
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            return View(cartItems);
        }

        [HttpPost]
        public IActionResult FinalizeOrder()
        {
            var userId = GetUserId();
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            //  nowe zamowienie
            var order = new Order
            {
                UserId = userId,
                OrderDate = DateTime.Now,
                TotalAmount = cartItems.Sum(i => i.Product.Price * i.Quantity),
                OrderDetails = cartItems.Select(c => new OrderDetail
                {
                    ProductId = c.ProductId,
                    Quantity = c.Quantity,
                    Price = c.Product.Price
                }).ToList()
            };

            _context.Orders.Add(order);

            // czyszcenie koszyka
            _context.CartItems.RemoveRange(cartItems);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }

        // Helper: Pobierz ID użytkownika jako int
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }
    }
}
