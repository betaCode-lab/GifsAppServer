using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GifsAppv2.Models
{
    public class User
    {
        [Key]
        public int IdUser { get; set; }
        
        [Required(ErrorMessage = "Username is required.")]
        [MinLength(3, ErrorMessage = "Username must be 3 characters long.")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[^@\\s]+@[^@\\s]+\\.(com|net|org|gov)$", ErrorMessage = "Email not valid.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be 8 characters long.")]
        public string? Password { get; set; }

        [NotMapped]
        public string? ConfirmPassword { get; set; }
    }
}
