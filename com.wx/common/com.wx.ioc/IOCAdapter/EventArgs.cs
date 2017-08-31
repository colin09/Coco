using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.ioc.IOCAdapter
{
    /// <summary>
    /// 提供Service Locator事件参数数据
    /// </summary>
    public class IocEventArgs : System.EventArgs
    {
        private string _key;

        public IocEventArgs(string key)
        {
            this._key = key;
        }

        /// <summary>
        /// 向容器中注册或者获取服务时的键值
        /// </summary>
        public string Key
        {
            get { return _key; }
            set { _key = value; }
        }
    }
}
