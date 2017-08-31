using System;
using com.wx.api.client;
using com.wx.common.logger;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace com.wx.api.weixin
{
    public class AccountMg
    {

        private static ILog _log = Logger.Current();

        #region  -- qrcode  ---



        public static string CreateTicketFoyStore(string access_token, int storeId)
        {
            //_log.Info($"CreateTicketFoyStore:{access_token}--{storeId}");
            var sceneStr = string.Format("s-{0}", storeId);
            return CreateTicket(access_token, sceneStr);
            //return CreateTicket(access_token, storeId + 1*10000*10000);
            //return CreateTempTicket(access_token, storeId + 1*10000*10000);
        }



        /// <summary> 创建微信二维码ticket
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="buyerId"></param>
        /// <returns></returns>
        public static string CreateTicket(string access_token, string sceneStr)
        {
            try
            {
                var qr = new
                {
                    action_name = "QR_LIMIT_STR_SCENE",
                    action_info = new
                    {
                        scene = new
                        {
                            scene_str = sceneStr  //string.Format("subscribe={0}", buyerId)
                        }
                    }
                };

                var client = new WxApiClient();
                var request = client.Req4CreateQrCode();

                request.AddUrlSegment("access_token", access_token);
                request.AddBody(qr);
                var response = client.Execute<dynamic>(request);
                _log.Info(response.StatusCode);

                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(response.Content);
                if (jsonObj != null && jsonObj["ticket"] != null)
                {
                    return jsonObj["ticket"].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("请求创建微信二维码Ticket异常；异常信息:{0}", ex));
                return null;
            }
        }


        /// <summary> 创建微信二维码ticket
        /// </summary>
        /// <param name="access_token"></param>
        /// <param name="sceneId"></param>
        /// <returns></returns>
        public static string CreateTempTicket(string access_token, int sceneId)
        {
            try
            {
                var qr = new
                {
                    action_name = "QR_SCENE",
                    expire_seconds = 60 * 60 * 2,
                    action_info = new
                    {
                        scene = new
                        {
                            scene_id = sceneId  //string.Format("subscribe={0}", buyerId)
                        }
                    }
                };

                var client = new WxApiClient();
                var request = client.Req4CreateQrCode();

                request.AddUrlSegment("access_token", access_token);
                request.AddBody(qr);
                var content = client.Execute<dynamic>(request).Content;

                JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);
                if (jsonObj != null && jsonObj["ticket"] != null)
                {
                    return jsonObj["ticket"].ToString();
                }
                return "";
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("请求创建微信二维码Ticket异常；异常信息:{0}", ex));
                return null;
            }
        }




        /// <summary> 获取买手二维码
        /// </summary>
        /// <param name="ticket">二维码ticket</param>
        /// <returns></returns>
        public static byte[] GetQrCodeByTicket(string ticket)
        {
            try
            {
                var client = new WxMpClient();
                var request = client.Req4ShowQrCode();

                request.AddUrlSegment("ticket", ticket);
                var response = client.Execute<dynamic>(request);
                var bytes = response.RawBytes;
                if (bytes == null || bytes.Length <= 0)
                {
                    _log.Info("读取微信二维码图片错误: null or length =0 ");
                    return null;
                }
                /*
                _log.Info($"byte.length:{bytes.Length}");
                MemoryStream ms = new MemoryStream(bytes);
                System.Drawing.Image image = System.Drawing.Image.FromStream(ms);
                var fileName = $"store\\qrcode\\{Guid.NewGuid().ToString()}";
                var path = $"{AppDomain.CurrentDomain.BaseDirectory}\\{fileName}.png";
                image.Save(path);
                */
                return bytes;
            }
            catch (Exception ex)
            {
                _log.Info(string.Format("读取微信二维码图片异常；异常信息:{0}", ex));
                return null;
            }
        }


        #endregion









    }
}
