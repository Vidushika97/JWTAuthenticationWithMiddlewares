using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Helpers.Utils;
using JWTAuthenticationWithMiddlewares.Models;
using Microsoft.AspNetCore.Hosting.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace JWTAuthenticationWithMiddlewares.Services.AuthService
{
    public class AuthService : IAuthService
    {
        //variable to store application db context
        private readonly ApplicationDbContext context;

        public AuthService(ApplicationDbContext applicationDbContext)
        {
            //get db context through DI
            context = applicationDbContext;
        }

        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                UserModel? user = dbContext.User.Where(u => u.username == request.username).FirstOrDefault();
                if (user == null)
                {
                    return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Invalid username or password"));
                }

                // get password in MDS
                string md5Password = Supports.GenerateMD5(request.password);
                // match password
                if (user.password == md5Password)
                {
                    //generate jwt
                    string jwt = JwtUtils.GenerateJwtToken(user);

                    //save token in login details
                    LoginDetailModel? loginDetail = dbContext.LoginDetails.Where(ld => ld.user_id == user.user_id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.user_id = user.user_id;
                        loginDetail.token = jwt;

                        dbContext.LoginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }

                    dbContext.SaveChanges();
                    return new BaseResponse(StatusCodes.Status200OK, new { token = jwt });
                }
                else
                {
                    return new BaseResponse(StatusCodes.Status401Unauthorized, new MessageDTO("Invalid username or password"));
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse { status_code = StatusCodes.Status500InternalServerError, data = new MessageDTO(ex.Message) };
            }
        }
    }
}
