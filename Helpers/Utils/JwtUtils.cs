﻿using JWTAuthenticationWithMiddlewares.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace JWTAuthenticationWithMiddlewares.Helpers.Utils
{
    public static class JwtUtils
    {
        static string secret = "3hO4Lash4CzZfk0Ga6yQhd48208RGTAu";

        public static string GenerateJwtToken(UserModel user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

            byte[] key = Encoding.ASCII .GetBytes(secret);

            //token claims
            List<Claim> claims = new List<Claim>
            {
                new Claim("user_id",user.user_id.ToString()),
                new Claim("username",user.username),
            };

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken jwtToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(jwtToken);
        }

        public static bool ValidateJwtToken(string jwt)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                byte[] key = Encoding.ASCII.GetBytes(secret);

                TokenValidationParameters validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                };

                tokenHandler.ValidateToken(jwt, validationParameters, out SecurityToken validatedToken);
                JwtSecurityToken validatedJWT = (JwtSecurityToken)validatedToken;

                //get claims
                long userId = long.Parse(validatedJWT.Claims.First(claim => claim.Type == "user_id").Value);

                using(ApplicationDbContext dbContext = new ApplicationDbContext())
                {
                    UserModel? user = dbContext.Users.FirstOrDefault(u => u.user_id == userId);

                    if(user == null)
                    {
                        return false;
                    }
                    else
                    {
                        //check is the given token is the last issued token to the user
                        LoginDetailModel loginDetail = dbContext.LoginDetails.Where(ld => ld.user_id == userId).First();

                        //login detail must available
                        if(loginDetail.token != jwt)
                        {
                            return false;
                        }
                        else
                        {
                            //token is valid and latest token
                            return true;
                        }

                    }
                }
            }
            catch (Exception ex) 
            {
                return false;
            }
        }
    }
}
