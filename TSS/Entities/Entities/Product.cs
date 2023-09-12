using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Product
    {
        public Product()
        {
            ShoppingDetails = new HashSet<ShoppingDetail>();
        }

        public int ProductId { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string ProductDescription { get; set; } = null!;
        public decimal UnitPrice { get; set; }
        public int Stock { get; set; }

        public virtual Brand Brand { get; set; } = null!;
        public virtual Category Category { get; set; } = null!;
        public virtual ICollection<ShoppingDetail> ShoppingDetails { get; set; }
    }
}
