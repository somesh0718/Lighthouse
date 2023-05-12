using Quartz;
using Quartz.Spi;
using System;

namespace Igmite.Lighthouse.Services.HostServices.Helpers
{
    public class QuartzJobFactory : IJobFactory
    {
        protected readonly IServiceProvider _serviceProvider;
        public QuartzJobFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IJob NewJob(TriggerFiredBundle bundle, IScheduler scheduler)
        {
            try
            {
                return _serviceProvider.GetService(bundle.JobDetail.JobType) as IJob;
            }
            catch (Exception ex)
            {
                throw new QuartzConfigurationException(ex.Message);
            }
        }

        public void ReturnJob(IJob job)
        {
            var obj = job as IDisposable;
            obj?.Dispose();
        }
    }
}