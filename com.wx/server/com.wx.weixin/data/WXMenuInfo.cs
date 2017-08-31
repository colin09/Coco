using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.weixin.data
{
    public class WXMenuInfo
    {

        #region  --  属性  --

        /// <summary>
        /// 按钮描述，既按钮名字，不超过16个字节，子菜单不超过40个字节
        /// </summary>
        public string name { get; set; }

        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string type { get; set; }

        /// <summary>
        /// 按钮KEY值，用于消息接口(event类型)推送，不超过128字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string key { get; set; }

        /// <summary>
        /// 网页链接，用户点击按钮可打开链接，不超过256字节
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        /// <summary>
        /// 子按钮数组，按钮个数应为2~5个
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<WXMenuInfo> sub_button { get; set; }

        #endregion


        #region  -- 构造函数  --

        /// <summary>
        /// 参数化构造函数
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="buttonType">菜单按钮类型</param>
        /// <param name="value">按钮的键值（Click)，或者连接URL(View)</param>
        public WXMenuInfo(string name, ButtonType buttonType, string value)
        {
            this.name = name;
            this.type = buttonType.ToString();

            if (buttonType == ButtonType.click)
            {
                this.key = value;
            }
            else if (buttonType == ButtonType.view)
            {
                this.url = value;
            }
        }



        /// <summary>
        /// 参数化构造函数,用于构造子菜单
        /// </summary>
        /// <param name="name">按钮名称</param>
        /// <param name="sub_button">子菜单集合</param>
        public WXMenuInfo(string name, IEnumerable<WXMenuInfo> sub_button)
        {
            this.name = name;
            this.sub_button = new List<WXMenuInfo>();
            this.sub_button.AddRange(sub_button);
        }

        #endregion
    }




    public enum ButtonType
    {
        click,
        view
    }


    /// <summary>
    /// 菜单的Json字符串对象
    /// </summary>
    public class MenuJson
    {
        public List<WXMenuInfo> button { get; set; }

        public MenuJson()
        {
            button = new List<WXMenuInfo>();
        }
    }

    /// <summary>
    /// 菜单列表的Json对象
    /// </summary>
    public class MenuListJson
    {
        public MenuJson menu { get; set; }
    }
}
