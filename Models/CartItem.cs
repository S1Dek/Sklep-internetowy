namespace Sklep_Internetowy.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int UserId { get; set; } // Klucz obcy jako string
        public User User { get; set; } // Nawigacja do użytkownika
        public int ProductId { get; set; } // Klucz obcy do produktu
        public Product Product { get; set; } // Nawigacja do produktu
        public int Quantity { get; set; } // Ilość
    }
}
