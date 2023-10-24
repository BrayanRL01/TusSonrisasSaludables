using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProcedureViewModel
    {
        public int ProcedureId { get; set; }

        [Required(ErrorMessage = "El tipo de procedimiento es requerido."), Display(Name = "Tipo de procedimiento"), StringLength(30)]
        public string ProcedureName { get; set; } = null!;
    }
}
