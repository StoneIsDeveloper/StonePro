using Quartz;
using Quartz.Impl;
using QueartzDemo._01_SimpleJobScheduler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuartzDemo
{
    class Program
    {
        static  void Main(string[] args)
        {
           // var re = CodeMethodAsync();
           // Console.ReadKey();

            var res =  new SimpleJobSchedulerExample().Run();
            Console.ReadKey();
        }

        private static async Task CodeMethodAsync()
        {
            Console.WriteLine(DateTime.Now.ToShortTimeString());
            // 1.创建一个任务调度池
            ISchedulerFactory schedulerFactory = new StdSchedulerFactory();
            IScheduler scheduler = await  schedulerFactory.GetScheduler();

            // 2.创建一个具体的作业
            IJobDetail job = JobBuilder.Create<JobDemo>().Build();

            // 3.创建一个出发器
            var trigger = TriggerBuilder.Create()
                            .WithSimpleSchedule(x => x.WithIntervalInSeconds(2).RepeatForever())
                            .Build();

            // 4.加入作业调度池中 
            await scheduler.ScheduleJob(job, trigger);

            //5.开始运行
            await scheduler.Start();

        }
    }
}
