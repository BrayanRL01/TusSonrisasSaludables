using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FrontEnd.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public int BrandId { get; set; }

        [Display(Name = "Nombre del producto")]
        public string ProductName { get; set; } = null!;

        [Display(Name = "Descripcion del producto")]
        public string ProductDescription { get; set; } = null!;

        [Display(Name = "Precio")]
        public double UnitPrice { get; set; }

        [Display(Name = "Cantidad")]
        public int Stock { get; set; }

        public string ProductImage { get; set; } = null!;
    }
}
