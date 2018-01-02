using com.wx.common.logger;
using com.wx.ioc;
using com.wx.ioc.IOCAdapter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.weixin.data
{
    public abstract class BaseMessage
    {
        protected ILog log;

        public BaseMessage()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }

        /// <summary>
        /// 开发者微信号
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 发送方帐号（一个OpenID）
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间 （整型）
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public MsgType MsgType { get; set; }

        public abstract string Response();

    }
}
