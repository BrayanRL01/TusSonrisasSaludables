using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwProduct
    {
        public int ProductId { get; set; }
        public string BrandName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string UnitPrice { get; set; } = null!;
        public int Stock { get; set; }
        public byte[]? ProductImage { get; set; }
    }
}
