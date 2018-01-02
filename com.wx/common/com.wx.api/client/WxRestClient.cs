using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace com.wx.api.client
{
    class WxApiClient : RestClient
    {

        private static string _baseUrl = "https://api.weixin.qq.com";



        public WxApiClient()
            : base(_baseUrl)
        {
        }


        public RestRequest Req4AccessToken()
        {
            return new RestRequest("/cgi-bin/token?grant_type=client_credential&appid={appid}&secret={secret}", Method.GET);
        }

        public RestRequest Req4JsApiTicket()
        {
            return new RestRequest("/cgi-bin/ticket/getticket?type=jsapi&access_token={access_token}", Method.GET);
        }


        #region  -- accountM  -

        public RestRequest Req4CreateQrCode()
        {
            var request = new RestRequest("/cgi-bin/qrcode/create?access_token={access_token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            return request;
        }

        #endregion


        public RestRequest Req4GetMedia()
        {
            var request = new RestRequest("/cgi-bin/media/get?access_token={access_token}&media_id={media_id}", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            return request;
        }


        /// <summary>
        /// appId
        /// secret
        /// code
        /// </summary>
        /// <returns></returns>
        public RestRequest Req4OAuthToken()
        {
            var request = new RestRequest("/sns/oauth2/access_token?appid={appId}&secret={secret}&code={code}&grant_type=authorization_code", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            return request;
        }


        /// <summary>
        /// access_token
        /// openId
        /// </summary>
        /// <returns></returns>
        public RestRequest Req4UserInfo()
        {
            var request = new RestRequest("/sns/userinfo?access_token={access_token}&openid={openId}&lang=zh_CN", Method.GET);
            //request.RequestFormat = DataFormat.Json;
            return request;
        }


        #region  -- menu  -

        public RestRequest Req4GetMenu()
        {
            return new RestRequest("/cgi-bin/menu/get?access_token={access_token}", Method.GET);
        }
        public RestRequest Req4CreateMenu()
        {
            var request = new RestRequest("/cgi-bin/menu/create?access_token={access_token}", Method.POST);
            request.RequestFormat = DataFormat.Json;
            return request;
        }
        public RestRequest Req4DeleteMenu()
        {
            return new RestRequest("/cgi-bin/menu/delete?access_token={access_token}", Method.GET);
        }

        #endregion















    }

    class WxMpClient : RestClient
    {
        private static string _baseUrl = "https://mp.weixin.qq.com";

        public WxMpClient() : base(_baseUrl)
        {

        }

        public RestRequest Req4ShowQrCode()
        {
            return new RestRequest("/cgi-bin/showqrcode?ticket={ticket}", Method.GET);
        }
    }

    /*
    class WxOpenClient : RestClient
    {
        private static string _baseUrl = "https://open.weixin.qq.com";

        public WxOpenClient() : base(_baseUrl)
        {

        }

        public RestRequest Req4OAuth2()
        {
            return new RestRequest("/connect/oauth2/authorize?appid={appId}&redirect_uri={redirect}&response_type=code&scope={scope}&state={state}#wechat_redirect", Method.GET);
        }
    }*/

}
