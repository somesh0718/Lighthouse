using Igmite.Lighthouse.Logging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace Igmite.Lighthouse.Services.Security
{
    public static class ExceptionHandler
    {
        public static void UseApiExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    //if any exception then report it and log it
                    if (contextFeature != null)
                    {
                        //Technical Exception for troubleshooting
                        //var logger = loggerFactory.CreateLogger("GlobalException");
                        //logger.LogError($"Something went wrong: {contextFeature.Error}");

                        ErrorManager.Instance.WriteErrorLogsInFile(contextFeature.Error);

                        string errorMessage = string.Format("StatusCode: {0}    Message: {1}", context.Response.StatusCode, contextFeature.Error.Message);

                        //Business exception - exit gracefully
                        await context.Response.WriteAsync(errorMessage);
                        //Message = "Something went wrongs.Please try again later"
                    }
                });
            });
        }
    }
}