using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class ClinicProcedure
    {
        public ClinicProcedure()
        {
            PatientRecords = new HashSet<PatientRecord>();
        }

        public int ProcedureId { get; set; }
        public string ProcedureName { get; set; } = null!;

        public virtual ICollection<PatientRecord> PatientRecords { get; set; }
    }
}
