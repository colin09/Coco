using System;


[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"configuration\Log4Net.config", Watch = true)]
namespace com.wx.common.logger
{
    public class FileLog : ILog
    {
        public static readonly log4net.ILog log = log4net.LogManager.GetLogger("loginfo");
        //public static readonly log4net.ILog _error = log4net.LogManager.GetLogger("logerror");
        //log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        public void Info(object obj)
        {
            log.Info(obj);
        }

        public void Info(object obj, params object[] args)
        {
            log.Info(string.Format(obj.ToString(), args));
        }

        public void Exception(object obj)
        {
            throw new NotImplementedException();
        }

        public void Debug(object obj)
        {
            throw new NotImplementedException();
        }

        public void Warn(object obj)
        {
            throw new NotImplementedException();
        }

        public void Error(object obj)
        {
            log.Info(obj);
            //throw new NotImplementedException();
        }
    }
}
