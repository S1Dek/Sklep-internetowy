using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [Display(Name = "Użytkownik")]
        public User User { get; set; }
        [Display(Name = "Data zamówienia")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Suma")]
        public decimal TotalAmount { get; set; }
        [Display(Name = "Szczegóły zamówienia")]
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
