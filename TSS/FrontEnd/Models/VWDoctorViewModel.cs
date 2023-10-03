using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWDoctorViewModel
    {
        public int DoctorId { get; set; }

        public string IdNumber { get; set; } = null!;
        public string SpecialtyName { get; set; } = null!;
        public string FullName { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
        public byte[]? DoctorPhoto { get; set; }
    }
}
