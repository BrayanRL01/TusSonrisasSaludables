using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWAppointmentViewModel
    {
        public int AppointmentId { get; set; }

        [Display(Name = "Nombre del Doctor/a")]
        public string Doctor { get; set; } = null!;

        [Display(Name = "Especialidad")]
        public string SpecialtyName { get; set; } = null!;

        [Display(Name = "Hora de Inicio")]
        public DateTime StartTime { get; set; }

        [Display(Name = "Hora de Fin")]
        public DateTime EndTime { get; set; }
    }
}
