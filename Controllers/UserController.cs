using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Services.UserService;
using Microsoft.AspNetCore.Mvc;

namespace JWTAuthenticationWithMiddlewares.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;

        //controller
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        //endpoints

        [HttpPost("createUser")]
        public BaseResponse CreateUser(CreateUserRequest request)
        {
            return userService.CreateUser(request);
        }
    }
}






