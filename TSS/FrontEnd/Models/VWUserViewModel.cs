using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWUserViewModel
    {
        public int UserId { get; set; }
        public string Idnumber { get; set; } = null!;
        public string ProvinceName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }
    }
}
