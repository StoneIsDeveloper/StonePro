using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using log4net;
using Quartz;

namespace QuartzManage
{
    public sealed class TestJob : IJob
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(TestJob));


        Task IJob.Execute(IJobExecutionContext context)
        {
            var task = new Task(() =>
            {
                _logger.InfoFormat("TestJob测试");
            });
            var task2 = Task.Factory.StartNew(() =>
            {

            });

           return task;

        }
    }
}
