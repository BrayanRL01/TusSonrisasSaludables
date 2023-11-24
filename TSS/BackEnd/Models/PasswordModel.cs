using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class PasswordModel
    {
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        [PasswordPropertyText]
        public string Password { get; set; } = string.Empty;
        [PasswordPropertyText]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
