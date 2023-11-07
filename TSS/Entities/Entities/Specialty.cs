using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Specialty
    {
        public Specialty()
        {
            Appointments = new HashSet<Appointment>();
            Doctors = new HashSet<Doctor>();
            Recomendations = new HashSet<Recomendation>();
        }

        public int SpecialtyId { get; set; }
        public string SpecialtyName { get; set; } = null!;

        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<Recomendation> Recomendations { get; set; }
    }
}
