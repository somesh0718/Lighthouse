using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;

namespace Igmite.Lighthouse.Services
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class IgmiteAuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authUserId = Convert.ToString(context.HttpContext.Items["AuthUserId"]);

            //context.HttpContext.Session.Set("", null);

            if (!context.HttpContext.Request.Path.Value.Contains("LoginByUserId") && string.IsNullOrEmpty(authUserId))
            {
                // Not logged in
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
        }
    }
}