using System;
using com.wx.api.client;
using com.wx.common.logger;

namespace com.wx.api.weixin
{
    public class MediaMg
    {
        private static readonly ILog _log = Logger.Current();

        
        public byte[] DownloacMedia(string accessToken, string mediaId)
        {
            try
            {
                var client = new WxApiClient();
                var request = client.Req4GetMedia();

                request.AddUrlSegment("access_token", accessToken);
                request.AddUrlSegment("media_id", mediaId);

                var response = client.Execute<dynamic>(request);
                var bytes = response.RawBytes;
                if (bytes == null || bytes.Length <= 0)
                {
                    _log.Info("读取微信二维码图片错误: null or length =0 ");
                    return null;
                }
                return bytes;
            }
            catch (Exception ex)
            {
                _log.Info("下载素材失败；异常信息:{0}", ex);
                return null;
            }
        }



    }
}
