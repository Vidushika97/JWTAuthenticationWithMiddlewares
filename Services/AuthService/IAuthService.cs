using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;

namespace JWTAuthenticationWithMiddlewares.Services.AuthService
{
    public interface IAuthService
    {
        BaseResponse Authenticate(AuthenticateRequest request);

       
    }
}
