using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwDoctor
    {
        public string IdNumber { get; set; } = null!;
        public string SpecialtyName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public byte[]? DoctorPhoto { get; set; }
    }
}
