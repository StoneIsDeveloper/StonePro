using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Base b = new Base();
            b.Dispose();               // 打印内容：Base's Dispose
            ((IDisposable)b).Dispose(); // 打印内容：Base's Dispose
            Console.WriteLine("---------------");

            Derived d = new Derived();
            d.Dispose();                // 打印内容：Derived's Dispose 
            ((IDisposable)d).Dispose(); // 打印内容：Derived's Dispose
            Console.WriteLine("---------------");

            b = new Derived();
            b.Dispose();             //  打印内容：Base's Dispose
            ((IDisposable)b).Dispose(); // Derived's Dispose 
            Console.ReadKey();
        }
    }
    class Base : IDisposable
    {
        public void Dispose()
        {
            Console.WriteLine("Base's Dispose");
        }
    }
    class Derived : Base , IDisposable
    {
        new public void Dispose()
        {
            Console.WriteLine("Derived's Dispose");
           // base.Dispose();  // 打印内容：Base's Dispose
        }
    }
}
