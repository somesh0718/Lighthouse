using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IConfiguration configuration;
        private readonly IAuthManager authManager;
        private const string JsonContentType = "application/json";

        public JwtMiddleware(RequestDelegate _next, IConfiguration _configuration, IAuthManager _authManager)
        {
            this.next = _next;
            this.configuration = _configuration;
            this.authManager = _authManager;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
                await AttachUserToContext(context, token);

            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                var httpStatusCode = ConfigurateExceptionTypes(ex);

                // set http status code and content type
                context.Response.StatusCode = httpStatusCode;
                context.Response.ContentType = JsonContentType;

                string errorMessage = string.Format("StatusCode: {0}    Message: {1}", context.Response.StatusCode, ex.Message);

                Logging.ErrorManager.Instance.WriteErrorLogsInFile(ex);
                // writes / returns error model to the response
                await context.Response.WriteAsync(errorMessage);

                //JsonConvert.SerializeObject(new ErrorModelViewModel { Message = exception.Message})

                context.Response.Headers.Clear();
            }
        }

        /// <summary>
        /// Attach user to current context
        /// </summary>
        /// <param name="context"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private async Task AttachUserToContext(HttpContext context, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Constants.SecretKey);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtSecurityToken = (JwtSecurityToken)validatedToken;
                var loginId = jwtSecurityToken.Claims.First(x => x.Type == "LoginId").Value;

                // Attach account to context on successful jwt validation
                //var account = await authManager.GetAccountByLoginIdAsync(loginId);
                //account.Password = string.Empty;
                //context.Items["Account"] = account;
                context.Items["AuthUserId"] = jwtSecurityToken.Payload["UserId"];
            }
            catch
            {
                // do nothing if jwt validation fails
                // account is not attached to context so request won't have access to secure routes
            }
        }

        /// <summary>
        /// Configurates/maps exception to the proper HTTP error Type
        /// </summary>
        /// <param name="exception">The exception.</param>
        /// <returns></returns>
        private static int ConfigurateExceptionTypes(Exception exception)
        {
            int httpStatusCode;

            // Exception type To Http Status configuration
            switch (exception)
            {
                case var _ when exception is ValidationException:
                    httpStatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                default:
                    httpStatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            return httpStatusCode;
        }
    }
}