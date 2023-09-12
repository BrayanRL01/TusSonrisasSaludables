using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class ShoppingCart
    {
        public ShoppingCart()
        {
            ShoppingDetails = new HashSet<ShoppingDetail>();
        }

        public int CartId { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreationDate { get; set; }
        public decimal? Total { get; set; }

        public virtual User? User { get; set; }
        public virtual ICollection<ShoppingDetail> ShoppingDetails { get; set; }
    }
}
