

using System;
using com.mh.model.messages;

namespace com.mh.rabbit.iservice
{
    public interface IMessageReceiver<T> where T : IBaseMessage
    {
        void ReceiveReliable(Func<T, bool> postMessageHandler);

        void ReceiveReliable(Func<T, bool> postMessageHandler, string messageTopic);

        void ReceiveReliable(Func<T, bool> postMessageHandler, string messageTopic, bool discardFailMessage);
        
        void Cancel();
    }
}
