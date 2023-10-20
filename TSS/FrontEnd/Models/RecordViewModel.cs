namespace FrontEnd.Models
{
    public class RecordViewModel
    {
        public int RecordId { get; set; }
        public int UserId { get; set; }
        public int DoctorId { get; set; }
        public int ProcedureId { get; set; }
        public string Diagnoses { get; set; } = null!;
        public string Symptoms { get; set; } = null!;
        public string Treatment { get; set; } = null!;
        public DateTime RecordDate { get; set; }
    }
}
