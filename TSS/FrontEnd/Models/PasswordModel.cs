using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class PasswordModel
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "La nueva contraseña es requerida."), DataType(DataType.Password), Display(Name = "Contraseña"), StringLength(20)]
        [RegularExpression(@"^(?=.*\d)(?=.*[A-Z])(?=.*\W).{8,}$", ErrorMessage = "La contraseña debe cumplir con 8 caractéres, un número, " +
            "una letra mayúscula y un caractér especial.")]
        public string Password { get; set; } = string.Empty;
    }
}
