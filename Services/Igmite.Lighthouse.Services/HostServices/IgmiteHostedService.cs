using Igmite.Lighthouse.BAL;
using Igmite.Lighthouse.BAL.Providers;
using Igmite.Lighthouse.DAL;
using Igmite.Lighthouse.DAL.EF;
using Igmite.Lighthouse.Models;
using Igmite.Lighthouse.Services.HostServices.Helpers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace Igmite.Lighthouse.Services.HostServices
{
    public class IgmiteHostedService : IHostedService
    {
        private DateTime startTime = Constants.GetCurrentDateTime;

        public IgmiteHostedService()
        {
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            try
            {
                IScheduler scheduler = await GetScheduler();

                var serviceProvider = GetConfiguredServiceProvider();
                scheduler.JobFactory = new QuartzJobFactory(serviceProvider);

                await scheduler.Start();
                await ConfigureDailyDashboardJob(scheduler);
                await ConfigureDailyNotSubmittedReportByVTJob(scheduler);
                await ConfigureWeeklyAttendanceMessageJob(scheduler);

                //https://www.freeformatter.com/cron-expression-generator-quartz.html
                //await ConfigureDailyJob(scheduler);
                //await ConfigureWeeklyJob(scheduler);
                //await ConfigureMonthlyJob(scheduler);
            }
            catch (Exception ex)
            {
                QuartzLogger.Instance.WriteErrorLogsInFile(ex);
            }
        }

        private async Task ConfigureDailyDashboardJob(IScheduler scheduler)
        {
            IJobDetail dailyDashboardJob = GetDailyDashboardJob();
            ITrigger dailyDashboardJobTrigger = GetDailyDashboardJobTrigger();

            if (await scheduler.CheckExists(dailyDashboardJob.Key))
            {
                await scheduler.RescheduleJob(dailyDashboardJobTrigger.Key, dailyDashboardJobTrigger);
                await scheduler.ResumeJob(dailyDashboardJob.Key);

                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", dailyDashboardJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(dailyDashboardJob, dailyDashboardJobTrigger);
            }
        }

        private async Task ConfigureDailyNotSubmittedReportByVTJob(IScheduler scheduler)
        {
            IJobDetail dailyNotSubmittedReportByVTJob = GetDailyNotSubmittedReportByVTJob();
            ITrigger dailyNotSubmittedReportByVTJobTrigger = GetDailyNotSubmittedReportByVTJobTrigger();

            if (await scheduler.CheckExists(dailyNotSubmittedReportByVTJob.Key))
            {
                await scheduler.RescheduleJob(dailyNotSubmittedReportByVTJobTrigger.Key, dailyNotSubmittedReportByVTJobTrigger);
                await scheduler.ResumeJob(dailyNotSubmittedReportByVTJob.Key);

                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", dailyNotSubmittedReportByVTJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(dailyNotSubmittedReportByVTJob, dailyNotSubmittedReportByVTJobTrigger);
            }
        }

        private async Task ConfigureWeeklyAttendanceMessageJob(IScheduler scheduler)
        {
            IJobDetail weeklyAttendanceMessageJob = GetWeeklyAttendanceMessageJob();
            ITrigger weeklyAttendanceMessageJobTrigger = GetWeeklyAttendanceMessageJobTrigger();

            if (await scheduler.CheckExists(weeklyAttendanceMessageJob.Key))
            {
                await scheduler.RescheduleJob(weeklyAttendanceMessageJobTrigger.Key, weeklyAttendanceMessageJobTrigger);
                await scheduler.ResumeJob(weeklyAttendanceMessageJob.Key);

                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", weeklyAttendanceMessageJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(weeklyAttendanceMessageJob, GetWeeklyAttendanceMessageJobTrigger());
            }
        }

        private async Task ConfigureDailyJob(IScheduler scheduler)
        {
            var dailyJob = GetDailyJob();
            if (await scheduler.CheckExists(dailyJob.Key))
            {
                await scheduler.ResumeJob(dailyJob.Key);
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", dailyJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(dailyJob, GetDailyJobTrigger());
            }
        }

        private async Task ConfigureWeeklyJob(IScheduler scheduler)
        {
            var weklyJob = GetWeeklyJob();
            if (await scheduler.CheckExists(weklyJob.Key))
            {
                await scheduler.ResumeJob(weklyJob.Key);
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", weklyJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(weklyJob, GetWeeklyJobTrigger());
            }
        }

        private async Task ConfigureMonthlyJob(IScheduler scheduler)
        {
            var monthlyJob = GetMonthlyJob();
            if (await scheduler.CheckExists(monthlyJob.Key))
            {
                await scheduler.ResumeJob(monthlyJob.Key);
                QuartzLogger.Instance.WriteErrorLogsInFile(string.Format("The job key {0} was already existed, thus resuming the same", monthlyJob.Key));
            }
            else
            {
                await scheduler.ScheduleJob(monthlyJob, GetMonthlyJobTrigger());
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        #region "Private Functions"

        private IServiceProvider GetConfiguredServiceProvider()
        {
            var services = new ServiceCollection();
            //.AddScoped<IDailyJob, DailyJob>()
            //.AddScoped<IWeeklyJob, WeeklyJob>()
            //.AddScoped<IMonthlyJob, MonthlyJob>()

            services.AddScoped<IDailyDashboardJob, DailyDashboardJob>();
            services.AddScoped<IDashboardJobService, DashboardJobService>();

            services.AddScoped<INotSubmittedReportJob, NotSubmittedReportJob>();
            services.AddScoped<INotSubmittedReportJobService, NotSubmittedReportJobService>();

            services.AddScoped<IWeeklyAttendanceMessageJob, WeeklyAttendanceMessageJob>();
            services.AddScoped<IWeeklyAttendanceMessageJobService, WeeklyAttendanceMessageJobService>();

            services.AddScoped<IDashboardJobManager, DashboardJobManager>();
            services.AddScoped<IDashboardJobRepository, DashboardJobRepository>();

            services.AddScoped<ICommonManager, CommonManager>();
            services.AddScoped<ICommonRepository, CommonRepository>();

            return services.BuildServiceProvider();
        }

        private IJobDetail GetDailyDashboardJob()
        {
            return JobBuilder.Create<IDailyDashboardJob>()
                .WithIdentity("dailyDashboardJob", "dailyDashboardGroup")
                .Build();
        }

        private ITrigger GetDailyDashboardJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailyDashboardTrigger", "dailyDashboardGroup")
                 .StartAt(startTime)

                 // Scheduling Job for 'Summary Dashboard' - Fire two time every day - 1AM and 2AM  - "0 0 1,2 * * ?"
                 .WithSchedule(CronScheduleBuilder.CronSchedule(Constants.DashboardCronExpression).WithMisfireHandlingInstructionDoNothing())
                 //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/2 * ? * *").WithMisfireHandlingInstructionDoNothing())
                 .Build();
        }

        private IJobDetail GetDailyNotSubmittedReportByVTJob()
        {
            return JobBuilder.Create<IDailyDashboardJob>()
                .WithIdentity("dailyNotSubmittedReportByVTJob", "dailyNotSubmittedReportByVTGroup")
                .Build();
        }

        private ITrigger GetDailyNotSubmittedReportByVTJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailyNotSubmittedReportByVTTrigger", "dailyNotSubmittedReportByVTGroup")
                 .StartAt(startTime)

                 // Scheduling Job for 'Not Submitted Report By VT' - Fire two time last 2 days in the month - 3AM and 4AM  - "0 0 3,4 L-2 * ?"
                 .WithSchedule(CronScheduleBuilder.CronSchedule(Constants.NotReportingVTCronExpression).WithMisfireHandlingInstructionDoNothing())
                 //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/3 * ? * *").WithMisfireHandlingInstructionDoNothing())
                 .Build();
        }

        private IJobDetail GetWeeklyAttendanceMessageJob()
        {
            return JobBuilder.Create<IWeeklyAttendanceMessageJob>()
                .WithIdentity("weeklyAttendanceMessageJob", "weeklyAttendanceMessageGroup")
                .Build();
        }

        private ITrigger GetWeeklyAttendanceMessageJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("weeklyAttendanceMessageTrigger", "weeklyAttendanceMessageGroup")
                 .StartAt(startTime)

                 // Scheduling Job for 'Weekly Attendance Messages' - Fire every monday - 1AM and 2AM  - "0 0 1,2 * * ?"
                 .WithSchedule(CronScheduleBuilder.CronSchedule(Constants.WeeklyAttendanceCronExpression).WithMisfireHandlingInstructionDoNothing())
                 //.WithSchedule(CronScheduleBuilder.CronSchedule("0 0/5 * ? * *").WithMisfireHandlingInstructionDoNothing())
                 .Build();
        }

        private IJobDetail GetDailyJob()
        {
            return JobBuilder.Create<IDailyJob>()
                .WithIdentity("dailyJob", "dailyGroup")
                .Build();
        }

        private ITrigger GetDailyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("dailyTrigger", "dailyGroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInHours(24)
                     .RepeatForever())
                 .Build();
        }

        private IJobDetail GetWeeklyJob()
        {
            return JobBuilder.Create<IWeeklyJob>()
                .WithIdentity("weeklyjob", "weeklygroup")
                .Build();
        }

        private ITrigger GetWeeklyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("weeklytrigger", "weeklygroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInHours(167)
                     .RepeatForever())
                 .Build();
        }

        private IJobDetail GetMonthlyJob()
        {
            return JobBuilder.Create<IMonthlyJob>()
                .WithIdentity("monthlyjob", "monthlygroup")
                .Build();
        }

        private ITrigger GetMonthlyJobTrigger()
        {
            return TriggerBuilder.Create()
                 .WithIdentity("monthlytrigger", "monthlygroup")
                 .StartNow()
                 .WithSimpleSchedule(x => x
                     .WithIntervalInHours(715)
                     .RepeatForever())
                 .Build();
        }

        private static async Task<IScheduler> GetScheduler()
        {
            // Comment this if you don't want to use database start
            var config = (NameValueCollection)ConfigurationManager.GetSection("quartz");
            var factory = new StdSchedulerFactory(config);
            // Comment this if you don't want to use database end

            // Uncomment this if you want to use RAM instead of database start
            //var props = new NameValueCollection { { "quartz.serializer.type", "binary" } };

            //var factory = new StdSchedulerFactory(props);
            // Uncomment this if you want to use RAM instead of database end
            var scheduler = await factory.GetScheduler();

            return scheduler;
        }

        #endregion "Private Functions"
    }
}