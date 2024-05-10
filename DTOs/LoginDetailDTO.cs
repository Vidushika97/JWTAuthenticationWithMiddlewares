using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs
{
    public class LoginDetailDTO
    {
        public long user_id { get; set; }

        [Required]
        public string token { get; set; }
    }
}
