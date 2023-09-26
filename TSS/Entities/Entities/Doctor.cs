using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Doctor
    {
        public Doctor()
        {
            Appointments = new HashSet<Appointment>();
            PatientRecords = new HashSet<PatientRecord>();
        }

        public int DoctorId { get; set; }
        public int TypeId { get; set; }
        public int SpecialtyId { get; set; }
        public int GenreId { get; set; }
        public string IdNumber { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public byte[]? DoctorPhoto { get; set; }

        public virtual Genre Genre { get; set; } = null!;
        public virtual Specialty Specialty { get; set; } = null!;
        public virtual IdentificationType Type { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientRecord> PatientRecords { get; set; }
    }
}
