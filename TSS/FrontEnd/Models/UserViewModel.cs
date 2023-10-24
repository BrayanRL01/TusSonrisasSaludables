using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        public int RoleId { get; set; }

        [Required(ErrorMessage = "El tipo de identificación es necesaria."), Display(Name = "Tipo de Identificación")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "El género es necesario."), Display(Name = "Género")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "La provincia es necesaria."), Display(Name = "Provincia de Residencia")]
        public int ProvinceId { get; set; }

        [Required(ErrorMessage = "El número de cédula es requerido."), Display(Name = "Número de Identificación")]
        [StringLength(11, ErrorMessage = "El número de cédula consta de 11 carácteres.")]
        [RegularExpression(@"^[1-8]-[0-9]{4}-[0-9]{4}$",
        ErrorMessage = "El número de cédula debe tener el siguiente formato: 1-1111-1111")]
        public string IdNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es requerido."), Display(Name = "Nombre"), StringLength(20)]
        public string UserName { get; set; } = null!;

        [Required(ErrorMessage = "El primer apellido es requerido."), Display(Name = "Primer Apellido"), StringLength(20)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El segundo apellido es requerido."), Display(Name = "Segundo Apellido"), StringLength(20)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida."), Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "El correo electrónico es requerido."), Display(Name = "Correo Electrónico"), StringLength(20)]
        [EmailAddress(ErrorMessage = "El correo electrónico debe ser válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El número de teléfono es requerido."), Display(Name = "Número de Teléfono")]
        [StringLength(9, ErrorMessage = "El número de teléfono consta de 9 carácteres."), DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[2, 5, 6, 7, 8][0-9]{3}-[0-9]{4}$",
        ErrorMessage = "El número de teléfono debe tener el siguiente formato: 8888-8888")]
        public string PhoneNumber { get; set; } = null!;

        [Required(ErrorMessage = "La dirección es requerida."), Display(Name = "Dirección de vivienda")]
        public string UserAddress { get; set; } = null!;

        [Required(ErrorMessage = "La contraseña es requerida."), Display(Name = "Contraseña"), DataType(DataType.Password), StringLength(20)]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "La contraseña debe cumplir con 8 caractéres, un número, " +
            "una letra mayúscula y un caractér especial.")]
        public string PasswordHash { get; set; } = null!;
    }
}
