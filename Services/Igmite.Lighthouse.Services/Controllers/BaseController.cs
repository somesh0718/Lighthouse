using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Text;

namespace Igmite.Lighthouse.Services.Controllers
{
    //[IgmiteAuthorize]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {
            string authToken = string.Empty;
        }

        [NonAction]
        public string GetErrorMessages(Exception ex)
        {
            string errorMessage = string.Empty;

            if (ex.InnerException != null)
            {
                errorMessage = string.Format("{0}\n{1}", ex.Message, ex.InnerException.Message);
            }
            else
            {
                errorMessage = string.Format("{0}\n{1}", ex.Message, Logging.ErrorManager.Instance.GetExceptionDetails(ex));
            }

            return errorMessage;
        }

        [NonAction]
        public string ReadRequestBody()
        {
            HttpRequest httpRequest = HttpContext.Request;

            // Allows using several time the stream in ASP.Net Core
            // We only do this if the stream isn't *already* seekable, as EnableBuffering will create a new stream instance each time it's called
            if (!httpRequest.Body.CanSeek)
            {
                httpRequest.EnableBuffering();
            }

            string requestValues = string.Empty;
            httpRequest.Body.Position = 0;

            using (var reader = new StreamReader(httpRequest.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
            {
                requestValues = reader.ReadToEnd();
            }
            //httpRequest.Body.Position = 0;

            return requestValues;
        }
    }
}