using System.ComponentModel.DataAnnotations;

namespace Portfolio.Models
{
    public class EmailModel
    {
        [Required]
        [Display(Name = "Name")]
        public string FromName { get; set; }

        [Required]
        [Display(Name = "E-mail")]
        [EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }
    }
}