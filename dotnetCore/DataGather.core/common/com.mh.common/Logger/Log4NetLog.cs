using System;

namespace com.mh.common.Logger
{
    public class Log4NetLog : ILog
    {
        private log4net.ILog log;

        public Log4NetLog(){            
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.CreateRepository("NETCoreRepository");
            log4net.Config.XmlConfigurator.Configure(repository, new System.IO.FileInfo(@"Configurations\log4net.config"));
            log = log4net.LogManager.GetLogger(repository.Name, "NETCorelog4net");
            System.Console.WriteLine(repository.Name);
            System.Console.WriteLine(new System.IO.FileInfo(@"Configurations\log4net.config").ToString());
        }

        #region Implementation of ILog

        /// <summary>
        /// 信息
        /// </summary>
        /// <param name="obj"></param>
        public void Info(object obj)
        {
            //System.Console.WriteLine($"log.info:{obj}");
            log.Info(obj);
        }

        /// <summary>
        /// 异常
        /// </summary>
        /// <param name="obj"></param>
        public void Exception(object obj)
        {
            Error(obj);
        }

        /// <summary>
        /// 调试信息
        /// </summary>
        /// <param name="obj"></param>
        public void Debug(object obj)
        {
            log.Debug(obj);
        }

        /// <summary>
        /// 警告
        /// </summary>
        /// <param name="obj"></param>
        public void Warn(object obj)
        {
            log.Warn(obj);
        }

        /// <summary>
        /// 错误
        /// </summary>
        /// <param name="obj"></param>
        public void Error(object obj)
        {
            log.Error(obj);
        }

        #endregion
    }
}
