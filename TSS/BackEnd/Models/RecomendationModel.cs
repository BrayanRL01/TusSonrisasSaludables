namespace BackEnd.Models
{
    public class RecomendationModel
    {
        public int RecomendationId { get; set; }
        public int DoctorId { get; set; }
        public int SpecialtyId { get; set; }
        public string Information { get; set; } = null!;
        public DateTime PostDate { get; set; }
        public byte[]? PostImage { get; set; } = Array.Empty<byte>();


    }
}
