using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MyEvent
{
    public sealed class EventKey { }

    public sealed class EventSet
    {
        private readonly Dictionary<EventKey, Delegate> m_events = new Dictionary<EventKey, Delegate>();

        // 添加 EventKey -> Delegate 映射(如果EventKey不存在)，
        // 或者将委托和现有的EventKey合并
        public void Add(EventKey eventKey,Delegate handler)
        {
            // Monitor: 提供同步对象访问的机制。
            // Monitor.Enter: 加锁
            Monitor.Enter(m_events);
            Delegate d;
            m_events.TryGetValue(eventKey, out d);
            m_events[eventKey] = Delegate.Combine(d, handler);
            
            // 释放锁
            Monitor.Exit(m_events);
        }

        public void Remove(EventKey eventKey,Delegate handler)
        {
            Monitor.Enter(m_events);
            // 调用TyeGetValue,避免异常
            Delegate d;
            if(m_events.TryGetValue(eventKey,out d))
            {
                if (d != null)
                    m_events[eventKey] = d;
                else
                    m_events.Remove(eventKey);
            }
            Monitor.Exit(m_events);
        }

        // 为指定的EventKey引发事件
        public void Raise(EventKey eventKey,Object sender,EventArgs e)
        {
            // 如果EventKey不在集合中，不抛出异常
            Delegate d;
            Monitor.Enter(m_events);
            m_events.TryGetValue(eventKey, out d);
            Monitor.Exit(m_events);

            if (d != null)
            {
                //由于委托类型的不确定性，使用动态绑定并调用
                d.DynamicInvoke(new Object[] { sender, e });
            }

        }

    }
}
