namespace ERP.Common.Infrastructure.Log
{
    public interface ILogger
    {
         /* Log a message object */
        void Debug(object message);
        void Info(object message);
        void Warn(object message);
        void Error(object message);
        void Fatal(object message);
    } 
}