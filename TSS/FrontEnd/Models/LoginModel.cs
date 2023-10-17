using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class LoginModel
    {
        [Required, EmailAddress, Display(Name = "Correo Electrónico")]
        public string Email { get; set; } = null!;

        [Required, Display(Name = "Contraseña")]
        [DataType(DataType.Password)]
        public string PasswordHash { get; set; } = null!;
        public string Roles { get; set; } = string.Empty;
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; } = string.Empty;
    }
}
