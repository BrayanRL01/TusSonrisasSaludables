using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace FrontEnd.Models
{
    public class VWRecomendationViewModel
    {
        public int RecomendationId { get; set; }

        [DataType(DataType.Date), Display(Name = "Fecha de Creción")]
        public DateTime PostDate { get; set; }

        [Display(Name = "Fecha de Creción")]
        public string FullName { get; set; } = null!;
        [Display(Name = "Especialidad")]
        public string SpecialtyName { get; set; } = null!;
        [Display(Name = "Información")]
        public string Information { get; set; } = null!;
        public byte[]? PostImage { get; set; }

    }
}
