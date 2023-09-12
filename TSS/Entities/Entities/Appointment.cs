using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Appointment
    {
        public int AppointmentId { get; set; }
        public int? UserId { get; set; }
        public int? DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

        public virtual Doctor? Doctor { get; set; }
        public virtual Specialty Specialty { get; set; } = null!;
        public virtual User? User { get; set; }
    }
}
