using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class EmailModel
    {
        [Required, EmailAddress(ErrorMessage = "Debe ingresar un correo electrónico válido."), Display(Name = "Correo Electrónico")]
        public string To { get; set; } = string.Empty;
        [Required, Display(Name = "Asunto")]
        public string Subject { get; set; } = string.Empty;
        [Required, Display(Name = "Contenido")]
        public string Body { get; set; } = string.Empty;

    }
}
