using Igmite.Lighthouse.Models;
using NLog;
using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public sealed class QuartzLogger
    {
        public static string ErrorFilePath { get; set; }

        public QuartzLogger()
        {
            //file name to be created
            string fileName = string.Format("Lighthouse-Quartz-Errors-{0}.txt", Constants.GetCurrentDateTime.ToString("yyyyMMddHH"));

            //file will created in this path
            ErrorFilePath = Path.Combine(Constants.RootPath, "Logs", fileName);
        }

        private static readonly object locker = new object();

        private static QuartzLogger instance = null;

        public static QuartzLogger Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new QuartzLogger();
                        }
                    }
                }

                return instance;
            }
        }

        public void WriteErrorLogsInFile(string text)
        {
            lock (locker)
            {
                using (FileStream file = new FileStream(ErrorFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
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
                using (FileStream file = new FileStream(ErrorFilePath, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (StreamWriter writer = new StreamWriter(file, Encoding.Unicode))
                {
                    StringBuilder errorLogs = this.GetExceptionDetails(exception, message);
                    writer.Write(errorLogs.ToString());
                }
            }
        }

        public StringBuilder GetExceptionDetails(Exception ex, string message = "")
        {
            var errorMessages = new StringBuilder();
            errorMessages.AppendFormat("{0}", Environment.NewLine);

            string targetSiteName = string.IsNullOrEmpty(ex.TargetSite.Name) ? String.Empty : ex.TargetSite.Name;

            if (!string.IsNullOrEmpty(message))
                errorMessages.AppendFormat("User Message   : {0}{1}", message, Environment.NewLine);

            errorMessages.AppendFormat("Date           : {0}{1}", Constants.GetCurrentDateTime, Environment.NewLine);
            errorMessages.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, targetSiteName, Environment.NewLine);
            errorMessages.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);

            errorMessages.Append(this.GetStackTrace(ex));
            errorMessages.Append(Environment.NewLine);

            return errorMessages;
        }

        private string GetStackTrace(Exception ex)
        {
            var sbStackTrace = new StringBuilder();

            string[] stackTraces = ex.StackTrace.Split('\n').Take(15).ToArray();
            string targetSiteName = string.IsNullOrEmpty(ex.TargetSite.Name) ? String.Empty : ex.TargetSite.Name;

            sbStackTrace.AppendFormat("StackTrace     : {0}", Environment.NewLine);

            foreach (var stackTrace in stackTraces)
                sbStackTrace.AppendFormat("                 {0}{1}", stackTrace.Trim(), Environment.NewLine);

            while (ex.InnerException != null)
            {
                ex = ex.InnerException;

                sbStackTrace.AppendFormat("Caused By{0}", Environment.NewLine);
                sbStackTrace.AppendFormat("Error Type     : {0} -> {1}{2}", ex.GetType().Name, targetSiteName, Environment.NewLine);
                sbStackTrace.AppendFormat("Error Message  : {0}{1}", ex.Message, Environment.NewLine);
                sbStackTrace.AppendFormat("StackTrace     : {0}", Environment.NewLine);

                stackTraces = ex.StackTrace.Split('\n').Take(15).ToArray();

                foreach (var stackTrace in stackTraces)
                    sbStackTrace.AppendFormat("                 {0}{1}", stackTrace.Trim(), Environment.NewLine);

                sbStackTrace.AppendFormat(Environment.NewLine);
            }

            return sbStackTrace.ToString();
        }
    }
}
