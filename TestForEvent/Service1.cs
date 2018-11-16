using MyEvent;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace TestForEvent
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            TypeWithLotsOfEvents twle = new TypeWithLotsOfEvents();
            twle.Foo += HandleFooEvent;  // 登记方法

            twle.SimulateFoo();

        }

        private void OnEvent()
        {
            TypeWithLotsOfEvents twle = new TypeWithLotsOfEvents();
            twle.Foo += HandleFooEvent;  // 登记方法

            twle.SimulateFoo();

        }

        private void HandleFooEvent(object sender, FooEventArgs e)
        {
            
        }

        protected override void OnStop()
        {
        }
    }
}
