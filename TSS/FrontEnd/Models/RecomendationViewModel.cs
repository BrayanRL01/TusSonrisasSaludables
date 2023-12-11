using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class RecomendationViewModel
    {
        public int RecomendationId { get; set; }
        [Required(ErrorMessage = "El doctor/a es requerido."), Display(Name = "Doctor/a")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "La especialidad es requerida."), Display(Name = "Especialidad")]

        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "La información es requerida."), Display(Name = "Recomendación"), StringLength(1000)]
        public string Information { get; set; } = null!;

        [DataType(DataType.Date), Display(Name = "Fecha de publicación")]
        public DateTime PostDate { get; set; }
        [Display(Name = "Imagen")]
        public byte[]? PostImage { get; set; } = Array.Empty<byte>();

    }
}
