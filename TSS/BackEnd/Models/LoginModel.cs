using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class LoginModel
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required, DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
        public string Roles { get; set; } = string.Empty;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
