using com.wx.api.client;
using com.wx.common.config;
using com.wx.common.logger;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace com.wx.api.weixin
{
    public class MenuMg
    {
        private static readonly ILog _log = Logger.Current();


        public static bool CrateMenu()
        {
            var client = new WxApiClient();
            var request = client.Req4CreateMenu();

            request.AddUrlSegment("access_token", TokenMg.Param.AccessToken);
            //var meun = MakeMenu();
            //_log.Info($"menu==>{meun}");

            var menu = new
            {
                button = new[] {
                    new MenuItem(){type="click",name="往期精彩",key="btn100"},
                    //new MenuItem(){type="click",name="美爵系列",key="button2"},
                    new MenuItem(){type="view",name="个人中心",url=$"{AppSettingConfig.WxDomain}wp/user/index"}
                }
            };
            request.AddBody(menu);
            _log.Info(JsonConvert.SerializeObject(menu));
            var content = client.Execute<dynamic>(request).Content;

            _log.Info(": CrateMenu By API - content:" + content);
            JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);

            if (jsonObj != null && jsonObj["errcode"].ToString() == "0")
                return true;
            return false;
        }


        public static string GetMenu()
        {
            var client = new WxApiClient();
            var request = client.Req4GetMenu();

            request.AddUrlSegment("access_token", TokenMg.Param.AccessToken);
            var content = client.Execute<dynamic>(request).Content;

            _log.Info(": GetMenu By API - content:" + content);
            //JObject jsonObj = (JObject)JsonConvert.DeserializeObject(content);

            return content;

        }











        private static string MakeMenu()
        {
            var menu = new
            {
                button = new[] {
                    new MenuItem(){type="click",name="往期精彩",key="btn100"},
                    //new MenuItem(){type="click",name="美爵系列",key="button2"},
                    new MenuItem(){type="view",name="个人中心",url=$"{AppSettingConfig.WxDomain}wp/user/index"}
                }
            };
            var menuStr = JsonConvert.SerializeObject(menu, new JsonSerializerSettings { DefaultValueHandling = DefaultValueHandling.Ignore });
            return menuStr;
        }


        /*
        {
            "button": [{
                "type": "click",
                "name": "今日歌曲",
                "key": "V1001_TODAY_MUSIC"

            },
            {
                "name": "菜单",
                "sub_button": [{
                    "type": "view",
                    "name": "搜索",
                    "url": "http://www.soso.com/"

                },
                {
                    "type": "view",
                    "name": "视频",
                    "url": "http://v.qq.com/"
                },
                {
                    "type": "click",
                    "name": "赞一下我们",
                    "key": "V1001_GOOD"
                }]
            }]
        }

        {
            "button": [{
                "name": "扫码",
                "sub_button": [{
                    "type": "scancode_waitmsg",
                    "name": "扫码带提示",
                    "key": "rselfmenu_0_0",
                    "sub_button": []
                },
                {
                    "type": "scancode_push",
                    "name": "扫码推事件",
                    "key": "rselfmenu_0_1",
                    "sub_button": []
                }]
            },
            {
                "name": "发图",
                "sub_button": [{
                    "type": "pic_sysphoto",
                    "name": "系统拍照发图",
                    "key": "rselfmenu_1_0",
                    "sub_button": []
                },
                {
                    "type": "pic_photo_or_album",
                    "name": "拍照或者相册发图",
                    "key": "rselfmenu_1_1",
                    "sub_button": []
                },
                {
                    "type": "pic_weixin",
                    "name": "微信相册发图",
                    "key": "rselfmenu_1_2",
                    "sub_button": []
                }]
            },
            {
                "name": "发送位置",
                "type": "location_select",
                "key": "rselfmenu_2_0"
            },
            {
                "type": "media_id",
                "name": "图片",
                "media_id": "MEDIA_ID1"
            },
            {
                "type": "view_limited",
                "name": "图文消息",
                "media_id": "MEDIA_ID2"
            }]
        }
        */

    }


    public class MenuItem
    {
        public string name { set; get; }
        public string type { set; get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { set; get; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { set; get; }
    }

    public enum ButtonType
    {
        click,
        view
    }


}
