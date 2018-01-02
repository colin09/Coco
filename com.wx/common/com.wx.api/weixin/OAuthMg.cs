using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.common.logger;
using com.wx.api.client;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.wx.api.weixin
{
    public class OAuthMg
    {
        private static ILog _log = Logger.Current();



        public static string GetOAuthUrl(string redirect, string state, string scope = "snsapi_base")
        {
            var appId = TokenMg.Param.AppId;
            var url =
                string.Format(
                    $"https://open.weixin.qq.com/connect/oauth2/authorize?appid={appId}&redirect_uri={redirect}&response_type=code&scope={scope}&state={state}#wechat_redirect");

            return url;
        }



        public static dynamic GetOAuthToken(string code)
        {
            try
            {
                var client = new WxApiClient();
                var request = client.Req4OAuthToken();

                request.AddUrlSegment("appId", TokenMg.Param.AppId);
                request.AddUrlSegment("secret", TokenMg.Param.AppSecret);
                request.AddUrlSegment("code", code);

                var content = client.Execute<dynamic>(request).Content;

                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
                _log.Info(": Read oauth2/access_token By API - content:" + content);
                if (jsonObj != null && jsonObj["access_token"] != null)
                {
                    var accesstoken = jsonObj["access_token"].ToString();
                    var openid = jsonObj["openid"].ToString();
                    var scope = jsonObj["scope"].ToString();

                    return new { accessTonen = accesstoken, openIe = openid, scope = scope };
                }
                return null;
            }
            catch (Exception ex)
            {
                _log.Info("Read oauth2/access_token 失败；异常信息:{0}", ex);
                return null;
            }

        }
        public static string GetOAuthUserInfo(string accessToken, string openId)
        {
            try
            {
                var client = new WxApiClient();
                var request = client.Req4UserInfo();

                request.AddUrlSegment("access_token", accessToken);
                request.AddUrlSegment("openId", openId);

                var content = client.Execute<dynamic>(request).Content;

                _log.Info(": Read userinfo By API - content:" + content);
                /*
                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
                if (jsonObj != null && jsonObj["openid"] != null)
                */
                return content;
            }
            catch (Exception ex)
            {
                _log.Info("Read oauth2.userinfo 失败；异常信息:{0}", ex);
                return null;
            }

        }



    }
}
