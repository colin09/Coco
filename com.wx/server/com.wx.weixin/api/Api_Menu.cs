using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using Newtonsoft.Json;
using System.Text;
using com.wx.common.config;
using com.wx.common.helper;
using com.wx.weixin.data;
using com.wx.ioc.IOCAdapter;
using com.wx.common.logger;

namespace com.wx.weixin.api
{
    public class Api_Menu
    {
        private static ILog log;

        public Api_Menu()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }

        public void CreateMenu()
        {
            //string accountCenterUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}&state=STATE#wechat_redirect",
            string oauthUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}",
                "AppId", System.Web.HttpUtility.UrlEncode("{0}oauthWX.aspx".Frmt(AppSettingConfig.WxAuthDomain), Encoding.UTF8), "snsapi_base");
            string oauthInfoUrl = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&scope={2}",
                "AppId", System.Web.HttpUtility.UrlEncode("{0}oauthWX.aspx".Frmt(AppSettingConfig.WxAuthDomain), Encoding.UTF8), "snsapi_userinfo");

            WXMenuInfo menu1 = new WXMenuInfo("优惠充值", new WXMenuInfo[] { 
                new WXMenuInfo("移动官网查询", ButtonType.view, AppSettingConfig.WxAuthDomain), 
                new WXMenuInfo("话费优惠", ButtonType.view,AppSettingConfig.WxAuthDomain), 
                new WXMenuInfo("流量优惠", ButtonType.view, AppSettingConfig.WxAuthDomain), 
            });

            WXMenuInfo menu2 = new WXMenuInfo("创业中心", new WXMenuInfo[] { 
                new WXMenuInfo("我要创业", ButtonType.view, oauthInfoUrl.Frmt("&state=join")),
                new WXMenuInfo("推广名片", ButtonType.click, "button-2-2"),
            });

            WXMenuInfo menu3 = new WXMenuInfo("个人中心", new WXMenuInfo[] {
                new WXMenuInfo("在线客服", ButtonType.click, "button-3-1"),
                new WXMenuInfo("会员中心", ButtonType.view, oauthUrl.Frmt("&state=AccountCenter")),
                new WXMenuInfo("今日公告", ButtonType.click, "button-3-3")
            });
            /*
            var m1 = new WXMenuInfo("在线客服", ButtonType.click, "button-3-1");
            var m2 = new WXMenuInfo("会员中心", ButtonType.view, accountCenterUrl);
            var m3 = new WXMenuInfo("今日签到", ButtonType.click, "button-3-3");
            */

            MenuJson menuJson = new MenuJson();
            menuJson.button.AddRange(new WXMenuInfo[] { menu1, menu2, menu3 });
            //menuJson.button.AddRange(new WXMenuInfo[] { m1,m2,m3 });

            string json = JsonConvert.SerializeObject(menuJson);
            log.Info(": CreateMenu - " + json);
            //return;

            bool flag = CreateMenu(menuJson);

        }





        public static string UrlEncode(string str)
        {
            StringBuilder sb = new StringBuilder();
            byte[] byStr = System.Text.Encoding.UTF8.GetBytes(str); //默认是System.Text.Encoding.Default.GetBytes(str)
            for (int i = 0; i < byStr.Length; i++)
            {
                sb.Append(@"%" + Convert.ToString(byStr[i], 16));
            }

            return (sb.ToString());
        }

























        /// <summary>
        /// 菜单查询接口
        /// http请求方式：GET
        /// https://api.weixin.qq.com/cgi-bin/menu/get?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public MenuJson GetMenu()
        {
            MenuJson menu = null;
            string accessToken = "access_token";

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/get?access_token={0}", accessToken);
            string result = AccessHelper.VisitUrlByGet(url);

            log.Info(": GetMenu return - " + result);
            return menu;
        }

        /// <summary>
        /// 创建菜单
        /// http请求方式：POST（请使用https协议） 
        /// https://api.weixin.qq.com/cgi-bin/menu/create?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <param name="menuJson">菜单对象</param>
        /// <returns></returns>
        private bool CreateMenu(MenuJson menuJson)
        {
            string accessToken = "access_token";
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/create?access_token={0}", accessToken);
            string json = JsonConvert.SerializeObject(menuJson);
            string result = AccessHelper.VisitUrlByPost(url, json);

            log.Info(": CreateMenu return - " + result);
            return false;
        }

        /// <summary>
        /// 删除菜单
        /// http请求方式：GET
        /// https://api.weixin.qq.com/cgi-bin/menu/delete?access_token=ACCESS_TOKEN
        /// </summary>
        /// <param name="accessToken">调用接口凭证</param>
        /// <returns></returns>
        public bool DeleteMenu()
        {
            string accessToken = "access_token";
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/menu/delete?access_token={0}", accessToken);
            string result = AccessHelper.VisitUrlByGet(url);

            log.Info(": DeleteMenu return - " + result);
            return false;
        }

    }
}