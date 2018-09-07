using System;
using com.wx.common.helper;
using com.wx.weixin.data;
using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.redis;
using com.wx.weixin.api;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace com.wx.weixin
{
    public sealed class WXAccessToken0
    {
        private static ILog log = LocatorFacade.Current.Resolve<ILog>();



        public WXAccessToken0()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }





        private static WxAppParam _param = new WxAppParam();
        /// <summary>  
        /// 获取到的凭证   
        /// </summary>  
        private static WxAppParam param
        {
            get
            {
                if (DateTime.Now > _param.ExpiresTime)
                {
                    GetAccess_token();
                    return _param;
                }
                else
                    return _param;
            }
        }


        private static bool GetAccess_token()
        {
            if (ReadWxParamByDB())
                return true;

            string strUrl = "https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid=" + _param.AppId + "&secret=" + _param.AppSecret;

            string content = AccessHelper.VisitUrlByGet(strUrl);
            if (content == null || content.Length < 2)
                return false;
            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
            if (jsonObj != null && jsonObj["access_token"] != null)
            {
                int expires = Convert.ToInt32(jsonObj["expires_in"].ToString());

                _param.ExpiresTime = DateTime.Now.AddSeconds(expires - 120);
                _param.AccessToken = jsonObj["access_token"].ToString();

                //accessToken 保存的redis
                RedisHelper.Set($"AccessToken_{_param.AppId}", _param.AccessToken, _param.ExpiresTime);
                log.Info(": ReadWxParam By API - over," + _param.AccessToken + " - " + _param.ExpiresTime);

                GetJsapi_ticket();
            }
            return true;
        }

        private static bool GetJsapi_ticket()
        {
            string strUrl = "https://api.weixin.qq.com/cgi-bin/ticket/getticket?type=jsapi&access_token=" + _param.AccessToken;

            string content = AccessHelper.VisitUrlByGet(strUrl);
            if (content == null || content.Length < 2)
                return false;
            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
            if (jsonObj != null && jsonObj["errcode"].ToString() == "0")
            {
                /*int expires = Convert.ToInt32(jsonObj["expires_in"].ToString());
                _param.ExpiresIn = DateTime.Now.AddSeconds(expires - 120);
                */
                _param.JsapiTicket = jsonObj["ticket"].ToString();


                //accessToken 保存的redis
                RedisHelper.Set($"JsapiTicket_{_param.AppId}", _param.JsapiTicket, _param.ExpiresTime);
                log.Info(": ReadWxParam.ticket By API - over," + _param.JsapiTicket + " - " + _param.ExpiresTime);

                //SaveWxParamByDB();
            }
            return true;
        }



        private static bool ReadWxParamByDB()
        {
            log.Info( ": ReadWxParam By DB - start...");
            /*
            var m = GetWxParamByDB();
            if (m != null && m.ExpiresTime > DateTime.Now)
            {
                _param = m;
                return true;
            }
            else
            {}*/
                _param = new WxAppParam()
                {

                    AccessToken = "",
                    JsapiTicket = "",
                    ExpiresTime = DateTime.Now.AddSeconds(-1)
                };
            
            log.Info( ": ReadWxParam By DB - over ," + _param.ToJson());
            return false;
        }




        /*
        private static bool SaveWxParamByDB()
        {
            RedisHelper.Set($"AccessToken_{_param.AppId}", _param.AccessToken, _param.ExpiresTime);
            return false;
        }*/

        private static WxAppParam GetWxParamByDB()
        {
            var accessToken = RedisHelper.getValueString($"AccessToken_{_param.AppId}");
            var jsapiTicket = RedisHelper.getValueString($"JsapiTicket_{_param.AppId}");
            _param.AccessToken = accessToken;
            _param.JsapiTicket = jsapiTicket;
            return null;
        }






    }
}