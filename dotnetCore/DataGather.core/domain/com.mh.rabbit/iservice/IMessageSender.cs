
using System;
using com.mh.model.messages;

namespace com.mh.rabbit.iservice
{
    public interface IMessageSender<T> where T : IBaseMessage
    {
        bool SendMessageReliable(T message);

        bool SendMessageReliable(T message, Action<T> preMessageHandler);

        bool SendMessageReliable(T message, Action<T> preMessageHandler,string messageTopic);
    }
}
