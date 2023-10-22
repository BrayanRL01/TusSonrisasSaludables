using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWProductViewModel
    {
        public int ProductId { get; set; }
        [Display(Name = "Marca")]
        public string BrandName { get; set; } = null!;
        [Display(Name = "Categoría")]
        public string CategoryName { get; set; } = null!;
        [Display(Name = "Nombre del Producto")]

        public string ProductName { get; set; } = null!;
        [Display(Name = "Descripción del Producto")]

        public string Description { get; set; } = null!;

        [Display(Name = "Precio")]

        public string UnitPrice { get; set; } = null!;
        [Display(Name = "Cantidad")]

        public int Stock { get; set; }
        [Display(Name = "Imagen")]

        public byte[]? ProductImage { get; set; }

    }
}
