
using com.mh.model.messages.datagatherMessage;
using com.mh.rabbit.iservice;

namespace com.mh.rabbit.datagather
{
    public class DataGatherMessageProvider : IMessageProvider<DataGatherBaseMessage>
    {
        private DataGatherMessageClient _client;

        private void EnsureClient()
        {
            if (_client == null)
                _client = new DataGatherMessageClient();
        }

        public IMessageSender<DataGatherBaseMessage> GetSender()
        {
            EnsureClient();
            return _client;
        }

        public IMessageReceiver<DataGatherBaseMessage> GetReceiver()
        {
            EnsureClient();
            return _client;
        }


    }
}
