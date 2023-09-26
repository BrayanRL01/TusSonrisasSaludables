using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [Display(Name = "Tipo de Identificación")]
        public int TypeId { get; set; }

        [Display(Name = "Género")]
        public int GenreId { get; set; }

        [Display(Name = "Provincia de Residencia")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "El número de cédula es requerido.")]
        [StringLength(11, ErrorMessage = "El número de cédula consta de 11 carácteres.")]
        [RegularExpression(@"^[1-8]-[0-9]{4}-[0-9]{4}$",
        ErrorMessage = "El número de cédula debe tener el siguiente formato: 1-1111-1111")]
        [Display(Name = "Número de Identificación")]
        public string Idnumber { get; set; } = null!;

        [Display(Name = "Nombre")]
        public string UserName { get; set; } = null!;

        [Display(Name = "Primer Apellido")]
        public string FirstName { get; set; } = null!;

        [Display(Name = "Segundo Apellido")]
        public string LastName { get; set; } = null!;

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }

        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        [Display(Name = "Número de Teléfono")]
        [StringLength(9, ErrorMessage = "El número de teléfono consta de 9 carácteres.")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[2, 5, 6, 7, 8][0-9]{3}-[0-9]{4}$",
        ErrorMessage = "El número de teléfono debe tener el siguiente formato: 8888-8888")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Dirección de vivienda")]
        public string UserAddress { get; set; } = null!;

        [Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
    }
}
