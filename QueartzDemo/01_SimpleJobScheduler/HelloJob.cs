using Quartz;
using Quartz.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueartzDemo._01_SimpleJobScheduler
{
    public class HelloJob : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"HelloJ ob Run:{DateTime.Now.ToString()}");
            });

        }
    }
}
