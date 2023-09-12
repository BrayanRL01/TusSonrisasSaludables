using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Category
    {
        public Category()
        {
            InverseMainCategory = new HashSet<Category>();
            Products = new HashSet<Product>();
        }

        public int CategoryId { get; set; }
        public int? MainCategoryId { get; set; }
        public string CategoryName { get; set; } = null!;

        public virtual Category? MainCategory { get; set; }
        public virtual ICollection<Category> InverseMainCategory { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
