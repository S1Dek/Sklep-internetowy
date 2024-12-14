using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class OrderStatisticsViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Łączna liczba zamówień")]
        public int TotalOrders { get; set; }
        [Display(Name = "Całkowity dochód")]
        public decimal TotalRevenue { get; set; }
        [Display(Name = "Najczęściej zamawiany produkt")]
        public MostOrderedProductViewModel MostOrderedProduct { get; set; }
    }
    public class MostOrderedProductViewModel
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa produktu")]
        public Product Product { get; set; }
        [Display(Name = "Całkowita ilość")]
        public int TotalQuantity { get; set; }
    }
}