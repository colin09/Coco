using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.weixin;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using com.wx.weixin.api;
using com.wx.sqldb;
using com.wx.sqldb.data;
using com.wx.common.helper;
using com.wx.api.weixin;

namespace com.wx.listener
{
    public partial class oauth : System.Web.UI.Page
    {

        private readonly HuiDbSession db = new HuiDbSession();
        private readonly ILog log;

        public oauth()
        {
            UnityBootStrapper.Init();
            log = LocatorFacade.Current.Resolve<ILog>();
        }
        /*
         *  appid	是	公众号的唯一标识
            redirect_uri	是	授权后重定向的回调链接地址
            response_type	是	返回类型，请填写code
            scope	是	应用授权作用域，snsapi_base （不弹出授权页面，直接跳转，只能获取用户openid），snsapi_userinfo （弹出授权页面，可通过openid拿到昵称、性别、所在地。并且，即使在未关注的情况下，只要用户授权，也能获取其信息）
            state	否	重定向后会带上state参数，开发者可以填写任意参数值
            #wechat_redirect	否	直接在微信打开链接，可以不填此参数。做页面302重定向时候，必须带此参数
         *
         *  auth2.0 url :https://open.weixin.qq.com/connect/oauth2/authorize?appid=APPID&redirect_uri=REDIRECT_URI&response_type=code&scope=SCOPE&state=STATE#wechat_redirect
         * 
         *  REDIRECT_URI?code=00b788e3b42043c8459a57a8d8ab5d9f&state=1
         * 得到 code ="";
         * 
         * 换取网页授权access_token页面的构造方式：
         * https://api.weixin.qq.com/sns/oauth2/access_token?appid=APPID&secret=SECRET&code=CODE&grant_type=authorization_code
         * 
            appid	是	公众号的唯一标识
            secret	是	公众号的appsecret
            code	是	填写第一步获取的code参数
            grant_type	是	填写为authorization_code
         * 
         * 得到如下json数据"
         * {
                "access_token": "OezXcEiiBSKSxW0eoylIeAsR0GmYd1awCffdHgb4fhS_KKf2CotGj2cBNUKQQvj-G0ZWEE5-uBjBz941EOPqDQy5sS_GCs2z40dnvU99Y5AI1bw2uqN--2jXoBLIM5d6L9RImvm8Vg8cBAiLpWA8Vw",
                "expires_in": 7200,
                "refresh_token": "OezXcEiiBSKSxW0eoylIeAsR0GmYd1awCffdHgb4fhS_KKf2CotGj2cBNUKQQvj-G0ZWEE5-uBjBz941EOPqDQy5sS_GCs2z40dnvU99Y5CZPAwZksiuz_6x_TfkLoXLU7kdKM2232WDXB3Msuzq1A",
                "openid": "oLVPpjqs9BhvzwPj5A-vTYAX3GLc",
                "scope": "snsapi_userinfo,"
            }
         * 
         * 数据格式解读如下"
         * 
            access_token	网页授权接口调用凭证,注意：此access_token与基础支持的access_token不同
            expires_in	access_token接口调用凭证超时时间，单位（秒）
            refresh_token	用户刷新access_token
            openid	用户唯一标识，请注意，在未关注公众号时，用户访问公众号的网页，也会产生一个用户和公众号唯一的OpenID
            scope	用户授权的作用域，使用逗号（,）分隔
         * 
         * 
         * 使用access_token获取用户信息
         * https://api.weixin.qq.com/sns/userinfo?access_token=ACCESS_TOKEN&openid=OPENID
         * 
            {
                "openid": "oLVPpjqs9BhvzwPj5A-vTYAX3GLc",
                "nickname": "刺猬宝宝",
                "sex": 1,
                "language": "简体中文",
                "city": "深圳",
                "province": "广东",
                "country": "中国",
                "headimgurl": "http://wx.qlogo.cn/mmopen/utpKYf69VAbCRDRlbUsPsdQN38DoibCkrU6SAMCSNx558eTaLVM8PyM6jlEGzOrH67hyZibIZPXu4BK1XNWzSXB3Cs4qpBBg18/0",
                "privilege": []
            }
         */


        /*
        1、auth2.0  ==> code
        2、code ==> access_token,openid
        3、access_token,openid  ==> userInfo
           */

        protected void Page_Load(object sender, System.EventArgs e)
        {
            log.Info(":[oauthWX] from - " + HttpContext.Current.Request.RawUrl);

            string code = Utility.RequestString("code");
            string state = Utility.RequestString("state");

            GetOpenId(code, state);
        }


        private void GetOpenId(string code, string state)
        {
            log.Info(": get aouth back code;" + code);

            string url = string.Format("https://api.weixin.qq.com/sns/oauth2/access_token?appid={0}&secret={1}&code={2}&grant_type=authorization_code",
                TokenMg.Param.AppId, TokenMg.Param.AppSecret, code);
            string json = AccessHelper.VisitUrlByGet(url);

            log.Info(": get auto weixin json;" + json);

            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(json);
            log.Info(": get auto weixin errcode;" + jsonObj["errcode22"]);
            if (jsonObj != null)
            {
                string accessToken = jsonObj["access_token"].ToString();
                string expiresIn = jsonObj["expires_in"].ToString();
                string refesh = jsonObj["refresh_token"].ToString();
                string openId = jsonObj["openid"].ToString();
                string scope = jsonObj["scope"].ToString();
                if (state == "join")
                {
                    var flag = GetUserInfo(openId, accessToken);
                    if (flag)
                    {
                        // 去购买 --->
                    }
                    // 授权失败
                }
                else if (state == "AccountCenter")
                {
                    // redirct(openId); 去个人中心
                }
            }
            else
            {
                log.Info("close current page...");
                Response.Write("<script language=\"javascript\">window.opener=null;window.close();</script>");
                //redirct("0");
            }
        }


        private bool GetUserInfo(string openId, string access_token)
        {
            log.Info(": get userinfo by openId;" + openId);

            string url = string.Format("https://api.weixin.qq.com/sns/userinfo?access_token={0}&openid={1}",
                access_token, openId);
            string json = AccessHelper.VisitUrlByGet(url);

            log.Info(": get weixin json-userinfo;" + json);

            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(json);
            if (jsonObj != null)
            {
                string openid = jsonObj["openid"].ToString();
                string nickname = jsonObj["nickname"].ToString();
                string sex = jsonObj["sex"].ToString();
                string headimgurl = jsonObj["headimgurl"].ToString();

                string city = jsonObj["city"].ToString();
                string province = jsonObj["province"].ToString();
                string country = jsonObj["country"].ToString();

                var account = db.OutsiteUserRepository.Where(ou => ou.OpenId == openId).FirstOrDefault();

                db.UserRepository.Create(new UserEntity
                {
                    NickName = nickname,
                    Name = nickname,
                    Password = CryptoHelper.MD5Encrypt(DateTime.Now.ToShortDateString()),
                    Gender = Convert.ToInt32(sex),
                    //AccountNO = account?.AccountNO ?? common.helper.Utility.GetSecond2015().ToString(),
                });

                //发模板消息
                return true;
            }
            return false;
        }

    }
}