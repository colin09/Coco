using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.ioc.IOCAdapter
{
    /// <summary>
    /// 针对不同的DI容器提供外观类
    /// </summary>
    public sealed class LocatorFacade
    {
        private static ILocator _serviceLocator;

        //private static readonly object _lockObj = new object();

        /// <summary>
        /// 当前容器
        /// </summary>
        public static ILocator Current
        {
            get
            {
                return _serviceLocator;
            }
        }

        /// <summary>
        /// 设置当前使用的容器
        /// </summary>
        /// <param name="newLocator"></param>
        public static void SetLocatorProvider(ILocator newLocator)
        {
            _serviceLocator = newLocator;
        }

    }
}
