using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class AppointmentViewModel
    {
        public int AppointmentId { get; set; }
        public int? UserId { get; set; }

        [Display(Name = "Doctor a Asignar")]
        public int? DoctorId { get; set; }

        [Display(Name = "Especialidad")]
        public int SpecialtyId { get; set; }

        [Display(Name = "Hora de Inicio")]
        [DataType(DataType.DateTime)]
        public DateTime StartTime { get; set; } = DateTime.Now.Date;

        [Display(Name = "Hora de Fin")]
        [DataType(DataType.DateTime)]
        public DateTime EndTime { get; set; } = DateTime.Now.Date;
    }
}
