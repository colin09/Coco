using RabbitMQ.Client;
using com.wx.common.config;

namespace com.wx.mq.client
{
    public class MqClientContext
    {
        public MqClientContext()
        {
            SendExchageName = AppSettingConfig.MqExchange;
            SendQueueName = AppSettingConfig.MqQueue;

            ListenExchageName = AppSettingConfig.MqExchange;
            ListenQueueName = AppSettingConfig.MqQueue;
        }

        public IConnection SendCollection { set; get; }

        public IModel SendChannel { set; get; }

        public string SendQueueName { set; get; }

        public string SendExchageName { set; get; }



        /****************************************************************/


        public IConnection ListenCollection { set; get; }

        public IModel ListenChannel { set; get; }

        public string ListenQueueName { set; get; }

        public string ListenExchageName { set; get; }


    }
}
