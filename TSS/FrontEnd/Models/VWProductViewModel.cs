using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWProductViewModel
    {
        public int ProductID { get; set; }
        [Display(Name = "Id")]
        public int CategoryID { get; set; } 
        [Display(Name = "Tipo de categoria")]
        public string ProductName { get; set; } = null!;
        [Display(Name = "Nombre del producto")]
        public string ProductDescription { get; set; } = null!;
        [Display(Name = "Descripcion del producto")]
        public double UnitPrice { get; set; } 
        [Display(Name = "Precio Unitario")]
        public int Stock { get; set; }
        [Display(Name = "Cantidad")]
        public string ProductImage { get; set; } = null!;

    }
}
