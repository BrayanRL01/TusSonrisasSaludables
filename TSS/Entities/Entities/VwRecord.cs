using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwRecord
    {
        public int RecordId { get; set; }
        public string PatientName { get; set; } = null!;
        public string DoctorName { get; set; } = null!;
        public string ProcedureName { get; set; } = null!;
        public string Diagnoses { get; set; } = null!;
        public string Symptoms { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public DateTime RecordDate { get; set; }
    }
}
