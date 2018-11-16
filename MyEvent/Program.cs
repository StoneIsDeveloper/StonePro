using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvent
{
    class Program
    {
        static void Main(string[] args)
        {
            // 事件
            MailManager manager = new MailManager();
            Fax fax = new Fax(manager); //构造函数 登记方法
            manager.SimulateNewMail("stone", "jack", "hello");

            // 显示定义事件
            TypeWithLotsOfEvents twle = new TypeWithLotsOfEvents();
            twle.Foo += HandleFooEvent;  // 登记方法
            twle.Foo += HandTwo;

            twle.SimulateFoo();

            Console.ReadKey();

        }

        private static void HandTwo(object sender, FooEventArgs e)
        {
            Console.WriteLine("22 Handing Foo Event here...");

        }

        private static void HandleFooEvent(object sender, FooEventArgs e)
        {
            Console.WriteLine("11 Handing Foo Event here...");
        }

        


    }
}
