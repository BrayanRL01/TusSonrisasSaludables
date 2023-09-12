using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class User
    {
        public User()
        {
            Appointments = new HashSet<Appointment>();
            PatientRecords = new HashSet<PatientRecord>();
            ShoppingCarts = new HashSet<ShoppingCart>();
        }

        public int UserId { get; set; }
        public int RoleId { get; set; }
        public int TypeId { get; set; }
        public int GenreId { get; set; }
        public int ProvinceId { get; set; }
        public string Idnumber { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string UserAddress { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public virtual Genre Genre { get; set; } = null!;
        public virtual Province Province { get; set; } = null!;
        public virtual Role Role { get; set; } = null!;
        public virtual IdentificationType Type { get; set; } = null!;
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual ICollection<PatientRecord> PatientRecords { get; set; }
        public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; }
    }
}
