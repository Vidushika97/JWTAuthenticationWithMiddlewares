using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Models;


namespace JWTAuthenticationWithMiddlewares.Services.UserService
{
    public class UserService : IUserService
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public UserService(ApplicationDbContext applicationDbContext)
        {
            //get db context through DI
            context = applicationDbContext;
        }
        public BaseResponse CreateUser(CreateUserRequest request)
        {
            BaseResponse response;
            
            try
            {

                // create new instace of user model

                UserModel newUser = new UserModel();
                newUser.first_name = request.first_name;
                newUser.last_name = request.last_name;
                newUser.email = request.email;
                newUser.username = request.username;
                newUser.password = request.password;
               

                using (context)
                {
                    context.Add(newUser);
                    context.SaveChanges();

                }

                response = new BaseResponse
                {
                    status_code = StatusCodes.Status200OK,
                    data = new { message = "Successfully created the new student" }
                };

                return response;
            }
            catch (Exception ex)
            {
                response = new BaseResponse
                {
                    status_code = StatusCodes.Status500InternalServerError,
                    data = new { message = "Internal server error : " + ex.Message }
                };

                return response;
            }
        }

    }
}
