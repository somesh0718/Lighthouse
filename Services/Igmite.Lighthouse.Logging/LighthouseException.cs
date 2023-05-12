using System;

namespace Igmite.Lighthouse.Logging
{
    public class LighthouseException : Exception
    {
        public LighthouseException()
        {
            Console.WriteLine("test");
        }

        public LighthouseException(string message)
            : base(message)
        {
            Console.WriteLine(message);
        }

        public LighthouseException(string message, Exception innerException)
           : base(message, innerException)
        {
            ErrorManager.Instance.WriteErrorLogsInFile(innerException, message);
        }
    }
}