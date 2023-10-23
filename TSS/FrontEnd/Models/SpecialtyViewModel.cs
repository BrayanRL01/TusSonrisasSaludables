using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class SpecialtyViewModel
    {
        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "El nombre de la especialidad es requerido."), Display(Name = "Especialidad")]
        public string SpecialtyName { get; set; } = null!;
    }
}
