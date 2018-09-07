using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.common.logger
{

    public interface ILog
    {
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="obj"></param>
        void Info(object obj);
        void Info(object obj,params object[] format);

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="obj"></param>
        [System.Obsolete("请使用Error方法")]
        void Exception(object obj);

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="obj"></param>
        void Debug(object obj);

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="obj"></param>
        void Warn(object obj);

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        void Error(object obj);
    }
}
