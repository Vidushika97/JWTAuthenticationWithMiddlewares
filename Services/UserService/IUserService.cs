using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;

namespace JWTAuthenticationWithMiddlewares.Services.UserService
{
    public interface IUserService
    {
        BaseResponse CreateUser(CreateUserRequest request);
    }
}
