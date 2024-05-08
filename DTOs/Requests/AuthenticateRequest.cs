using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs.Requests
{
    public class AuthenticateRequest
    {
        [Required]
        public string username { get; set; }
    
        [Required]
        public string user_id { get; set; }
    }
}


