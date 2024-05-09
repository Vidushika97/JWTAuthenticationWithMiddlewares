using JWTAuthenticationWithMiddlewares.DTOs;
using JWTAuthenticationWithMiddlewares.DTOs.Responses;
using JWTAuthenticationWithMiddlewares.Helpers.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;

namespace JWTAuthenticationWithMiddlewares.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        BaseResponse response;

        public Task Invoke(HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split("")[1];

            if(token == null)
            {
                //check incoming request is from a enabled unauthorized route
                if(IsEnabledUnauthorizedRoute(httpContext))
                {
                    return _next(httpContext);
                }

                response = new BaseResponse
                { 
                    status_code = StatusCodes.Status401Unauthorized,
                    data = new MessageDTO{ status = "Unauthorized" }
                };

                httpContext.Response.StatusCode = response.status_code;
                httpContext.Response.ContentType = "application/json";

                return httpContext.Response.WriteAsJsonAsync(response);

            }
            else
            {
                if (JwtUtils.ValidateJwtToken(token))
                {
                    return _next(httpContext);
                }
                else
                {
                    response = new BaseResponse
                    {
                        status_code = StatusCodes.Status401Unauthorized,
                        data = "unauthorized"
                    };

                    httpContext.Response.StatusCode = response.status_code;
                    httpContext.Response.ContentType = "application/json";
                    return httpContext.Response.WriteAsJsonAsync(response);
                }
                  
            }    
            
        }

        ///<summary>
        ///This method is used to check incoming request is from a enabled unauthorized request
        ///</summary>
        ///<param name="httpContext"></param>
        ///<returns></returns>


        private bool IsEnabledUnauthorizedRoute(HttpContext httpContext)
        {
            List<string> enableRoutes = new List<string>
                {
                    "/api/User/createUser",
                    "/api/auth/authenticate",
                };

            bool isEnableUnauthorizedRoute = false;

            if (httpContext.Request.Path.Value is not null)
            {
                isEnableUnauthorizedRoute = enableRoutes.Contains(httpContext.Request.Path.Value);
            }
            return isEnableUnauthorizedRoute;
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
