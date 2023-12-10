namespace BackEnd.Models
{
    public class RecomendationModel
    {
        public int RecomendationID { get; set; }
        public int UserID { get; set; }
        public int SpecialtyID { get; set; }
        public string Information { get; set; }
        public DateTime PostDate { get; set; }

    }
}
