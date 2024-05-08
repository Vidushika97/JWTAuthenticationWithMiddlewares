using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthenticationWithMiddlewares.Models
{
    [Table("Story")]
    public class StoryModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public long user_id { get; set; }

        [Required]
        public string title { get; set; }

        [Required]
        public string description { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        [Required]
        public DateTime updated_at { get; set;}

        // Navigation property for many-to-one relationship with User
        [ForeignKey("user_id")]
        public UserModel User { get; set; }

    }
}

