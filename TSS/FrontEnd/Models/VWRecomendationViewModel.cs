using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class VWRecomendationViewModel
    {
        public int RecomendationId { get; set; }
        [Display(Name = "Informacion")]
        public string Info { get; set; } = null!;
    }
}
