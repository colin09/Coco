using System;

namespace com.fs.common.logger
{
    public class Log4NetLog : ILog
    {
        private log4net.ILog log;

        public Log4NetLog()
        {
            log4net.Repository.ILoggerRepository repository = log4net.LogManager.CreateRepository("NETCoreRepository");
            log4net.Config.XmlConfigurator.Configure(repository, new System.IO.FileInfo(@"Configurations\log4net.config"));
            log = log4net.LogManager.GetLogger(repository.Name, "NETCorelog4net");
        }

        public void Debug(object obj)
        {
            log.Debug(obj);
        }

        public void Error(object obj)
        {
            log.Error(obj);
        }

        public void Exception(object obj)
        {
            log.Info(obj);
        }

        public void Info(object obj)
        {
            log.Info(obj);
        }

        public void Warn(object obj)
        {
            log.Warn(obj);
        }
    }
}