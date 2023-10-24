using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FrontEnd.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required(ErrorMessage = "La marca es requerida."), Display(Name = "Marca")]
        public int BrandId { get; set; }

        [Required(ErrorMessage = "La categoría es requerida."), Display(Name = "Categoría")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "El nombre del producto es requerido."), Display(Name = "Nombre del Producto"), StringLength(30)]
        public string ProductName { get; set; } = null!;

        [Required(ErrorMessage = "La descripción del producto es requerido."), Display(Name = "Descripción del Producto")]
        public string ProductDescription { get; set; } = null!;

        [Required(ErrorMessage = "El precio del producto es requerido."), Display(Name = "Precio")]
        public decimal UnitPrice { get; set; }
        [Required(ErrorMessage = "La cantidad es requerida."), Display(Name = "Cantidad")]
        public int Stock { get; set; }
        [Display(Name = "Imagen del Producto")]
        public byte[]? ProductImage { get; set; }
    }
}
