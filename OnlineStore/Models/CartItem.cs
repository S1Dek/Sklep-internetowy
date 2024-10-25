using OnlineStore.Models;

namespace OnlineStore.Models
{
    public class CartItem
    {
        public int Id { get; set; }           
        public Product Product { get; set; }  
        public int Quantity { get; set; }
    }
}
