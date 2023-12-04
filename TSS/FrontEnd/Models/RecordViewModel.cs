using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class RecordViewModel
    {
        public int RecordId { get; set; }
        [Required(ErrorMessage = "El paciente es requerido."), Display(Name = "Nombre del Paciente")]

        public int UserId { get; set; }
        [Required(ErrorMessage = "El doctor/a es requerido."), Display(Name = "Nombre del Doctor/a")]

        public int DoctorId { get; set; }
        [Required(ErrorMessage = "El procedimiento es requerido."), Display(Name = "Procedimiento")]

        public int ProcedureId { get; set; }

        [Required(ErrorMessage = "El diagnóstico es requerido."), Display(Name = "Diagnóstico")]
        public string Diagnoses { get; set; } = null!;
        [Required(ErrorMessage = "Los síntomas son requeridos."), Display(Name = "Síntomas")]

        public string Symptoms { get; set; } = null!;
        [Required(ErrorMessage = "El tratamiento es requerido."), Display(Name = "Tratamiento")]
        public string Treatment { get; set; } = null!;
        [Display(Name = "Fecha de la Cita")]
        public DateTime RecordDate { get; set; } = DateTime.Now.Date;
    }
}
