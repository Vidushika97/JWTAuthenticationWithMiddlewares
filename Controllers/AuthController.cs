using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Services.AuthService;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthenticationWithMiddlewares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
    {
        private readonly IAuthService authService;

        //controller
        public AuthController(IAuthService authService)
        {
            this.authService = authService;
        }

        //endpoints

        [HttpPost("authenticate")]
        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            return authService.Authenticate(request);
        }

      

    }
}




