using com.wx.common.helper;
using com.wx.mq.client;
using com.wx.mq.message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.weixin.helper
{
    public class MessageNotify000000000001
    {

        public static void NotifyOutsiteAccount(Array array)
        {
            NotifyMessage(MqEventType.Outsite, "", array);
        }

        public static void NotifyMessage(MqEventType eventType, string key, Array array)
        {
            var msg = new MqEventMessage()
            {
                Id = DateTime.Now.Millisecond,
                IsOperationOK = false,
                EventType = eventType,
                EventKey = key,
                EventSource = DateTime.Now.ToString(),
                Body = array.ToJson()
            };

            MqClient client = new MqClient();
            client.TriggerEventMessage(msg);
        }
    }
}
