using com.wx.common.config;
using com.wx.common.helper;
using com.wx.weixin.data;
using com.wx.weixin.helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.mq;

namespace com.wx.weixin
{
    public class MessageHelper
    {
        public static T ConvertObj<T>(string xmlstr)
        {
            //var log = LocatorFacade.Current.Resolve<ILog>();
            //log.Info("start load XMLElemeng......");
            XElement xdoc = XElement.Parse(xmlstr);
            var type = typeof(T);
            var t = Activator.CreateInstance<T>();
            foreach (XElement element in xdoc.Elements())
            {
                var pr = type.GetProperty(element.Name.ToString());
                //log.Info("element.Name：{0}，element.Value：{1}", pr.PropertyType.Name, element.Value);
                if (element.HasElements)
                {//这里主要是兼容微信新添加的菜单类型。nnd，竟然有子属性，所以这里就做了个子属性的处理
                    foreach (var ele in element.Elements())
                    {
                        pr = type.GetProperty(ele.Name.ToString());
                        pr.SetValue(t, Convert.ChangeType(ele.Value, pr.PropertyType), null);
                    }
                    continue;
                }
                if (pr.PropertyType.Name == "MsgType")//获取消息模型
                {
                    pr.SetValue(t, (MsgType)Enum.Parse(typeof(MsgType), element.Value.ToUpper()), null);
                    continue;
                }
                if (pr.PropertyType.Name == "Event")//获取事件类型。
                {
                    pr.SetValue(t, (MsgEvent)Enum.Parse(typeof(MsgEvent), element.Value.ToUpper()), null);
                    continue;
                }
                if (pr.PropertyType.Name == "MsgEvent")//获取事件类型。
                {
                    pr.SetValue(t, (MsgEvent)Enum.Parse(typeof(MsgEvent), element.Value.ToUpper()), null);
                    continue;
                }
                pr.SetValue(t, Convert.ChangeType(element.Value, pr.PropertyType), null);
            }
            return t;
        }



        public static string GetEncryptParam(string userOpenId)
        {
            string timestamp = Utility.GetSecond1970().ToString(), random = Utility.GetDataRandom();
            string[] ArrTmp = { userOpenId, timestamp, random };
            Array.Sort(ArrTmp);　　 //字典排序　

            string tmpStr = string.Join("", ArrTmp);
            tmpStr = CryptoHelper.SHA1Encrypt(tmpStr);

            string url = "?f=" + userOpenId + "&t=" + timestamp + "&r=" + random + "&msg=" + tmpStr.ToLower();

            return url;
        }
    }





    /// <summary>
    /// 文本
    /// </summary>
    public class TextMessage : BaseMessage
    {
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }

        public override string Response()
        {
            log.Info(": Create Message OK, Content by : " + Content);
            string resxml = "", reply = "coming soon...";
            switch (Content.ToUpper())
            {
                /*
                case "H":
                    reply = "创业团队正在为您精心策划，敬请期待...";
                    resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
                    break;
                case "S":
                    string item = string.Format(MessageReply.Message_News_Item, "【快克】", "复方氨酚烷胺胶囊（10粒装）", "", "http://baike.baidu.com/view/25554.htm");
                    resxml = string.Format(MessageReply.Message_News_Main, FromUserName, ToUserName, Utility.GetSecond1970(), "1", item);
                    break;
                case "G":
                    string sItem1 = string.Format(MessageReply.Message_News_Item, "【快克】", "复方氨酚烷胺胶囊（10粒装）", "", "http://baike.baidu.com/view/25554.htm");
                    string sItem2 = string.Format(MessageReply.Message_News_Item, "【快克】", "复方氨酚烷胺胶囊（10粒装）", "", "http://baike.baidu.com/view/25554.htm");
                    string sItem3 = string.Format(MessageReply.Message_News_Item, "【快克】", "复方氨酚烷胺胶囊（10粒装）", "", "");
                    string sItem4 = string.Format(MessageReply.Message_News_Item, "【快克】", "复方氨酚烷胺胶囊（10粒装）", "", "");
                    resxml = string.Format(MessageReply.Message_News_Main, FromUserName, ToUserName, Utility.GetSecond1970(), "1", sItem1 + sItem2 + sItem3 + sItem4);
                    break;
                case "A":
                    reply = "<a href='weixin://profile/gh_c095d0ec12a2'>关注我们</a>";
                    resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
                    break;
                case "C":
                    reply = "<a href='{0}'>我的账户</a>".Frmt(AppSettingConfig.ShopDomain + "user/Account/Center");
                    resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
                    break;
                */
                case "001":
                case "002":
                case "003":
                    break;
                default:
                    int accountNO = 1;
                    string recommendUser = "【系统】";

                    var lines = AppSettingConfig.WxFollowReply.Split('n');
                    StringBuilder str = new StringBuilder();
                    foreach (var line in lines)
                    {
                        str.Append(line).Append("\n\n");
                    }
                    reply = string.Format(str.ToString(), AppSettingConfig.WxOpenName, accountNO,
                        AppSettingConfig.ShopDomain + AppSettingConfig.SysHelpUrl, AppSettingConfig.ShopDomain + AppSettingConfig.JoinUsUrl);

                    break;
            }
            //log.Info(": ToUserName - " + ToUserName);
            resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
            return resxml.ToString();
        }
    }
    /// <summary>
    /// 图片
    /// </summary>
    public class ImgMessage : BaseMessage
    {
        /// <summary>
        /// 图片路径
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }

        public override string Response()
        {
            return "ImgMessage";
        }
    }
    /// <summary>
    /// 语音
    /// </summary>
    public class VoiceMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 格式
        /// </summary>
        public string Format { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音识别结果
        /// </summary>
        public string Recognition { get; set; }

        public override string Response()
        {
            return "VoiceMessage";
        }
    }
    /// <summary>
    /// 视频
    /// </summary>
    public class VideoMessage : BaseMessage
    {
        /// <summary>
        /// 缩略图ID
        /// </summary>
        public string ThumbMediaId { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 媒体ID
        /// </summary>
        public string MediaId { get; set; }

        public override string Response()
        {
            return "VideoMessage";
        }
    }
    /// <summary>
    /// 链接
    /// </summary>
    public class LinkMessage : BaseMessage
    {
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { get; set; }

        public override string Response()
        {
            return "LinkMessage";
        }
    }
    /// <summary>
    /// 地理位置信息
    /// </summary>
    public class LocationMessage : BaseMessage
    {
        /// <summary>
        /// 
        /// </summary>
        public string MsgId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Location_X { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Location_Y { get; set; }
        /// <summary>
        /// 比例尺
        /// </summary>
        public string Scale { get; set; }
        /// <summary>
        /// 位置信息
        /// </summary>
        public string Label { get; set; }

        public override string Response()
        {
            return "LocationMessage";
        }
    }




    public class EventMessage : BaseMessage
    {
        public MsgEvent Event { get; set; }

        public override string Response()
        {
            return "EventMessage";
        }

    }



    /// <summary>
    /// 关注事件
    /// </summary>
    public class SubEventMessage : EventMessage
    {
        private string _eventkey;
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值（已去掉前缀，可以直接使用）
        /// </summary>
        public string EventKey
        {
            get { return _eventkey; }
            set { _eventkey = value.Replace("qrscene_", ""); }
        }

        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        public override string Response()
        {
            log.Info($"关注公众号，Event-{Event},EventKey:{EventKey},Ticket:{Ticket}");

            if (EventKey.StartsWith("qrscene_"))
                EventKey = EventKey.Substring(7);
            if (!EventKey.IsEmpty())
                MessageNotify.NotifyScanQr(new string[] { FromUserName, EventKey });

           
            MessageNotify.NotifyOutsiteAccount(new string[] { FromUserName });

            string reply = AppSettingConfig.WxFollowReply;
            string resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
            return resxml;
        }
    }
    /// <summary>
    /// 取消关注事件
    /// </summary>
    public class UnSubEventMessage : EventMessage
    {
        private string _eventkey;
        /// <summary>
        /// 事件KEY值，qrscene_为前缀，后面为二维码的参数值（已去掉前缀，可以直接使用）
        /// </summary>
        public string EventKey
        {
            get { return _eventkey; }
            set { _eventkey = value.Replace("qrscene_", ""); }
        }
    }

    /// <summary>
    /// 扫描带参数的二维码实体
    /// </summary>
    public class ScanEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，是一个32位无符号整数，即创建二维码时的二维码scene_id
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 二维码的ticket，可用来换取二维码图片
        /// </summary>
        public string Ticket { get; set; }

        public override string Response()
        {
            log.Info($"已关注用户扫码二维码，Event-{Event},EventKey:{EventKey},Ticket:{Ticket}");

            if (EventKey.StartsWith("qrscene_"))
                EventKey = EventKey.Substring(7);
            if (!EventKey.IsEmpty())
                MessageNotify.NotifyScanQr(new string[] { FromUserName, EventKey });

            string reply = AppSettingConfig.WxFollowReply;
            var oauthUrl = AppSettingConfig.WxDomain + "OAuth/Weixin/";
            //reply=""
            string resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
            return resxml;
        }
    }
    /// <summary>
    /// 上报地理位置实体
    /// </summary>
    public class LocationEventMessage : EventMessage
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public string Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public string Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public string Precision { get; set; }

    }
    /// <summary>
    /// 普通菜单事件，包括click和view
    /// </summary>
    public class NormalMenuEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }

        public override string Response()
        {
            string reply = "", resxml = "";
            string cUrl = "";//Utility.GetDynamicRemoteNetURL(SiteEnum.Mobile, "person/index.aspx");
            string rUrl = "";//Utility.GetDynamicRemoteNetURL(SiteEnum.Mobile, "pass/login.aspx");
            string eUrl = "";//Utility.GetDynamicRemoteNetURL(SiteEnum.Exchange, "index.aspx");
            string aUrl = "";//Utility.GetDynamicRemoteNetURL(SiteEnum.Mobile, "help/about.aspx");
            string encrpty = MessageHelper.GetEncryptParam(FromUserName); //加密openId

            reply = "coming soon...";

            //UserManager _userManager = new UserManager();

            switch (EventKey)
            {
                case "button-1-2":
                    break;
                case "button-1-1":
                    break;
                case "button-2-2":
                    break;
                case "button-2-1":
                    break;
                case "button-3-1":
                    break;
                case "button-3-2":
                    break;
                case "button-3-3":
                    reply = "\n";
                    break;
            }
            resxml = string.Format(MessageReply.Message_Text, FromUserName, ToUserName, Utility.GetSecond1970(), reply);
            return resxml;
        }
    }



    public class SendJobFinishEventMessage : EventMessage
    {
        public string MsgId { get; set; }

        /// <summary>
        /// 发送状态
        /// </summary>
        public string Status { get; set; }
        public override string Response()
        {
            log.Info(": TEMPLATE SEND JOB FINISH, MsgId - {0}, ToUser - {1}", MsgId, ToUserName);
            return "success";
        }
    }


    /// <summary>
    /// 菜单扫描事件
    /// </summary>
    public class ScanMenuEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 扫码类型。qrcode是二维码，其他的是条码
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果
        /// </summary>
        public string ScanResult { get; set; }
    }
    /// <summary>
    /// 上报地理位置事件
    /// </summary>
    public class LocationMenuEventMessage : EventMessage
    {
        /// <summary>
        /// 事件KEY值
        /// </summary>
        public string EventKey { get; set; }
    }











}
