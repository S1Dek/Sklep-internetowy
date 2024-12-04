namespace Sklep_Internetowy.Models
{
    public class Order
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Klucz obcy jako string
        public User User { get; set; } // Nawigacja do użytkownika
        public DateTime OrderDate { get; set; } // Data zamówienia
        public decimal TotalAmount { get; set; } // Suma zamówienia
        public ICollection<OrderDetail> OrderDetails { get; set; }
    }

}
