using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWSubCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Categoría Madre")]
        public string MainCategory { get; set; } = null!;

        [Display(Name = "Subcategoría")]
        public string SubCategory { get; set; } = null!;
    }
}
