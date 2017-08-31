using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.mq.message
{
    public class MqEventMessage
    {

        public bool IsOperationOK { set; get; }

        public int Id { set; get; }

        public MqEventType EventType { set; get; }

        public string EventKey { set; get; }

        public string EventSource { set; get; }

        public string Body { set; get; }
    }



    public static class MaEventMessageExt
    {

        public static MqEventMessage BuildMsgResult(this MqEventMessage message, byte[] body)
        {
            var json = Encoding.UTF8.GetString(body);
            var msg = JsonConvert.DeserializeObject<MqEventMessage>(json);
            msg.IsOperationOK = true;
            return msg;
        }

        /*
        public static EventMessage Default()
        {
            return new EventMessage()
            {
                Id = DateTime.Now.Millisecond,
                IsOperationOK = false
            };
        }*/




    }

    public enum MqEventType
    {
        /// <summary>
        /// 同步数据
        /// </summary>
        Sync,
        /// <summary>
        /// 发生短信
        /// </summary>
        SMS,
        /// <summary>
        /// 发生邮件
        /// </summary>
        EMail,
        /// <summary>
        /// 推送微信
        /// </summary>
        Outsite,
        /// <summary>
        /// 推送微信
        /// </summary>
        ReplyWx,
        /// <summary>
        /// 推送微信
        /// </summary>
        PushWx,
        /// <summary>
        /// 新添店铺
        /// </summary>
        StoreQr,
        /// <summary>
        /// 扫描店铺二维码
        /// </summary>
        ScanQr,
    }
}
