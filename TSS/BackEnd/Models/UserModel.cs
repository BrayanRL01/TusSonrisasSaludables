namespace BackEnd.Models
{
    public class UserModel
    {
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
    }
}
