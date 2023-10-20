namespace FrontEnd.Models
{
    public class CategoryViewModel
    {
        public int CategoryId { get; set; }
        public int? MainCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;
    }
}
