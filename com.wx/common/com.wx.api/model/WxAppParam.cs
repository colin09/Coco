using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using com.wx.common.config;

namespace com.wx.api.model
{

    public class WxAppParam
    {

        /// <summary>
        /// 令牌 [自定义-填入微信]
        /// </summary>
        //public string Token { get { return "colin09X4b7OA2jkHJnJ4U6U53nq8"; } }
        public string Token => AppSettingConfig.WxToekn;
        /// <summary>
        /// 应用程序Id [微信提供]
        /// </summary>
        //public string AppId { get { return "wx2265477fde928230"; } }
        public string AppId => AppSettingConfig.WxAppId;
        /// <summary>
        /// 应用程序密钥 [微信提供]
        /// </summary>
        //public string AppSecret { get { return "3ef864f0b5424e886b79211ee79d1d35"; } }
        public string AppSecret => AppSettingConfig.WxAppSecret;
        /// <summary>
        /// 消息加密密钥 [微信提供/随机生成-配置时可修改]
        /// </summary>
        //public string EncodingAESKey { get { return "BsulHRbybMKJBT0js42hQMfe73QODg51HdU0hORu4hP"; } }
        public string EncodingAESKey => AppSettingConfig.WxAESKey;

        /// <summary>
        /// 公众号的全局唯一票据
        /// 有效期目前为2个小时
        /// </summary>
        public string AccessToken { set; get; }
        public string JsapiTicket { set; get; }
        /// <summary>
        /// AccessToken 过期时间
        /// </summary>
        public DateTime ExpiresTime { set; get; } = DateTime.MinValue;
        /// <summary>
        /// 是否加密
        /// </summary>
        public bool IsAes { set; get; }
    }
}
