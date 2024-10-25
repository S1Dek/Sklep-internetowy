using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models
{
    public class Product
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Nazwa produktu jest wymagana.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Opis produktu jest wymagany.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Cena produktu jest wymagana.")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        public string ImageUrl { get; set; }
    }
}
