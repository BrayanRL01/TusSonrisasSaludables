using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwAdminAppointment
    {
        public int AppointmentId { get; set; }
        public string Doctor { get; set; } = null!;
        public string PacientName { get; set; } = null!;
        public string SpecialtyName { get; set; } = null!;
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}
