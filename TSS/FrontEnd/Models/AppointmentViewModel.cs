using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int? UserId { get; set; }

        [Required(ErrorMessage = "El doctor/a es requerido."), Display(Name = "Doctor a Asignar")]
        public int? DoctorId { get; set; }

        [Required(ErrorMessage = "La especialidad es requerida."), Display(Name = "Especialidad")]
        public int SpecialtyId { get; set; }

        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Now.Date;

        [Display(Name = "Hora de Fin")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; } = DateTime.Now.Date;
        [EmailAddress]
        public string? Email { get; set; }

        public List<string>? SelectedTimes { get; set; }
    }
}
