using Quartz;
using Quartz.Impl;
using Quartz.Logging;
using QueartzDemo.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueartzDemo._01_SimpleJobScheduler
{
    public class SimpleJobSchedulerExample : IExample
    {
        public virtual async Task Run()
        {
            // ILog log = LogProvider.GetLogger(typeof(SimpleJobSchedulerExample));
            Console.WriteLine("----- Initializing ----");

            ISchedulerFactory sf = new StdSchedulerFactory();
            IScheduler sched = await sf.GetScheduler();
            Console.WriteLine("----- Initializaton Complete ----");

            DateTimeOffset runTime = DateBuilder.EvenMinuteDate(DateTimeOffset.UtcNow);
            Console.WriteLine("-----  Scheduling Job  ----");

            // define the job and tie it to our hellojob class
            IJobDetail job = JobBuilder.Create<HelloJob>()
                                .WithIdentity("job1", "group1")
                                .Build();
            //ITrigger trigger = TriggerBuilder.Create()
            //                    .WithIdentity("trigger1", "gruop1")
            //                    .Build();
            ICronTrigger trigger = (ICronTrigger)TriggerBuilder.Create()
              .WithIdentity("trigger1", "group1")
              .WithCronSchedule("0 40 22 * * ?")
              .Build();


            // tell quartz to schedule the job using our trigger
            await sched.ScheduleJob(job, trigger);
            Console.WriteLine($"{job.Key} will run at:{runTime:r}");

            await sched.Start();
            Console.WriteLine("-------- Started Scheduler --------");

        }
    }
}
