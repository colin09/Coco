namespace com.mh.common.Logger
{
    public class LoggerManager
    {
        private static readonly ILog Log;

        static LoggerManager()
        {
            //Log = new Log4NetLog();
            Log = new Logging();
        }

        public static ILog Current()
        {
            return Log;
        }
    }
}