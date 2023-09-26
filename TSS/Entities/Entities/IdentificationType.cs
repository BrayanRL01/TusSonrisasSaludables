using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class IdentificationType
    {
        public IdentificationType()
        {
            Doctors = new HashSet<Doctor>();
            Users = new HashSet<User>();
        }

        public int TypeId { get; set; }
        public string IdType { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
