using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class ShoppingDetail
    {
        public int DetailId { get; set; }
        public int? CartId { get; set; }
        public int? ProductId { get; set; }
        public int? Quantity { get; set; }
        public decimal? UnitPrice { get; set; }
        public decimal? Taxes { get; set; }
        public decimal? SubTotal { get; set; }

        public virtual ShoppingCart? Cart { get; set; }
        public virtual Product? Product { get; set; }
    }
}
