using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEvent
{
    internal sealed class Fax
    {
        public Fax(MailManager mm)
        {
            //构造函数 登记方法
            mm.NewMail += FaxMsg;
        }

        private void FaxMsg(object sender, NewMailEventAgrs e)
        {
            // sender表示MailManager对象
            Console.WriteLine("Faxing mail message");
            Console.WriteLine($"From={e.From}, To={e.To}, Subject={e.Subject};");
        }

        //注销对NewMail事件的关注
        public void Unregister(MailManager mm)
        {
            mm.NewMail -= FaxMsg;
        }
    }
}
