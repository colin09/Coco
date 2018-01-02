using com.wx.common.logger;
using com.wx.ioc;
using com.wx.ioc.IOCAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using RestSharp;

namespace com.wx.weixin.api
{
    public class AccessHelper
    {
        private static ILog log = LocatorFacade.Current.Resolve<ILog>();

        public AccessHelper()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }

        #region

        private static string _baseUrl = "https://mp.weixin.qq.com";

        #endregion

        public static string GetUrl(string url, string access_token, object obj)
        {
            log.Info("get url strart:{0}", url);
            try
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest(url, Method.GET);

                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("access_token", access_token);
                request.AddBody(obj);

                var response = client.Execute(request);
                return response.Content;

            }
            catch (Exception ex)
            {
                log.Info("get url exception: {0}", ex.Message);
                return null;
            }
        }

        public static string DownloadUrl(string url, string access_token, object obj)
        {
            log.Info("download url strart:{0}", url);
            try
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest(url, Method.GET);

                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("access_token", access_token);
                request.AddBody(obj);

                var response = client.Execute(request);

                var bytes = response.RawBytes;
                if (bytes == null || bytes.Length <= 0)
                    return string.Empty;
#if DEBUG
                //MemoryStream ms = new MemoryStream(bytes);
                //Image image = System.Drawing.Image.FromStream(ms);
                //var path = AppDomain.CurrentDomain.BaseDirectory + "\\" + DateTime.Now.ToString("HHmmss") + ".png";
                //image.Save(path);
#endif
                var result = Convert.ToBase64String(bytes);
                return result;
            }
            catch (Exception ex)
            {
                log.Info("download url exception: {0}", ex.Message);
                return null;
            }
        }

        public static string PostUrl(string url, string access_token, object obj)
        {
            log.Info("post url strart:{0}", url);
            try
            {
                var client = new RestClient(_baseUrl);
                var request = new RestRequest(url, Method.POST);

                request.RequestFormat = DataFormat.Json;
                request.AddUrlSegment("access_token", access_token);
                request.AddBody(obj);

                var response = client.Execute(request);
                return response.Content;

            }
            catch (Exception ex)
            {
                log.Info("post url exception: {0}", ex.Message);
                return null;
            }
        }




























        public static string VisitUrlByGet(string strUrl)
        {
            log.Info(": VisitUrl - start :{0}", strUrl);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);

            req.Method = "GET";
            using (WebResponse wr = req.GetResponse())
            {
                HttpWebResponse myResponse = (HttpWebResponse)req.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

                string content = reader.ReadToEnd();

                log.Info(": VisitUrl over - {0}", content);

                return content;
            }
        }


        /// <summary>
        /// 下载素材
        /// </summary>
        /// <param name="strUrl">get URL</param>
        /// <param name="savePath">[例:~/headImages/]</param>
        /// <param name="saveName">[例:1000000001.jpg]</param>
        /// <returns>fileName</returns>
        public static string ReadUrlByGet(string strUrl, string savePath, string saveName)
        {
            log.Info(": ReadUrl - start :" + strUrl);

            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(strUrl);

            req.Method = "GET";
            using (WebResponse response = req.GetResponse())
            {
                string resExtName = GetExtName(response.ContentType);
                log.Info(": ReadUrl resource ContentType - {0} , ReadUrl resource ContentLength - {1}", response.ContentType, response.ContentLength);
                Stream reader = response.GetResponseStream();

                long totalBytes = response.ContentLength;

                byte[] by = new byte[8096];
                int osize = reader.Read(by, 0, (int)by.Length);

                Stream writer = new FileStream(@"E:\yaochengwang.com\ycw.3.0\Mobile\images\temp\" + saveName + resExtName, FileMode.Create);
                while (osize > 0 && totalBytes > 0)
                {
                    writer.Write(by, 0, osize);

                    osize = reader.Read(by, 0, (int)by.Length);
                    System.Threading.Thread.Sleep(10);
                }
                writer.Flush(); writer.Close();
                reader.Flush(); reader.Close();

                FileStream fStream = new FileStream(@"E:\yaochengwang.com\ycw.3.0\Mobile\images\temp\" + saveName + resExtName, FileMode.Open, FileAccess.Read);
                Byte[] b = new Byte[fStream.Length];
                fStream.Read(b, 0, b.Length);
                fStream.Flush(); fStream.Close();
                /*  上传至服务器
                UploadServiceReference.UploadServiceSoapClient client = new UploadServiceReference.UploadServiceSoapClient();
                bool flag = client.Up(b, savePath, saveName + resExtName, 1);
                
                log.Info(": ReadUrlByGet over - {0}" , flag);

                if (flag)
                    return saveName + resExtName;
                else
                    return flag.ToString();
                */
                reader.Close();

                return "";
            }
        }


        private static string GetExtName(string type)
        {
            string extName = ".jpg";
            if (extName.Contains("/"))
                switch (type.Substring(type.LastIndexOf("/")).ToLower())
                {
                    case "jpeg": extName = ".jpg"; break;
                    case "png": extName = ".png"; break;
                    case "gif": extName = ".gif"; break;
                }
            return extName;
        }






        public static string VisitUrlByPost(string strUrl, string postData)
        {
            log.Info(": VisitUrl - start : {0}", strUrl);

            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);

            request.Method = "POST";
            request.Accept = "*/*";
            request.ContentType = "application/x-www-form-urlencoded";
            try
            {
                System.Text.Encoding encoding = System.Text.Encoding.UTF8;
                byte[] buffer = encoding.GetBytes(postData);
                request.ContentLength = buffer.Length;
                request.GetRequestStream().Write(buffer, 0, buffer.Length);

                HttpWebResponse myResponse = (HttpWebResponse)request.GetResponse();
                StreamReader reader = new StreamReader(myResponse.GetResponseStream(), System.Text.Encoding.UTF8);

                string content = reader.ReadToEnd();

                reader.Close();
                log.Info(": VisitUrl over - {0}", content);

                return content;
            }
            catch (Exception ex)
            {
                log.Info(": VisitUrlByPost error - {0}", ex.Message);
                return "";
            }

        }


    }
}
