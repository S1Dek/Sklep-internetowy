using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        [Display(Name = "Zamówienie")]
        public Order Order { get; set; }
        public int ProductId { get; set; }
        [Display(Name = "Produkt")]
        public Product Product { get; set; }
        [Display(Name = "Ilość")]
        public int Quantity { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
    }

}
