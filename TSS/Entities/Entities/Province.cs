using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Province
    {
        public Province()
        {
            Users = new HashSet<User>();
        }

        public int ProvinceId { get; set; }
        public string ProvinceName { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
