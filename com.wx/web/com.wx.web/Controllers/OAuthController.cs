using System.Web.Mvc;
using com.wx.api.weixin;
using com.wx.common.config;
using com.wx.common.helper;
using com.wx.common.mvc;
using com.wx.service.iservice;
using com.wx.service.models;
using Newtonsoft.Json;

namespace com.wx.web.Controllers
{
    public class OAuthController : BaseController
    {
        private readonly IUserService _userService;

        public OAuthController(IUserService userService)
        {
            _userService = userService;
        }
        public ActionResult Weixin1(string returnUrl)
        {
            return Redirect("/home/index");
        }

        // GET: OAuth
        public ActionResult Weixin(string returnUrl)
        {
            var redirect = AppSettingConfig.WxDomain + Url.Action("WxCallBack");
            var defaultUrl = AppSettingConfig.WxDomain + Url.Action("Index", "User",new {area="wp"});

            if (string.IsNullOrEmpty(returnUrl))
                redirect += "?redirectUrl=" + System.Web.HttpUtility.UrlEncode(defaultUrl);
            else
                redirect += "?redirectUrl=" + System.Web.HttpUtility.UrlEncode(returnUrl);

            var url = OAuthMg.GetOAuthUrl(System.Web.HttpUtility.UrlEncode(redirect), "state", "snsapi_userinfo");
            log.Info($"OAuth==>{url}");
            return Redirect(url);
        }



        public ActionResult WxCallBack(string code, string state, string redirectUrl)
        {
            if (code.IsEmpty())
            {
                log.Info("获取用户授权，回调code为空，");
                return Redirect(Url.Action("Index", "Home"));
            }

            var token = OAuthMg.GetOAuthToken(code);
            if (token == null)
                return Redirect(Url.Action("Error", "Home", new { message = "获取授权access_token失败。" }));

            var json = OAuthMg.GetOAuthUserInfo(token.accessToken, token.openId);
            if (json.IsEmpty())
                return Redirect(Url.Action("Error", "Home", new { message = "获取授权用户信息失败。" }));

            // save userinfo 
            var result = _userService.SaveOAuthUser(json);
            if (result.IsSuccess)
            {
                var r = result as ServiceResult<dynamic>;
                var cookieName = "COOKIE_MANAGER_INFO";
                var cookie = JsonConvert.SerializeObject(r.Data);

                HttpHelper.WriteCookie(cookieName, cookie, 60);
            }

            return Redirect(redirectUrl);
        }








    }
}