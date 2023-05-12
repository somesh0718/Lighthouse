using Igmite.Lighthouse.Entities;
using Igmite.Lighthouse.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;

namespace Igmite.Lighthouse.Logging
{
    public sealed class ErrorManager
    {
        private ErrorManager()
        {
            //file name to be created
            string fileName = string.Format("Lighthouse-Errors-{0}.txt", Constants.GetCurrentDateTime.ToString("yyyyMMddHH"));

            //file will created in this path
            this.FilePath = Path.Combine(Constants.RootPath, "Logs", fileName);
        }

        private HttpContext httpContext = null;
        private static string extraSpace = "                 ";
        private static readonly object locker = new object();

        private static ErrorManager instance = null;

        public static ErrorManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new ErrorManager();
                        }
                    }
                }

                return instance;
            }
        }

        public string FilePath { get; set; }
        //private static object locker = new Object();

        public void WriteErrorLogsInFile(string text)
        {
            lock (locker)
            {
                using (FileStream file = new FileStream(this.FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    writer.Write(text.ToString() + "\n");
                }
            }
        }

        public void WriteErrorLogsInFile(Exception exception, string message = "")
        {
            lock (locker)
            {
                using (FileStream file = new FileStream(this.FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    StringBuilder errorLogs = this.GetExceptionDetails(exception, message);
                    writer.Write(errorLogs.ToString());
                }
            }
        }

        public void WriteErrorLogsInFile(dynamic exception)
        {
            lock (locker)
            {
                using (FileStream file = new FileStream(this.FilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    StringBuilder errorLogs = this.GetExceptionDetails(exception.Exception, exception.ErrorLocation);
                    writer.Write(errorLogs.ToString());
                }
            }
        }

        public void WriteErrorLogsInDatabase(dynamic exception)
        {
            //ExceptionManager exceptionManager = new ExceptionManager(new ExceptionRepository());
            ErrorLogModel errorLog = new ErrorLogModel();

            //Get a StackTrace object for the exception
            StackTrace stackTrace = new StackTrace(exception.Exception, true);

            //Get the first stack frame
            StackFrame stackFrame = stackTrace.GetFrame(0);

            errorLog.ErrorLogId = Guid.NewGuid();
            errorLog.ErrorCode = null;
            errorLog.ErrorSeverity = null;
            errorLog.ErrorState = null;
            errorLog.ErrorProcedure = null;
            errorLog.ErrorLine = stackFrame.GetFileLineNumber();
            errorLog.ErrorTime = Constants.GetCurrentDateTime;
            errorLog.ErrorType = exception.Exception.GetType().Name;
            errorLog.ErrorLocation = exception.ErrorLocation;
            errorLog.ErrorMessage = exception.Exception.Message;
            errorLog.StackTrace = this.GetStackTrace(exception.Exception);
            errorLog.ErrorStatus = RecordStatus.Pending.ToString();
            errorLog.IsResolved = false;
            errorLog.IsActive = true;

            //exceptionManager.SaveOrUpdateExceptionDetails(errorLog);
        }

        public StringBuilder GetExceptionDetails(Exception ex, string message = "")
        {
            var errorMessages = new StringBuilder();

            string targetSiteName = string.IsNullOrEmpty(ex.TargetSite.Name) ? String.Empty : ex.TargetSite.Name;

            errorMessages.AppendFormat("Date           : {0}{1}", Constants.GetCurrentDateTime, Environment.NewLine);
            if (this.httpContext != null)
            {
                errorMessages.AppendFormat("User By        : {0}{1}", this.httpContext.Items["AuthUserId"], Environment.NewLine);
                errorMessages.AppendFormat("AuthToken      : {0}{1}", this.httpContext.Request.Headers["Authorization"].ToString(), Environment.NewLine);

                string httpRequestBody = this.ReadRequestBody(this.httpContext);
                errorMessages.AppendFormat("Request Body   : {0}{1}", httpRequestBody, Environment.NewLine);
                errorMessages.AppendLine();
            }

            if (!string.IsNullOrEmpty(message))
            {
                errorMessages.AppendFormat("User Message  : {0}{1}", ex.Message, Environment.NewLine);
            }

            errorMessages.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, targetSiteName, Environment.NewLine);
            errorMessages.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);

            errorMessages.Append(this.GetStackTrace(ex));
            errorMessages.Append("---------------------------------------------------------------------------------------------------------");
            errorMessages.AppendFormat("{0}", Environment.NewLine);

            return errorMessages;
        }

        private string GetStackTrace(Exception ex)
        {
            int errorIndex = 0;
            var sbStackTrace = new StringBuilder();

            string[] stackTraces = ex.StackTrace.Split('\n').Take(15).ToArray();
            string targetSiteName = string.IsNullOrEmpty(ex.TargetSite.Name) ? String.Empty : ex.TargetSite.Name;

            sbStackTrace.AppendFormat("StackTrace     : {0}", string.Empty);

            foreach (var stackTrace in stackTraces)
            {
                sbStackTrace.AppendFormat("{0}{1}{2}", (errorIndex == 0 ? string.Empty : extraSpace), stackTrace.Trim(), Environment.NewLine);
                errorIndex += 1;
            }

            if (ex.InnerException == null)
                sbStackTrace.AppendFormat(Environment.NewLine);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                sbStackTrace.AppendFormat("Caused By{0}", Environment.NewLine);
                sbStackTrace.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, targetSiteName, Environment.NewLine);
                sbStackTrace.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);
                sbStackTrace.AppendFormat("StackTrace     : {0}", string.Empty);

                stackTraces = ex.StackTrace.Split('\n').Take(15).ToArray();

                errorIndex = 0;
                foreach (var stackTrace in stackTraces)
                {
                    sbStackTrace.AppendFormat("{0}{1}{2}", (errorIndex == 0 ? string.Empty : extraSpace), stackTrace.Trim(), Environment.NewLine);
                    errorIndex += 1;
                }

                sbStackTrace.AppendFormat(Environment.NewLine);
            }

            return sbStackTrace.ToString();
        }

        public string GetErrorMessages(Exception ex, HttpContext context = null)
        {
            this.httpContext = context;

            this.WriteErrorLogsInFile(ex);

            string errorMessage = this.GetLastErrorMessage(ex);
            return errorMessage;
        }

        private string GetErrorMessageDetails(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                if (ex.InnerException == null)
                {
                    break;
                }
            };

            int errorLineNo = 0;

            StackTrace stackTrace = new StackTrace(ex, true);
            if (stackTrace != null)
            {
                StackFrame stackFrame = stackTrace.GetFrame(0);

                errorLineNo = stackFrame != null ? stackFrame.GetFileLineNumber() : 0;
            }

            StringBuilder sbErrors = new StringBuilder();

            sbErrors.AppendFormat("Error Message: <span style=\"color: red;\">{0}</span><br />\n", ex.Message);
            sbErrors.AppendFormat("Method Name: {0}<br />\n", ex.TargetSite.Name);
            sbErrors.AppendFormat("Class Name: {0}<br />\n", ex.TargetSite.DeclaringType.FullName.Trim());
            sbErrors.AppendFormat("Line number: {0}<br />\n", errorLineNo);

            return sbErrors.ToString();
        }

        private string GetLastErrorMessage(Exception ex)
        {
            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                if (ex.InnerException == null)
                {
                    break;
                }
            };

            return ex.Message;
        }

        public string ReadRequestBody(HttpContext context)
        {
            // Allows using several time the stream in ASP.Net Core
            // We only do this if the stream isn't *already* seekable, as EnableBuffering will create a new stream instance each time it's called
            if (!context.Request.Body.CanSeek)
            {
                context.Request.EnableBuffering();
            }

            context.Request.Body.Position = 0;
            string requestBody = string.Empty;

            using (var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false))
            {
                requestBody = reader.ReadToEnd();
            }

            return requestBody;
        }
    }
}