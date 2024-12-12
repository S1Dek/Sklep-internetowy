using System.ComponentModel.DataAnnotations;

namespace Sklep_Internetowy.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Display(Name = "Nazwa")]
        public string Name { get; set; }
        [Display(Name = "Obraz")]
        public string Image { get; set; }
        [Display(Name = "Cena")]
        public decimal Price { get; set; }
        [Display(Name = "Ilość w magazynie")]
        public int Stock { get; set; }
    }
}
