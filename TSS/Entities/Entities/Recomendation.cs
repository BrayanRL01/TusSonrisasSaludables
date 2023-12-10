using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Recomendation
    {
        public int RecomendationId { get; set; }
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public string Information { get; set; } = null!;
        public DateTime PostDate { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual Specialty Specialty { get; set; } = null!;
    }
}
