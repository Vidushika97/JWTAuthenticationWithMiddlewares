using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;

namespace JWTAuthenticationWithMiddlewares.Services.AuthService
{
    public interface IAuthService
    {
        BaseResponse Authenticate(AuthenticateRequest request);

        String GetMD5Hash(string input);
    }
}
