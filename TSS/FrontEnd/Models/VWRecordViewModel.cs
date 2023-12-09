using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWRecordViewModel
    {
        public int RecordId { get; set; }
        [Display(Name = "Paciente")]
        public string PatientName { get; set; } = null!;
        [Display(Name = "Número de Cédula")]
        public string IdNumber { get; set; } = null!;
        [Display(Name = "Doctor/a")]
        public string DoctorName { get; set; } = null!;
        [Display(Name = "Procedimiento")]
        public string ProcedureName { get; set; } = null!;

        [Display(Name = "Diagnóstico")]
        public string Diagnoses { get; set; } = null!;

        [Display(Name = "Síntomas")]
        public string Symptoms { get; set; } = null!;

        [Display(Name = "Tratamiento")]
        public string Treatment { get; set; } = null!;

        [Display(Name = "Fecha de la Cita")]
        public DateTime RecordDate { get; set; }
    }
}
