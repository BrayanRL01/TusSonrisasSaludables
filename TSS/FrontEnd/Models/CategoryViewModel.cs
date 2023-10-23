using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Categoría Madre")]
        public int? MainCategoryId { get; set; }
        [Required(ErrorMessage = "El nombre es requerido"), Display(Name = "Nombre de Categoría/Subcategoría")]
        public string CategoryName { get; set; } = null!;
    }
}
