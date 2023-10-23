using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWDoctorViewModel
    {
        public int DoctorId { get; set; }
        [Display(Name = "Número de Cédula")]

        public string IdNumber { get; set; } = null!;
        [Display(Name = "Especialidad")]

        public string SpecialtyName { get; set; } = null!;

        [Display(Name = "Nombre Completo")]

        public string FullName { get; set; } = null!;

        [DataType(DataType.Date), Display(Name = "Fecha de Nacimiento")]
        public DateTime BirthDate { get; set; }

        [DataType(DataType.EmailAddress), Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;

        [DataType(DataType.PhoneNumber), Display(Name = "Número de Teléfono")]
        public string PhoneNumber { get; set; } = null!;
        [Display(Name = "Imagen")]
        public byte[]? DoctorPhoto { get; set; }
    }
}
