using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using com.wx.common.helper;
using com.wx.common.logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using com.wx.sqldb;

namespace com.wx.common.mvc
{
    public class UserAuthorizeAttribute : AuthorizeAttribute
    {
        private readonly string cookieName = "COOKIE_MANAGER_INFO";


        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            Logger.Current().Info("<HR />");
            var flag = false;
            var cookie = HttpHelper.GetCookie(cookieName);
            if (!string.IsNullOrEmpty(cookie))
                flag = true;
            return flag;


            /*
            var userAgent = httpContext.Request.UserAgent;
            if (string.IsNullOrEmpty(userAgent) || (!userAgent.Contains("MicroMessenger") && !userAgent.Contains("Windows Phone")))
            {
                //在微信外部
                return false;
            }
            //在微信内部
            return true;
            */

        }


        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            var returnUri = filterContext.RequestContext.HttpContext.Request.Url;
            Logger.Current().Info("获取用户Cookie失败，跳转到登录。");
            /*
            filterContext.Result = new RedirectToRouteResult(
                new RouteValueDictionary(
                    new
                    {
                        //area = "",
                        controller = "Account",
                        action = "Login",
                        returnUri
                    }));
            */
            if (returnUri.ToString().ToLower().IndexOf("/mgr/", StringComparison.Ordinal) > 0)
                filterContext.Result = new RedirectResult("~/Account/Login");
            else if (returnUri.ToString().ToLower().IndexOf("/wp/", StringComparison.Ordinal) > 0)
                filterContext.Result = new RedirectResult("~/OAuth/Weixin?returnUrl=" + returnUri);

            //base.HandleUnauthorizedRequest(filterContext);
        }

        /*
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            Logger.Current().Info("&lt;HR&gt;");
            var cookie = HttpHelper.GetCookie(cookieName);
            Logger.Current().Info($"OnAuthorization read cookie ==> {cookie}");
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = JsonConvert.DeserializeObject(cookie) as JObject;
                if (user != null)
                {
                    var id = user["id"];

                    var db = new HuiDbSession();
                    var m = db.UserRepository.ReadOne((int)id);
                    if (m != null)
                    {
                        //if (m.UserLever.ToString() != this.Roles)
                        //    filterContext.Result = new RedirectResult("~/Account/Login");//角色认证失败重新登陆
                    }
                    else
                        filterContext.Result = new RedirectResult("~/Account/Login");//认证失败重新登陆

                    filterContext.HttpContext.Request.RequestContext.RouteData.Values.Add("authId", id.ToString());
                }
            }
            else {
                Logger.Current().Info($"OnAuthorization , no cookie ==> ~/Account/Login");
                filterContext.Result = new RedirectResult("~/Account/Login");
            }
            //base.OnAuthorization(filterContext);

        }*/





    }
}
