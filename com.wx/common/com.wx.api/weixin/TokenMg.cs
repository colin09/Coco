using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.api.client;
using com.wx.api.model;
using com.wx.redis;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using com.wx.common.logger;
using RestSharp;
using com.wx.common.helper;

namespace com.wx.api.weixin
{
    public class TokenMg
    {

        private static ILog _log = Logger.Current();


        private static readonly WxAppParam _param = new WxAppParam();
        /// <summary>  
        /// 获取到的凭证   
        /// </summary>  
        public static WxAppParam Param
        {
            get
            {
                //_log.Info("get WxAppParam...");
                if (DateTime.Now > _param.ExpiresTime)
                {
                    if (!ReadCache())
                        GetAccessToken();
                    return _param;
                }
                else
                    return _param;
            }
        }

        

        public static bool ReadCache()
        {
            _log.Info("read token from redia...");
            var item = XmlHelper.ReadCache();
            if (item != null)
            {
                _param.AccessToken = item[0];
                _param.JsapiTicket = item[1];
                return true;
            }
            else return false;





            var access = RedisHelper.getValueString($"AccessToken_{_param.AppId}");
            var ticket = RedisHelper.getValueString($"JsapiTicket_{_param.AppId}");

            if (string.IsNullOrEmpty(access) || string.IsNullOrEmpty(ticket))
                return false;
            _param.AccessToken = access;
            _param.JsapiTicket = ticket;
            return true;
        }


        public static void GetAccessToken()
        {
            _log.Info("read token from api...");
            try
            {
                var client = new WxApiClient();
                var request = client.Req4AccessToken();

                //var client = new RestClient("https://api.weixin.qq.com");
                //var request = new RestRequest("/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}", Method.GET);

                request.AddUrlSegment("appid", _param.AppId);
                request.AddUrlSegment("secret", _param.AppSecret);
                var response = client.Execute<dynamic>(request);
                //_log.Info(JsonConvert.SerializeObject(response));
                var content = response.Content;

                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
                if (jsonObj != null && jsonObj["access_token"] != null)
                {
                    int expires = Convert.ToInt32(jsonObj["expires_in"].ToString());
                    var accessToken = jsonObj["access_token"].ToString();

                    _param.ExpiresTime = DateTime.Now.AddSeconds(expires - 120);
                    _param.AccessToken = accessToken;

                    //accessToken 保存的redis
                    //RedisHelper.Set($"AccessToken_{_param.AppId}", _param.AccessToken, _param.ExpiresTime);
                    _log.Info(": Read AccessToken By API - over," + _param.AccessToken + " - " + _param.ExpiresTime);

                    GetJsApiTicket();
                }
                else
                    _log.Info("Read AccessToken By API - faild:"+ content);
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("Read AccessToken By API Error；异常信息:{0}", ex));
                //return null;
            }
        }

        public static void GetJsApiTicket()
        {
            _log.Info("read JsApiTicket from api...");
            try
            {
                var client = new WxApiClient();
                var request = client.Req4JsApiTicket();

                request.AddUrlSegment("access_token", _param.AccessToken);
                var content = client.Execute<dynamic>(request).Content;

                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
                _log.Info(": Read JsApiTicket By API - content:" + content);
                if (jsonObj != null && jsonObj["ticket"] != null)
                {
                    var ticket = jsonObj["ticket"].ToString();

                    _param.JsapiTicket = ticket;

                    _log.Info(": Read JsApiTicket By API - over," + _param.JsapiTicket);

                    //accessToken 保存的redis
                    //RedisHelper.Set($"JsapiTicket_{_param.AppId}", _param.JsapiTicket, _param.ExpiresTime);
                    //accessToken 保存的 xml
                    XmlHelper.WriteCache(_param.Token,_param.JsapiTicket,_param.ExpiresTime);
                }
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("Read JsApiTicket By API；异常信息:{0}", ex));
                //return null;
            }
        }

    }
}
