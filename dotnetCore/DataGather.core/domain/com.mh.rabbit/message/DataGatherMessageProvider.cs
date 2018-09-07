
using com.mh.model.messages.message;
using com.mh.rabbit.iservice;

namespace com.mh.rabbit.message
{
    public class MessageProvider : IMessageProvider<BaseMessage>
    {
        private  MessageClient _client;

        private void EnsureClient()
        {
            if (_client == null)
                _client = new MessageClient();
        }

        public IMessageSender<BaseMessage> GetSender()
        {
            EnsureClient();
            return _client;
        }

        public IMessageReceiver<BaseMessage> GetReceiver()
        {
            EnsureClient();
            return _client;
        }


    }
}
