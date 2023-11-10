using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWUserViewModel
    {
        public int UserId { get; set; }
        [Display(Name = "Número de Cédula")]
        public string IdNumber { get; set; } = null!;
        [Display(Name = "Provincia de Residencia")]
        public string ProvinceName { get; set; } = null!;
        [Display(Name = "Nombre Completo")]
        public string FullName { get; set; } = null!;
        [Display(Name = "Género")]
        public string GenreName { get; set; } = null!;
        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }
        [Display(Name = "Correo Electrónico")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;
        [Display(Name = "Número de Teléfono")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; } = null!;
    }
}
