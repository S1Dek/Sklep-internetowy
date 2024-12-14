using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sklep_Internetowy.Data;
using Sklep_Internetowy.Models;
using System.Linq;

namespace Sklep_Internetowy.Controllers
{
    [Authorize(Roles = "admin")]
    public class OrderStatisticsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public OrderStatisticsController(ApplicationDbContext context)
        {
            _context = context;
        }
        private async Task<OrderStatisticsViewModel> GetOrderStatisticsAsync()
        {
            return new OrderStatisticsViewModel
            {
                TotalOrders = await _context.Orders.CountAsync(),
                TotalRevenue = (decimal)await _context.Orders.SumAsync(o => (double)o.TotalAmount),
                MostOrderedProduct = await _context.OrderDetails
                    .GroupBy(od => od.Product)
                    .OrderByDescending(g => g.Sum(od => od.Quantity))
                    .Select(g => new MostOrderedProductViewModel
                    {
                        Product = g.Key,
                        TotalQuantity = g.Sum(od => od.Quantity)
                    })
                    .FirstOrDefaultAsync()
            };
        }
        public async Task<IActionResult> Index()
        {
            try
            {
                var stats = await GetOrderStatisticsAsync();
                return View(stats);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas generowania statystyk: {ex.Message}");
                ViewBag.ErrorMessage = "Nie udało się załadować statystyk.";
                return View("Error");
            }
        }
        public async Task<IActionResult> PaymentActivity()
        {
            var startDate = DateTime.Now.AddDays(-30);
            var orders = await _context.Orders
                .Where(o => o.OrderDate >= startDate)
                .ToListAsync(); // Pobranie danych do pamięci

            var payments = orders
                .GroupBy(o => o.OrderDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    TotalRevenue = g.Sum(o => o.TotalAmount) // Agregacja w pamięci
                })
                .OrderBy(x => x.Date)
                .ToList();

            return Json(payments);
        }


    }
}
