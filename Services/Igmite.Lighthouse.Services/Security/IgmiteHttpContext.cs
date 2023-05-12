using Microsoft.AspNetCore.Http;
using System;

namespace Igmite.Lighthouse.Services
{
    public static class IgmiteHttpContext
    {
        public static IServiceProvider ServiceProvider;

        static IgmiteHttpContext()
        { }

        public static HttpContext Current
        {
            get
            {
                object factory = ServiceProvider.GetService(typeof(IHttpContextAccessor));

                HttpContext context = ((HttpContextAccessor)factory).HttpContext;
                // context.Response.WriteAsync("Test");

                return context;
            }
        }
    }
}