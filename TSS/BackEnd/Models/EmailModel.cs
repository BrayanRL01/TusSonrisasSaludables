using System.ComponentModel.DataAnnotations;

namespace BackEnd.Models
{
    public class EmailModel
    {
        [EmailAddress]
        public string To { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
    }
}
