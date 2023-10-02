namespace FrontEnd.Models
{
    public class DoctorViewModel
    {
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
    }
}
