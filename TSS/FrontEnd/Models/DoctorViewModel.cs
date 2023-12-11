using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class DoctorViewModel
    {
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "El tipo de identificación es requerido."),
            Display(Name = "Tipo de Identificación")]
        public int TypeId { get; set; }

        [Required(ErrorMessage = "La especialidad es requerida."),
            Display(Name = "Especialidad")]
        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "El género es requerido."),
            Display(Name = "Género")]
        public int GenreId { get; set; }

        [Required(ErrorMessage = "El número de cédula es requerido."), Display(Name = "Número de Identificación")]
        [StringLength(11, ErrorMessage = "El número de cédula consta de 11 carácteres.")]
        [RegularExpression(@"^[1-8]-[0-9]{4}-[0-9]{4}$",
     ErrorMessage = "El número de cédula debe tener el siguiente formato: 1-1111-1111")]
        public string IdNumber { get; set; } = null!;

        [Required(ErrorMessage = "El nombre es requerido."), Display(Name = "Nombre"), StringLength(30)]
        public string DoctorName { get; set; } = null!;

        [Required(ErrorMessage = "El primer apellido es requerido."), Display(Name = "Primer Apellido"), StringLength(20)]
        public string FirstName { get; set; } = null!;

        [Required(ErrorMessage = "El segundo apellido es requerido."), Display(Name = "Segundo Apellido"), StringLength(20)]
        public string LastName { get; set; } = null!;

        [Required(ErrorMessage = "La fecha de nacimiento es requerida."), Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; } = DateTime.Now.Date;

        [Required(ErrorMessage = "El correo electrónico es requerido."), Display(Name = "Correo Electrónico"), StringLength(50)]
        [EmailAddress(ErrorMessage = "El correo electrónico debe ser válido.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El número de teléfono es requerido."), Display(Name = "Número de Teléfono")]
        [StringLength(9, ErrorMessage = "El número de teléfono consta de 9 carácteres."), DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^[2, 5, 6, 7, 8][0-9]{3}-[0-9]{4}$",
ErrorMessage = "El número de teléfono debe tener el siguiente formato: 8888-8888")]
        public string PhoneNumber { get; set; } = null!;

        [Display(Name = "Imagen")]
        public byte[]? DoctorPhoto { get; set; }
    }
}
