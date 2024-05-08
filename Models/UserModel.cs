using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthenticationWithMiddlewares.Models
{
    [Table("User")]
    public class UserModel
    {
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long user_id { get; set; }

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

        [Required]
        public DateTime created_at { get; set; }
        
        [Required]
        public DateTime updated_at { get; set;}

        // Navigation property for one-to-one relationship with LoginDetail
        public LoginDetailModel Login_Detail { get; set; }

        // Navigation property for one-to-many relationship with Story
        public List<StoryModel> Stories { get; set; }

    }
}


