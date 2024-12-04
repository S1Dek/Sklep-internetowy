namespace Sklep_Internetowy.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; } // Powiązanie z zamówieniem
        public Order Order { get; set; } // Nawigacja
        public int ProductId { get; set; } // Powiązanie z produktem
        public Product Product { get; set; } // Nawigacja
        public int Quantity { get; set; } // Ilość produktów
        public decimal Price { get; set; } // Cena produktu
    }

}
