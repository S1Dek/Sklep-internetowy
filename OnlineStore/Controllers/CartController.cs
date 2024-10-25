using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Models;
using OnlineStore.Data;
using Microsoft.AspNetCore.Http;
using OnlineStore.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace OnlineStore.Controllers
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
            var cartItems = GetCartItems(); 
            return View(cartItems); 
        }
        [HttpPost]
        public IActionResult AddToCart(int productId)
        {
            // Pobieranie produktu z banzy
            var product = _context.Products.Find(productId);
            if (product == null)
            {
                return NotFound();
            }

            // sesja koszyk 
            var cart = GetCartItems();
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == productId);

            if (cartItem != null)
            {
                cartItem.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { Product = product, Quantity = 1 });
            }

            SaveCartItems(cart);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int productId)
        {
            var cart = GetCartItems(); 
            var cartItem = cart.FirstOrDefault(c => c.Product.Id == productId);

            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartItems(cart);
            }

            return RedirectToAction("Index");
        }

        private List<CartItem> GetCartItems()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>("Cart");
            if (cart == null)
            {
                cart = new List<CartItem>(); 
                HttpContext.Session.Set("Cart", cart);
            }
            return cart;
        }

        private void SaveCartItems(List<CartItem> cart)
        {
            HttpContext.Session.Set("Cart", cart);
        }
    }
}
