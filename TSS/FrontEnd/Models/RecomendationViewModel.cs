namespace FrontEnd.Models
{
    public class RecomendationViewModel
    {
        public int RecomendationId { get; set; }
        public DateTime PostDate { get; set; }
        public string FullName { get; set; } = null!;
        public string SpecialtyName { get; set; } = null!;
        public string Information { get; set; } = null!;
    }
}
