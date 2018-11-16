using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyDelegate
{
    // 声明一个委托类型，它的实例引用一个方法
    // 该方法获取一个Int32参数，返回void
    internal delegate void Feedback(Int32 value);

    public sealed class Program
    {
        static void Main(string[] args)
        {
            StaticDelegateDemo();
            InstanceDelegateDemo();
            ChainDelegateDemo1(new Program());
            ChainDelegateDemo2(new Program());
            Console.ReadKey();
        }

        public static void StaticDelegateDemo()
        {
            Console.WriteLine("-----Static Delegate Demo------");
            Counter(1, 3, null);
            Counter(1, 3, new Feedback(FeedbackToConsole)); 
            Counter(1, 3, new Feedback(FeedbackToMsfBox));
            Console.WriteLine();
        }


        private static void InstanceDelegateDemo()
        {
            Console.WriteLine("-----Instance Delegate Demo------");
            Program p = new Program();
            Counter(1, 3, new Feedback(p.FeedbackToFile));
            Console.WriteLine();
        }

        private static void ChainDelegateDemo1(Program p)
        {
            Console.WriteLine("----- Chain Delegate Demo 1 ------");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(FeedbackToMsfBox);
            Feedback fb3 = new Feedback(p.FeedbackToFile);

            Feedback fbChain = null;
            fbChain =   (Feedback)Delegate.Combine(fbChain,fb1);
            fbChain = (Feedback)Delegate.Combine(fbChain, fb2);
            fbChain = (Feedback)Delegate.Combine(fbChain, fb3);
            Counter(1, 2, fbChain);

            Console.WriteLine();
            fbChain = (Feedback)Delegate.Remove(fbChain, new Feedback(FeedbackToMsfBox));

            Counter(1, 2, fbChain);

        }

        private static void ChainDelegateDemo2(Program p)
        {
            Console.WriteLine("----- Chain Delegate Demo 2 ------");
            Feedback fb1 = new Feedback(FeedbackToConsole);
            Feedback fb2 = new Feedback(FeedbackToMsfBox);
            Feedback fb3 = new Feedback(p.FeedbackToFile);

            Feedback fbChain = null;
            fbChain += fb1;
            fbChain += fb2;
            fbChain += fb3;
            Counter(1, 2, fbChain);

            Console.WriteLine();
            fbChain -= new Feedback(FeedbackToMsfBox);

            Counter(1, 2, fbChain);

        }


        private static void Counter(int from, int to, Feedback fb)
        {
            for(Int32 val = from; val <= to; val++)
            {
                fb?.Invoke(val);
            }
        }

        private static void FeedbackToConsole(Int32 value)
        {
            Console.WriteLine($"Console Item={value}");
        }
         
        private static void FeedbackToMsfBox(Int32 value)
        {
           // MessageBox.Show($"Item={value}");
            Console.WriteLine($"Msg Box Item={value}");
        }

        private void FeedbackToFile(Int32 value)
        {
            using (StreamWriter sw = new StreamWriter("Status",true))
            {
                sw.WriteLine($"Item={value}");
            }
            Console.WriteLine($"File Item={value}");

        }

    }
}
