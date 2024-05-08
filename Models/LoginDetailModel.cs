using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace JWTAuthenticationWithMiddlewares.Models
{
    [Table("Login_Detail")]
    public class LoginDetailModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long id { get; set; }

        [Required]
        public long user_id { get; set; }

        [Required]
        public string token { get; set; }

        [Required]
        public DateTime updated_at { get; set; }

        [Required]
        public DateTime created_at { get; set; }

        // Navigation property for one-to-one relationship with User
        [ForeignKey("user_id")]
        public UserModel User { get; set; }

    }
}

