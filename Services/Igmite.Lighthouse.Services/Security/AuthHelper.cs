using Igmite.Lighthouse.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Igmite.Lighthouse.Services
{
    public class AuthHelper
    {
        /// <summary>
        /// Get JWT token after authentication successful
        /// </summary>
        /// <param name="loginId"></param>
        /// <returns></returns>
        public static string GenerateJWTToken(LoginResponce userModel)
        {
            if (userModel == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Constants.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, userModel.LoginId),
                    new Claim(ClaimTypes.GivenName, userModel.UserName),
                    new Claim("LoginId", userModel.LoginId),
                    new Claim("UserId", userModel.UserId),
                    new Claim("RoleId", userModel.RoleCode)                    
                }),

                Issuer = userModel.EmailId,
                IssuedAt = Constants.GetCurrentDateTime,
                Expires = Constants.GetCurrentDateTime.AddDays(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            string authToken = tokenHandler.WriteToken(token);

            //SecurityToken securityToken = tokenHandler.ReadToken(authToken);
            //JwtSecurityToken jwtSecurityToken = (JwtSecurityToken)securityToken;

            return authToken;
        }

        public bool ValidateAuthToken(string authToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken = null;

            try
            {
                tokenHandler.ValidateToken(authToken, new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,

                    ValidIssuer = Constants.CorsServiceUrl,
                    ValidAudience = Constants.CorsServiceUrl,

                    // Specify the key used to sign the token:
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.SecretKey))
                }, out validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }

        public string GetClaim(string token, string claimType)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.FirstOrDefault(claim => claim.Type == claimType).Value;

            return stringClaimValue;
        }
    }
}