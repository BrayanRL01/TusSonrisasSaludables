using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class AppointmentModel
    {
        public int AppointmentId { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        [EmailAddress]
        public string? Email { get; set; }
    }
}
