namespace BackEnd.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        public int? MainCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
