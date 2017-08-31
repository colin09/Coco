using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.weixin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.mq.handler
{
    class ReplyWxHandler : BaseHandler
    {
        private ILog log;

        public ReplyWxHandler()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }


        public bool Action(string message)
        {
            var array = JsonConvert.DeserializeObject<string[]>(message);
            var timestamp = array[0];
            var nonce = array[1];
            var msg_signature = array[2];
            var encrypt_type = array[3];
            var postStr = array[4];

            string data = "";
            if (encrypt_type == "aes")//加密模式处理
            {
                WXAccessToken.param.IsAes = true;
                var ret = new WXBizMsgCrypt(WXAccessToken.param.Token, WXAccessToken.param.EncodingAESKey, WXAccessToken.param.AppId);
                int r = ret.DecryptMsg(msg_signature, timestamp, nonce, postStr, ref data);
                if (r != 0)
                {
                    log.Info(": DecryptMsg fail ... ");
                    return false;
                }
                else
                    log.Info(": DecryptMsg OK !!! ");
            }
            else
            {
                WXAccessToken.param.IsAes = false;
                data = postStr;
            }
            log.Info(": DecryptMsg - " + data);

            if (!MessageFactory.CheckMessage(data))
            {
                var wxMessage = MessageFactory.CreateMessage(data);

                log.Info(": Create Message OK,Type - " + wxMessage.MsgType);


                string responseContent = wxMessage.Response();
                log.Info(": Make reply = " + responseContent);
                /*
                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Write(responseContent);
                */
                return true;
            }
            return false;
        }
    }
}
