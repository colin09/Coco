using com.wx.common.helper;
using com.wx.mq.client;
using com.wx.mq.message;
using System;

namespace com.wx.listener
{
    public class Utility
    {
        public static string RequestString(string name)
        {
            System.Web.HttpRequest Request = System.Web.HttpContext.Current.Request;
            string v = Request[name];
            if (v == null)
            {
                v = string.Empty;
            }
            return v;
        }


        public static void NotifyWxMessage(string key,Array array)
        {
            var msg = new MqEventMessage()
            {
                Id = DateTime.Now.Millisecond,
                IsOperationOK = false,
                EventType = MqEventType.ReplyWx,
                EventKey = "reply",
                EventSource = DateTime.Now.ToString(),
                Body = array.ToJson()
            };

            MqClient client = new MqClient();
            client.TriggerEventMessage(msg);
        }

    }
}