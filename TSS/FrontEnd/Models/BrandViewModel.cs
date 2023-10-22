using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class BrandViewModel
    {
        public int BrandId { get; set; }

        [Required(ErrorMessage = "El nombre de la marca es requerido."), Display(Name = "Nombre de Marca")]
        public string BrandName { get; set; } = null!;
    }
}
