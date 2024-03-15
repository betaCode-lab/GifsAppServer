using System.ComponentModel.DataAnnotations;

namespace GifsAppv2.Models.OPT
{
    public class UserDTO
    {
        [Key]
        public int IdUser { get; set; }

        public string? Username { get; set; }

        public string? Email { get; set; }
    }
}
