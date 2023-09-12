using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class Genre
    {
        public Genre()
        {
            Doctors = new HashSet<Doctor>();
            Users = new HashSet<User>();
        }

        public int GenreId { get; set; }
        public string GenreName { get; set; } = null!;

        public virtual ICollection<Doctor> Doctors { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
