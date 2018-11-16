using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvent
{
    public class FooEventArgs : EventArgs { }

    public class TypeWithLotsOfEvents
    {
        protected EventSet EventSet { get; } = new EventSet();

        // 构造一个静态只读对象来标识这个事件
        protected static readonly EventKey s_fooEventKey = new EventKey();

        // 定义事件的防卫器方法，用于在集合中增删委托
        public event EventHandler<FooEventArgs> Foo
        {
            add { EventSet.Add(s_fooEventKey, value);  }
            remove { EventSet.Remove(s_fooEventKey, value);   }
        }

        // 为这个事件定义受保护的虚方法
        protected virtual void OnFoo(FooEventArgs e)
        {
            EventSet.Raise(s_fooEventKey, this, e);
        }

        // 定义将输入转换成这个事件的方法
        public void SimulateFoo()
        {
            OnFoo(new FooEventArgs());
        }

    }
}
