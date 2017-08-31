using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.config
{
    public class AppSettingConfig
    {


        /********************************************************************************/

        public static string TDES_Key
        {
            get { return ConfigurationManager.AppSettings["TDES_Key"]; }
        }
        public static string TDES_IV
        {
            get { return ConfigurationManager.AppSettings["TDES_IV"]; }
        }

        /********************************************************************************/

        public static string MgConn
        {
            get { return ConfigurationManager.AppSettings["MgConn"]; }
        }
        public static string MgDBName
        {
            get { return ConfigurationManager.AppSettings["MgDBName"]; }
        }
        public static string MgPrefix
        {
            get { return ConfigurationManager.AppSettings["MgPrefix"]; }
        }

        /********************************************************************************/

        public static string MqHost
        {
            get { return ConfigurationManager.AppSettings["MqHost"]; }
        }
        public static string MqUser
        {
            get { return ConfigurationManager.AppSettings["MqUser"]; }
        }

        public static string MqPwd
        {
            get { return ConfigurationManager.AppSettings["MqPwd"]; }
        }

        public static string MqExchange
        {
            get { return ConfigurationManager.AppSettings["MqExchange"]; }
        }
        public static string MqQueue
        {
            get { return ConfigurationManager.AppSettings["MqQueue"]; }
        }

        /********************************************************************************/

        public static string ShopDomain
        {
            get { return ConfigurationManager.AppSettings["ShopDomain"]; }
        }
        public static string ImgDomain => ConfigurationManager.AppSettings["ImgDomain"];

        /// <summary>
        /// wx.listen domain
        /// </summary>
        public static string WxDomain => ConfigurationManager.AppSettings["WxDomain"];


        public static string WxToekn => ConfigurationManager.AppSettings["wxToken"];
        public static string WxAppId => ConfigurationManager.AppSettings["wxAppId"];
        public static string WxAppSecret => ConfigurationManager.AppSettings["wxAppSecret"];
        public static string WxAESKey => ConfigurationManager.AppSettings["wxEncodingAESKey"];
        public static string WxDataPath => ConfigurationManager.AppSettings["wxDataPath"];

        public static string SourceDomain => ConfigurationManager.AppSettings["sourceDomain"];





        public static string WxAuthDomain
        {
            get { return ConfigurationManager.AppSettings["WxAuthDomain"]; }
        }

        public static string WxFollowReply
        {
            get { return ConfigurationManager.AppSettings["WxFollowReply"]; }
        }
        public static string WxOpenName
        {
            get { return ConfigurationManager.AppSettings["WxOpenName"]; }
        }
        public static string SysHelpUrl
        {
            get { return ConfigurationManager.AppSettings["SysHelpUrl"]; }
        }
        public static string JoinUsUrl
        {
            get { return ConfigurationManager.AppSettings["JoinUsUrl"]; }
        }
        /********************************************************************************/



        /********************************************************************************/



        /********************************************************************************/
    }
}
