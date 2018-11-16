using System;
using System.Threading;

namespace MyEvent
{
    internal class MailManager
    {
        // 事件成员
        public event EventHandler<NewMailEventAgrs> NewMail;

        //第三步：定义负责引发引发事件的方法来通知已登记的对象
        protected virtual void OnNewMail(NewMailEventAgrs e)
        {
            //EventHandler<NewMailEventAgrs> temp = Volatile.Read(ref NewMail);
            //if (temp != null)
            //    temp(this, e);

            // 线程安全的方式，调用委托
            Volatile.Read(ref NewMail)?.Invoke(this, e);

        }

        //第四部：定义方法将输入转化为期望事件
        public void SimulateNewMail(string from, string to,string subject)
        {
            //构造一个对象来，把数据 传给 知接收者
            NewMailEventAgrs e = new NewMailEventAgrs(from, to,subject);

            // 调用虚方法通知事件已发生，默认 通知所有登记的对象
            OnNewMail(e);
        }

    }
}
