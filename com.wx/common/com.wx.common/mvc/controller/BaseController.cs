using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.common.helper;
using com.wx.common.logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.wx.common.mvc
{

    [ErrorFilter]
   public class BaseController:System.Web.Mvc.Controller
    {



        private readonly string cookieName = "COOKIE_MANAGER_INFO";

        protected int UserId = 0;
        protected string OpenId = "0";
        protected string Mobile = "0";
        protected int UserLevel = -1;
        protected string NickName = "0";


        protected ILog log { get; private set; }

        protected BaseController()
        {
            log = Logger.Current();

            var cookie = HttpHelper.GetCookie(cookieName);
            log.Info($"base.Init,read cookie ==> {cookie}");
            if (!string.IsNullOrEmpty(cookie))
            {
                var user = JsonConvert.DeserializeObject(cookie) as JObject;
                if (user != null)
                {
                    UserId = (int)user["id"];
                    OpenId = user["openId"].ToString();
                    Mobile = user["mobile"].ToString();
                    UserLevel = (int)user["userLevel"];
                    NickName = user["nickName"].ToString();

                }
            }
        }


    }
}
