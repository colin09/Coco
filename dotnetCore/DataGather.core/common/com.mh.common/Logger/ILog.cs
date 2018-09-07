namespace com.mh.common.Logger
{
    public interface ILog
    {
        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="obj"></param>
        void Info(object obj);

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