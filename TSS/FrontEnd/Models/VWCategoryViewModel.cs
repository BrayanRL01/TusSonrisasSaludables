using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWCategoryViewModel
    {
        public int CategoryId { get; set; }
        [Display(Name = "Categoría Madre")]
        public string CategoryName { get; set; } = null!;
    }
}
