using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.Services.Core.Api.Scheduler
{

    public class HandleScheduler
    {
        /*
         Every Second : * * * ? * *
            Every Minute : 0 * * ? * *
            Every Day noon 12 pm : 0 0 12 * * ?
         */
        private static readonly string ScheduleCronExpression = "0 * * ? * *";

        public static async System.Threading.Tasks.Task StartAsync(IServiceProvider serviceProvider)
        {
            try
            {
                //var scheduler = await StdSchedulerFactory.GetDefaultScheduler();
                //if (!scheduler.IsStarted)
                //{
                //    await scheduler.Start();
                //}
                //var job1 = JobBuilder.Create<DemoJob>().WithIdentity("ExecuteTaskServiceCallJob1", "group1").Build();
                //var trigger1 = TriggerBuilder.Create().WithIdentity("ExecuteTaskServiceCallTrigger1", "group1").WithCronSchedule(ScheduleCronExpression).Build();
                //await scheduler.ScheduleJob(job1, trigger1);
                // var serviceCollection = new ServiceCollection();
                //var serviceProvider = serviceCollection.BuildServiceProvider();

                var props = new NameValueCollection
                {
                    { "quartz.serializer.type", "binary" }
                };

                // var serviceCollection = new Microsoft.Extensions.DependencyInjection.ServiceCollection();
                //var serviceProvider = serviceCollection.BuildServiceProvider();
                var factory = new StdSchedulerFactory(props);
                var sched = await factory.GetScheduler();
                await sched.Start();
                var job = JobBuilder.Create<DemoJob>()
                    .WithIdentity("myJob", "group1")
                    .Build();
                sched.JobFactory = new Task1(serviceProvider);


                //var trigger = TriggerBuilder.Create()
                //     .WithIdentity("trigger3", "group1")
                //     .WithCronSchedule("0 */5 * * * ?")
                //     .ForJob("myJob", "group1")
                //     .Build();

                var trigger = TriggerBuilder.Create()
                    .WithIdentity("myTrigger", "group2")
                    .StartNow()
                    .WithSimpleSchedule(x => x
                        .WithIntervalInMinutes(1)
                        //  .WithCronSchedule("0/5 * * * * ?")
                        .RepeatForever())
                .Build();
                await sched.ScheduleJob(job, trigger);
            }
            catch (Exception ex) { }
        }
    }
}
