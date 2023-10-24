using System.ComponentModel.DataAnnotations;

namespace FrontEnd.Models
{
    public class GenreViewModel
    {
        public int GenreId { get; set; }

        [Required(ErrorMessage = "El nombre del género es requerido."), Display(Name = "Nombre del Género"), StringLength(20)]
        public string GenreName { get; set; } = null!;
    }
}
