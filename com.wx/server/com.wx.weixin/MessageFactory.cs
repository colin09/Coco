using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.weixin.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace com.wx.weixin
{
    public class MessageFactory
    {
        protected static ILog log = LocatorFacade.Current.Resolve<ILog>();

        public MessageFactory()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }


        private static List<BaseMsg> _queue;


        public static bool CheckMessage(string xml)
        {
            //log.Info(": CheckMessage start - - - ");

            bool flag = false;
            if (_queue == null)
            {
                _queue = new List<BaseMsg>();
            }
            else if (_queue.Count >= 50)
            {
                _queue = _queue.Where(q => { return q.CreateTime.AddSeconds(20) > DateTime.Now; }).ToList();//保留20秒内未响应的消息
            }

            //log.Info(": Read Message start - - - ");

            XElement xdoc = XElement.Parse(xml);
            var FromUserName = xdoc.Element("FromUserName").Value;
            var CreateTime = xdoc.Element("CreateTime").Value;
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();

            MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);

            var MsgId = "0";
            if (type != MsgType.EVENT)/* 事件消息无 MsgId */
                MsgId = xdoc.Element("MsgId").Value;

            log.Info(": FromUserName - " + FromUserName);
            log.Info(": CreateTime - " + CreateTime);
            log.Info(": MsgType - " + msgtype);
            log.Info(": MsgId - " + MsgId);

            string CheckID = MsgId;
            if (type == MsgType.EVENT)
                CheckID = CreateTime + "|" + FromUserName;

            if (_queue.FirstOrDefault(m => { return m.MsgFlag == CheckID; }) == null)
            {
                _queue.Add(new BaseMsg
                {
                    CreateTime = DateTime.Now,
                    FromUser = FromUserName,
                    MsgFlag = MsgId
                });
            }
            else flag = true;
            return flag;
        }


        public static BaseMessage CreateMessage(string xml)
        {
            XElement xdoc = XElement.Parse(xml);
            var msgtype = xdoc.Element("MsgType").Value.ToUpper();
            MsgType type = (MsgType)Enum.Parse(typeof(MsgType), msgtype);
            switch (type)
            {
                case MsgType.TEXT: return MessageHelper.ConvertObj<TextMessage>(xml);
                case MsgType.IMAGE: return MessageHelper.ConvertObj<ImgMessage>(xml);
                case MsgType.VIDEO: return MessageHelper.ConvertObj<VideoMessage>(xml);
                case MsgType.VOICE: return MessageHelper.ConvertObj<VoiceMessage>(xml);
                case MsgType.LINK:
                    return MessageHelper.ConvertObj<LinkMessage>(xml);
                case MsgType.LOCATION:
                    return MessageHelper.ConvertObj<LocationMessage>(xml);
                case MsgType.EVENT://事件类型
                    {
                        var eventtype = (MsgEvent)Enum.Parse(typeof(MsgEvent), xdoc.Element("Event").Value.ToUpper());
                        switch (eventtype)
                        {
                            case MsgEvent.CLICK:
                                return MessageHelper.ConvertObj<NormalMenuEventMessage>(xml);
                            case MsgEvent.VIEW: return MessageHelper.ConvertObj<NormalMenuEventMessage>(xml);
                            case MsgEvent.LOCATION: return MessageHelper.ConvertObj<LocationEventMessage>(xml);
                            case MsgEvent.LOCATION_SELECT: return MessageHelper.ConvertObj<LocationMenuEventMessage>(xml);
                            case MsgEvent.SCAN: return MessageHelper.ConvertObj<ScanEventMessage>(xml);
                            case MsgEvent.SUBSCRIBE: return MessageHelper.ConvertObj<SubEventMessage>(xml);
                            case MsgEvent.UNSUBSCRIBE: return MessageHelper.ConvertObj<UnSubEventMessage>(xml);
                            case MsgEvent.SCANCODE_WAITMSG: return MessageHelper.ConvertObj<ScanMenuEventMessage>(xml);
                            case MsgEvent.TEMPLATESENDJOBFINISH: return MessageHelper.ConvertObj<SendJobFinishEventMessage>(xml);
                            default:
                                return MessageHelper.ConvertObj<EventMessage>(xml);
                        }
                    } break;
                default:
                    return MessageHelper.ConvertObj<BaseMessage>(xml);
            }
        }
    }





    public class BaseMsg
    {
        /// <summary>
        /// 发送者标识
        /// </summary>
        public string FromUser { get; set; }
        /// <summary>
        /// 消息表示。普通消息时，为msgid，事件消息时，为事件的创建时间
        /// </summary>
        public string MsgFlag { get; set; }
        /// <summary>
        /// 添加到队列的时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }


}
