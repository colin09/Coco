

using com.mh.model.messages;

namespace com.mh.rabbit.iservice
{
    public interface IMessageProvider<T> where T : IBaseMessage
    {
        IMessageSender<T> GetSender();

        IMessageReceiver<T> GetReceiver();
    }
}
