using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.weixin.data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace com.wx.weixin.api
{
    public class Api_Material
    {


        private static ILog log;

        public Api_Material()
        {
            log = LocatorFacade.Current.Resolve<ILog>();
        }



        /// <summary>
        /// 获取临时素材
        /// http请求方式：GET
        /// https://api.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string DownloadTempMaterial(string sourceId, string savePath, string saveName)
        {
            string accessToken = "access_token";

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}", accessToken, sourceId);
            string flag = AccessHelper.ReadUrlByGet(url, savePath, saveName);

            log.Info(": DownloadTempMaterial return - " + flag);

            return flag;
        }




        /// <summary>
        /// 分类型获取永久素材的列表
        /// http请求方式：POST
        /// https://api.weixin.qq.com/cgi-bin/media/get?access_token=ACCESS_TOKEN&media_id=MEDIA_ID
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public string GetTempMaterialList(MediaType type, int offset, int count)
        {
            string accessToken = "access_token";

            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", accessToken);
            string json = "{\"type\":\"" + type + "\",\"offset\":\"" + offset + "\", \"count\":\"" + count + "\"}";
            string result = AccessHelper.VisitUrlByPost(url, json);

            log.Info(": GetTempMaterialList return - " + result);

            return result;
        }







    }
}