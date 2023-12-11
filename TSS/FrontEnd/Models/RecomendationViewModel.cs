using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class RecomendationViewModel
    {
        public int RecomendationId { get; set; }
        [Required(ErrorMessage = "El doctor/a es requerido.")]
        public int DoctorId { get; set; }
        [Required(ErrorMessage = "La especialidad es requerida.")]

        public int SpecialtyId { get; set; }
        [Required(ErrorMessage = "La información es requerida.")]

        public string Information { get; set; } = null!;

        [DataType(DataType.Date)]
        public DateTime PostDate { get; set; }
        public byte[]? PostImage { get; set; } = Array.Empty<byte>();

    }
}
