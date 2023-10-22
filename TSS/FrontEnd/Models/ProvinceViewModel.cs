using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class ProvinceViewModel
    {
        public int ProvinceId { get; set; }
        [Display(Name = "Provincia")]
        public string ProvinceName { get; set; } = null!;
    }
}
