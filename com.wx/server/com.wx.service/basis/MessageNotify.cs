using com.wx.common.helper;
using com.wx.mq.client;
using com.wx.mq.message;
using System;

namespace com.wx.service.basis
{
    public class MessageNotify00
    {

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



        public static void NotifyOutsiteAccount(Array array)
        {
            NotifyMessage(MqEventType.Outsite, "", array);
        }
        public static void NotifyStoreQrCode(Array array)
        {
            NotifyMessage(MqEventType.StoreQr, "", array);
        }
    }
}
