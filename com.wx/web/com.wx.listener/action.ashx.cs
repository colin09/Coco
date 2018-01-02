using System;
using System.Web;
using com.wx.common.logger;
using System.IO;
using com.wx.common.config;
using com.wx.common.helper;
using com.wx.ioc.IOCAdapter;
using com.wx.weixin;
using System.Collections;
using com.wx.api.weixin;

namespace com.wx.listener
{
    /// <summary>
    /// action 的摘要说明
    /// </summary>
    public class action : IHttpHandler
    {
        readonly ILog log;

        public action()
        {
            UnityBootStrapper.Init();
            log = LocatorFacade.Current.Resolve<ILog>();
        }

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            MakeAction();
        }




        private string MakeAction()
        {
            log.Info(": from - {0}", HttpContext.Current.Request.RawUrl);

            string signature = Utility.RequestString("signature");
            string timestamp = Utility.RequestString("timestamp");
            string nonce = Utility.RequestString("nonce");
            string echostr = Utility.RequestString("echostr");

            if (!checkSignature(signature, timestamp, nonce, echostr))
                return "";

            string data = "error";
            string postString = string.Empty;
            if (HttpContext.Current.Request.HttpMethod.ToUpper() == "POST")
            {
                log.Info(": by POST; ");

                using (Stream stream = HttpContext.Current.Request.InputStream)
                {
                    Byte[] postBytes = new Byte[stream.Length];
                    stream.Read(postBytes, 0, (Int32)stream.Length);
                    postString = System.Text.Encoding.UTF8.GetString(postBytes);

                    log.Info(": message - {0}", postString);

                    Handle(postString);
                    /*
                    //action 不做业务处理，交由MQ进行处理 ; 如何回复呢？？？ 难道回复模板消息？
                    var array = new string[] {signature,timestamp,nonce,echostr ,postString};
                    Utility.NotifyWxMessage(array);
                    */
                }
            }
            else
            {
                log.Info(": by GET; ");
                HttpContext.Current.Response.Write(echostr);

                /*if (checkSignature())
                {
                    data = Utility.RequestString("echostr");
                    HttpContext.Current.Response.Write(data);
                }*/
            }
            return data;
        }



        private bool checkSignature(string signature, string timestamp, string nonce, string echostr)
        {
            var wxToken = TokenMg.Param.Token;

            //log.Info("signature:{0}", signature);
            //log.Info("timestamp:{0}", timestamp);
            //log.Info("nonce:{0}", nonce);
            //log.Info("echostr:{0}", echostr);
            //log.Info("Token:{0}", wxToken);

            string[] ArrTmp = { wxToken, timestamp, nonce };
            Array.Sort(ArrTmp);　　 //字典排序　
            string tmpStr = string.Join("", ArrTmp);

            tmpStr = CryptoHelper.SHA1Encrypt(tmpStr);
            tmpStr = tmpStr.ToLower();
            //log.Info("signature-tmpStr:{0}", tmpStr);
            if (tmpStr == signature)
            {
                log.Info("验证通过！");
                return true;
            }
            else
            {
                log.Info("验证失败！");
                return false;
            }
        }




        public bool Handle(string postStr, bool bug = true)
        {
            var timestamp = Utility.RequestString("timestamp");
            var nonce = Utility.RequestString("nonce");
            var msg_signature = Utility.RequestString("msg_signature");
            var encrypt_type = Utility.RequestString("encrypt_type");

            string data = "";
            if (encrypt_type == "aes")//加密模式处理
            {
                TokenMg.Param.IsAes = true;
                var ret = new WXBizMsgCrypt(TokenMg.Param.Token, TokenMg.Param.EncodingAESKey, TokenMg.Param.AppId);
                int r = ret.DecryptMsg(msg_signature, timestamp, nonce, postStr, ref data);
                if (r != 0)
                {
                    log.Info($"{TokenMg.Param.Token},{TokenMg.Param.EncodingAESKey},{TokenMg.Param.AppId}");
                    log.Info(": DecryptMsg fail ... ");
                    return false;
                }
                else
                    log.Info(": DecryptMsg OK !!! ");
            }
            else
            {
                TokenMg.Param.IsAes = false;
                data = postStr;
            }
            //log.Info(": DecryptMsg - " + data);

            if (!MessageFactory.CheckMessage(data))
            {
                var message = MessageFactory.CreateMessage(data);

                log.Info(": Create Message OK,Type - " + message.MsgType);

                string responseContent = message.Response();
                log.Info(": Make reply = " + responseContent);

                HttpContext.Current.Response.ContentEncoding = System.Text.Encoding.UTF8;
                HttpContext.Current.Response.Write(responseContent);

                return true;
            }
            return false;
        }



        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}