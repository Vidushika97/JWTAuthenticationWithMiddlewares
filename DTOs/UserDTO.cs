using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs
{
    public class UserDTO
    {
        [Required]
        public string first_name { get; set; }

        [Required]
        public string last_name { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public string username { get; set; }

        [Required]
        public string password { get; set; }
    }
}
