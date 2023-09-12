using System;
using System.Collections.Generic;

namespace Entities.Entities
{
    public partial class VwUser
    {
        public int UserId { get; set; }
        public string Idnumber { get; set; } = null!;
        public string ProvinceName { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string GenreName { get; set; } = null!;
        public DateTime BirthDate { get; set; }
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
    }
}
