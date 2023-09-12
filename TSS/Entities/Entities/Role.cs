using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Role
    {
        public Role()
        {
            Users = new HashSet<User>();
        }

        public int RoleId { get; set; }
        public string RoleType { get; set; } = null!;

        public virtual ICollection<User> Users { get; set; }
    }
}
