using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs.Requests
{
    public class CreateStoryRequest
    {
     

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }
    }
}
