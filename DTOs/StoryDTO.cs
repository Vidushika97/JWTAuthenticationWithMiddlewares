using System.ComponentModel.DataAnnotations;

namespace JWTAuthenticationWithMiddlewares.DTOs
{
    public class StoryDTO
    {
        [Required]
        public long user_id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

       
    }
}
