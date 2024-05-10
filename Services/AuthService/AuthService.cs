using JWTAuthenticationWithMiddlewares.DTOs.Requests;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Helpers.Utils;
using JWTAuthenticationWithMiddlewares.Models;
using Microsoft.AspNetCore.Hosting.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;


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

        public string GetMD5Hash(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                // Convert the string to a byte array
                byte[] bytes = Encoding.UTF8.GetBytes(input);

                // Compute the hash
                byte[] hash = md5Hash.ComputeHash(bytes);

                // Convert the byte array to a string in hexadecimal format
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hash.Length; i++)
                {
                    sb.Append(hash[i].ToString("x2"));
                }

                return sb.ToString();
            }
        }

        public BaseResponse Authenticate(AuthenticateRequest request)
        {
            try
            {
                UserModel? user = context.Users.Where(u => u.username == request.username).FirstOrDefault();
                if (user == null)
                {
                    return new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = new DTOs.MessageDTO { status = "Invalid username or password" }
                    };
                }

                // get password in MDS
                string md5Password = GetMD5Hash(request.password);
                // match password
                if (user.password == md5Password)
                {
                    //generate jwt
                    string jwt = JwtUtils.GenerateJwtToken(user);

                    //save token in login details
                    LoginDetailModel loginDetail = context.LoginDetails.Where(ld => ld.user_id == user.user_id).FirstOrDefault();

                    if (loginDetail == null)
                    {
                        loginDetail = new LoginDetailModel();
                        loginDetail.user_id = user.user_id;
                        loginDetail.token = jwt;

                        context.LoginDetails.Add(loginDetail);
                    }
                    else
                    {
                        loginDetail.token = jwt;
                    }

                    context.SaveChanges();
                    return new BaseResponse { status_code = StatusCodes.Status200OK, data = new { token = jwt } };
                }
                else
                {
                    return new BaseResponse { status_code = StatusCodes.Status401Unauthorized, data = new DTOs.MessageDTO { status = "Invalid username or password" } };
                }
            }
            catch (Exception ex)
            {
                return new BaseResponse { status_code = StatusCodes.Status500InternalServerError, data = new DTOs.MessageDTO { status = ex.Message } };
            }
        }
    }
}
