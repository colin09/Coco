namespace com.fs.common.logger
{
    public interface ILog
    {
        void Info(object obj);
        void Exception(object obj);
        void Error(object obj);
        void Debug(object obj);
        void Warn(object obj);
    }
}