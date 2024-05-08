using JWTAuthenticationWithMiddlewares.Helpers.Utils.GlobalAttributes;
using JWTAuthenticationWithMiddlewares.Models;
using Microsoft.EntityFrameworkCore;

namespace JWTAuthenticationWithMiddlewares

{
    public class ApplicationDbContext : DbContext
    {
        //constructors
        public ApplicationDbContext()
        {
            
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //add models to database context
        public virtual DbSet<LoginDetailModel> LoginDetails { get; set; }


        public virtual DbSet<StoryModel> Stories { get; set; }


        public virtual DbSet<UserModel> Users { get; set; }

        //MYSQL configuration to use with default ApplicationDbContext constructor if not configured
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) 
            {
                optionsBuilder.UseMySql(GlobalAttributes.mysqlConfiguration.connectionString, ServerVersion.AutoDetect(GlobalAttributes.mysqlConfiguration.connectionString));
            }
        }
    }
}
