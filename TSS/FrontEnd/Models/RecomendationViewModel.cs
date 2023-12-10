namespace FrontEnd.Models
{
    public class RecomendationViewModel
    {
        public int RecomendationId { get; set; }
        public int UserId { get; set; }
        public int SpecialtyId { get; set; }
        public string Information { get; set; } = null!;
        public DateTime PostDate { get; set; }
    }
}
