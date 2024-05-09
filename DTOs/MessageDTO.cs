using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs
{
    public class MessageDTO
    {
        [Required]
        public string status { get; set; }
    }
}
