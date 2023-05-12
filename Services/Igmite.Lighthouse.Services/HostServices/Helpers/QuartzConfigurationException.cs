using System;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    [Serializable]
    public class QuartzConfigurationException : Exception
    {
        public QuartzConfigurationException()
        {
        }

        public QuartzConfigurationException(string exception) : base(exception)
        {
        }
    }
}