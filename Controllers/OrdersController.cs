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

        // ID użytkownika jako int
        private int GetUserId()
        {
            return int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0");
        }

        public IActionResult Index()
        {
            var userId = GetUserId();
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            IQueryable<Order> orders;

            if (userRoles.Contains("admin") || userRoles.Contains("moderator"))
            {
                orders = _context.Orders.Include(o => o.OrderDetails).ThenInclude(od => od.Product);
            }
            else
            {
                orders = _context.Orders
                    .Where(o => o.UserId == userId)
                    .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product);
                Console.WriteLine("DUPA");
            }

            return View(orders.ToList());
        }

        [HttpPost]
        public IActionResult Checkout()
        {
            var userId = GetUserId(); 

            // pobieranie koszyka 
            var cartItems = _context.CartItems
                .Where(c => c.UserId == userId)
                .Include(c => c.Product)
                .ToList();

            if (!cartItems.Any())
            {
                return RedirectToAction("Cart");
            }

            var order = new Order
            {
                UserId = userId,
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

            _context.CartItems.RemoveRange(cartItems);

            _context.SaveChanges();

            return RedirectToAction("OrderConfirmation", new { id = order.Id });
        }

        public IActionResult Details(int id)
        {
            var userId = GetUserId();
            var userRoles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value);

            var query = _context.Orders
                .Where(o => o.Id == id)
                .Include(o => o.OrderDetails)
                .ThenInclude(d => d.Product);

            if (!(userRoles.Contains("admin") || userRoles.Contains("moderator")))
            {
                query = (Microsoft.EntityFrameworkCore.Query.IIncludableQueryable<Order, Product>)query.Where(o => o.UserId == userId);
            }

            var orderResult = query.FirstOrDefault();

            if (orderResult == null)
            {
                return NotFound();
            }

            return View(orderResult);
        }
    }
}
