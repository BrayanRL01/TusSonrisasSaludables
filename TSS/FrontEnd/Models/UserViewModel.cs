using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int TypeId { get; set; }
        public int GenreId { get; set; }
        public int ProvinceId { get; set; }
        [RegularExpression(@"^[1-8]-[0-9]{4}-[0-9]{4}$",
        ErrorMessage = "El número de cédula debe tener el siguiente formato: 1-1111-1111")]
        public string Idnumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[2, 5, 6, 7, 8][0-9]{3}-[0-9]{4}$",
        ErrorMessage = "El número de teléfono debe tener el siguiente formato: 8888-8888")]
        public string PhoneNumber { get; set; }
        public string UserAddress { get; set; } = null!;
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
    }
}
