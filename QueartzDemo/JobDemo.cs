using System;
using System.Threading.Tasks;
using Quartz;

namespace QuartzDemo
{
    public class JobDemo : IJob
    {
        public virtual Task Execute(IJobExecutionContext context)
        {
            return Task.Run(() =>
            {
                Console.WriteLine($"JobDemo Run:{DateTime.Now.ToString()}");
            });
        }
    }
}