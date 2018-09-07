using System;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace com.wx.common.helper
{
    public class HttpHelper
    {

        //------------------------------------------------------------------------

        #region 删除最后结尾的一个逗号
        /// <summary>
        /// 删除最后结尾的一个逗号
        /// </summary>
        public string DelLastComma(string str)
        {
            if (String.IsNullOrEmpty(str))
            {
                return "";
            }
            return str.Substring(0, str.LastIndexOf(","));
        }
        #endregion

        #region 删除最后结尾的指定字符后的字符
        /// <summary>
        /// 删除最后结尾的指定字符后的字符
        /// </summary>
        public string DelLastChar(string str, string strchar)
        {
            if (string.IsNullOrEmpty(str))
                return "";
            if (str.LastIndexOf(strchar) >= 0 && str.LastIndexOf(strchar) == str.Length - 1)
            {
                return str.Substring(0, str.LastIndexOf(strchar));
            }
            return str;
        }
        #endregion

        /// <summary>
        /// URL字符编码
        /// </summary>
        public static string UrlEncode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            str = str.Replace("'", "");
            return HttpContext.Current.Server.UrlEncode(str);
        }
        /// <summary>
        /// URL字符解码
        /// </summary>
        public static string UrlDecode(string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return "";
            }
            return HttpContext.Current.Server.UrlDecode(str);
        }


        #region  -- session -

        public string GetSessionId()
        {
            return HttpContext.Current.Session.SessionID;
        }

        public void SetSession(string key, string value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public void SetSession(string key, object value)
        {
            HttpContext.Current.Session[key] = value;
        }

        public string GetSession(string key)
        {
            return HttpContext.Current.Session[key] != null ? HttpContext.Current.Session[key].ToString() : null;
        }

        public object GetSessionObject(string key)
        {
            return HttpContext.Current.Session[key];
        }

        #endregion



        #region  --  cookie  -
        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public void WriteCookie(string strName, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public void WriteCookie(string strName, string key, string strValue)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        public void WriteCookie(string strName, string key, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie[key] = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 写cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <param name="strValue">值</param>
        /// <param name="strValue">过期时间(分钟)</param>
        public static void WriteCookie(string strName, string strValue, int expires)
        {
            HttpCookie cookie = HttpContext.Current.Request.Cookies[strName];
            if (cookie == null)
            {
                cookie = new HttpCookie(strName);
            }
            cookie.Value = UrlEncode(strValue);
            cookie.Expires = DateTime.Now.AddMinutes(expires);
            HttpContext.Current.Response.AppendCookie(cookie);
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public static string GetCookie(string strName)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName].Value.ToString());
            return "";
        }

        /// <summary>
        /// 读cookie值
        /// </summary>
        /// <param name="strName">名称</param>
        /// <returns>cookie值</returns>
        public string GetCookie(string strName, string key)
        {
            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null && HttpContext.Current.Request.Cookies[strName][key] != null)
                return UrlDecode(HttpContext.Current.Request.Cookies[strName][key].ToString());

            return "";
        }
        #endregion

        #region 替换指定的字符串
        /// <summary>
        /// 替换指定的字符串
        /// </summary>
        /// <param name="originalStr">原字符串</param>
        /// <param name="oldStr">旧字符串</param>
        /// <param name="newStr">新字符串</param>
        /// <returns></returns>
        public string ReplaceStr(string originalStr, string oldStr, string newStr)
        {
            if (string.IsNullOrEmpty(oldStr))
            {
                return "";
            }
            return originalStr.Replace(oldStr, newStr);
        }
        #endregion

        /// <summary>
        /// 时间格式化 年月日
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public string Formattime(string str)
        {
            string strsj;
            strsj = DateTime.Parse(str.ToString()).ToString("yyyy-MM-dd");
            return strsj;
        }

        /// <summary>
        /// 截取一定长度的字符串
        /// </summary>
        /// <param name="str">要截取的字符串</param>
        /// <param name="length">截取的长度</param>
        /// <returns>截取后的字符串</returns>
        public string SubString(string str, int length)
        {
            int stringcount = str.Length;
            if (stringcount > length)
            {
                return str.Substring(0, length) + "...";
            }
            else
            {
                return str;
            }
        }

        /// <summary>
        /// 将相对路径的URL转化为绝对路径，附带http前缀域名
        /// </summary>
        /// <param name="path">相对路径，必须以反斜杠开头</param>
        /// <returns>返回的绝对路径URL</returns>
        public string AbsoluteUrl(string path)
        {
            string baseUrl = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpContext.Current.Request.ApplicationPath;
            if (baseUrl.EndsWith("/"))
            {
                baseUrl = baseUrl.Substring(0, baseUrl.Length - 1);
            }

            return baseUrl + path;
        }


        /// <summary>
        /// 获取用户ip
        /// </summary>
        /// <returns>返回用户ip</returns>
        public string GetUserIP()
        {
            string strIP;
            HttpRequest request = HttpContext.Current.Request;

            if (request.ServerVariables["HTTP_VIA"] != null && request.ServerVariables["HTTP_VIA"] != "")
                strIP = request.ServerVariables["HTTP_X_FORWARDED_FOR"] + "";
            else
                strIP = request.ServerVariables["REMOTE_ADDR"] + "";

            string[] IPs = strIP.Split(new char[] { ',' });

            for (int i = 0; i < IPs.Length; i++)
            //for (int i = IPs.Length - 1; i >= 0; i--)
            {
                if (IPs[i] != null && IPs[i].Trim() != "")
                {
                    strIP = IPs[i].Trim();
                    break;
                }
            }

            if (strIP == null || strIP.Trim() == "")
            {
                strIP = "0.0.0.0";
            }
            return strIP;
        }

        /// <summary>
        /// 判断是否是IP地址格式 0.0.0.0
        /// </summary>
        /// <param name="str1">待判断的IP地址</param>
        /// <returns>true or false</returns>
        private bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }

        public string UpLoadImg(string fileName, string path)
        {
            //上传和返回(保存到数据库中)的路径
            string uppath = string.Empty;
            string savepath = string.Empty;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                //HttpContext.Current.Request.Files[""];
                HttpPostedFileBase imgFile = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[fileName]);
                if (imgFile != null)
                {
                    //创建图片新的名称
                    var nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //获得上传图片的路径
                    var strPath = imgFile.FileName;
                    //获得上传图片的类型(后缀名)
                    var type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                    if (!ValidateImg(type)) return savepath;
                    //拼写数据库保存的相对路径字符串
                    uppath = string.IsNullOrEmpty(path) ? HttpContext.Current.Server.MapPath("~/Uploads/") : HttpContext.Current.Server.MapPath("~/Uploads/" + path + "/");
                    savepath += nameImg + "." + type;
                    //拼写上传图片的路径
                    uppath += nameImg + "." + type;
                    //上传图片
                    imgFile.SaveAs(uppath);
                    return savepath;
                }
            }
            return "";
        }

        /// <summary>
        /// 验证图片格式
        /// </summary>
        /// <param name="imgName"></param>
        /// <returns></returns>
        public bool ValidateImg(string imgName)
        {
            string[] imgType = new string[] { "gif", "jpg", "png", "bmp" };

            int i = 0;
            bool blean = false;
            string message = string.Empty;

            //判断是否为Image类型文件
            while (i < imgType.Length)
            {
                if (imgName.Equals(imgType[i].ToString()))
                {
                    blean = true;
                    break;
                }
                else if (i == (imgType.Length - 1))
                {
                    break;
                }
                else
                {
                    i++;
                }
            }
            return blean;
        }

        /// <summary>
        ///  获得某个字符串在另个字符串中出现的次数
        /// </summary>
        /// <param name="strOriginal">要处理的字符</param>
        /// <param name="strSymbol">符号</param>
        /// <returns>返回值</returns>
        public int GetStrCount(string strOriginal, string strSymbol)
        {
            int count = 0;
            for (int i = 0; i < (strOriginal.Length - strSymbol.Length + 1); i++)
            {
                if (strOriginal.Substring(i, strSymbol.Length) == strSymbol)
                {
                    count = count + 1;
                }
            }
            return count;
        }

        public string UpLoadFile(string fileName, string path)
        {
            //上传和返回(保存到数据库中)的路径
            var uppath = string.Empty;
            var savepath = string.Empty;
            if (HttpContext.Current.Request.Files.Count > 0)
            {
                //HttpContext.Current.Request.Files[""];
                HttpPostedFileBase imgFile = new HttpPostedFileWrapper(HttpContext.Current.Request.Files[fileName]);
                if (imgFile != null)
                {
                    //创建图片新的名称
                    var nameImg = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    //获得上传图片的路径
                    var strPath = imgFile.FileName;
                    //获得上传图片的类型(后缀名)
                    var type = strPath.Substring(strPath.LastIndexOf(".") + 1).ToLower();
                    if (type != "rar" && type != "zip")
                        return savepath;
                    //拼写数据库保存的相对路径字符串
                    uppath = string.IsNullOrEmpty(path) ? HttpContext.Current.Server.MapPath("~/Uploads/") : HttpContext.Current.Server.MapPath("~/Uploads/" + path + "/");
                    savepath += nameImg + "." + type;
                    //拼写上传的路径
                    uppath += nameImg + "." + type;
                    //上传图片
                    imgFile.SaveAs(uppath);
                    return savepath;
                }
            }
            return "";
        }


        ///<summary>   
        ///依据连接串名字connectionName返回数据连接字符串   
        ///</summary>   
        ///<param name="connectionName"></param>
        ///<returns></returns>
        public string GetConnectionStringsConfig(string connectionName)
        {
            return ConfigurationManager.ConnectionStrings[connectionName].ConnectionString;
        }

        ///<summary>   
        ///返回＊.exe.config文件中appSettings配置节的value项   
        ///</summary>   
        ///<param name="strKey"></param>   
        ///<returns></returns>   
        public string GetAppConfig(string strKey)
        {
            return ConfigurationManager.AppSettings.Cast<string>().Any(key => key == strKey) ? ConfigurationManager.AppSettings[strKey] : null;
        }

       

    }
}
