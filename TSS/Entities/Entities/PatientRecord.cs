using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class PatientRecord
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int ProcedureId { get; set; }
        public string Diagnoses { get; set; } = null!;
        public string Symptoms { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public DateTime RecordDate { get; set; }

        public virtual Doctor Doctor { get; set; } = null!;
        public virtual ClinicProcedure Procedure { get; set; } = null!;
        public virtual User User { get; set; } = null!;
    }
}
