using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace GifsAppv2.Models
{
    public class ChangePassword
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        [NotMapped]
        public string? ConfirmPassword { get; set; }

        public string? Token { get; set; }
    }
}
