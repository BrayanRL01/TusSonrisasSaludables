namespace BackEnd.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }
        public byte[]? ProductImage { get; set; }
    }
}
