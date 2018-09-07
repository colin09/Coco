using System;
using System.Text;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;


using com.mh.common.ioc;
using com.mh.common.Logger;
using com.mh.common.configuration;
using com.mh.rabbit.iservice;
using com.mh.model.messages;
using com.mh.model.messages.datagatherMessage;

namespace com.mh.rabbit.datagather
{
    class DataGatherMessageClient : IMessageReceiver<DataGatherBaseMessage>, IMessageSender<DataGatherBaseMessage>
    {
        private ConnectionFactory _factory = new ConnectionFactory()
        {
            HostName = ConfigManager.RabbitHost,
            UserName = ConfigManager.RabbitUserName,
            Password = ConfigManager.RabbitPassword
        };
        private IModel _model = null;
        private  ILog log => IocProvider.GetService<ILog>();






        public bool SendMessageReliable(DataGatherBaseMessage message)
        {
            return SendMessageReliable(message, null);
        }

        public bool SendMessageReliable(DataGatherBaseMessage message, Action<DataGatherBaseMessage> preMessageHandler)
        {
            return SendMessageReliable(message, preMessageHandler, message.QueueName);
        }

        public bool SendMessageReliable(DataGatherBaseMessage message, Action<DataGatherBaseMessage> preMessageHandler, string messageTopic)
        {
            if (preMessageHandler != null)
                preMessageHandler(message);
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var queueName = messageTopic;
                    channel.QueueDeclare(queueName, true, false, false, null);

                    var jsonMessage = JsonConvert.SerializeObject(message);
                    var body = Encoding.UTF8.GetBytes(jsonMessage);

                    var properties = channel.CreateBasicProperties();
                    properties.SetPersistent(true);
                    channel.BasicPublish("", queueName, properties, body);
                }
            }
            return true;
        }











        //!!!
        public void ReceiveReliable(Func<DataGatherBaseMessage, bool> postMessageHandler)
        {
            ReceiveReliable(postMessageHandler, ConfigManager.RabbitDataGatherQuenuName);
        }
        public void ReceiveReliable(Func<DataGatherBaseMessage, bool> postMessageHandler, string messageTopic)
        {
            ReceiveReliable(postMessageHandler, messageTopic, false);
        }
        public void ReceiveReliable(Func<DataGatherBaseMessage, bool> postMessageHandler
            , string messageTopic
            , bool discardFailMessage)
        {
            using (var connection = _factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    var _channel = channel;
                    var queueName = messageTopic;
                    channel.QueueDeclare(queueName, true, false, false, null);

                    channel.BasicQos(0, 1, false);
                    var consumer = new QueueingBasicConsumer(channel);
                    channel.BasicConsume(queueName, false, consumer);

                    while (true)
                    {
                        try
                        {
                            var ea = (BasicDeliverEventArgs)consumer.Queue.Dequeue();

                            var body = ea.Body;
                            var rawMessage = Encoding.UTF8.GetString(body);

                            var message = GetMessage(rawMessage);

                            log.Debug("receiveMsg:" + rawMessage);
                            if (message == null)
                                log.Error(rawMessage);
                            channel.BasicAck(ea.DeliveryTag, false);

                            if (message != null)
                            {
								if (!postMessageHandler(message) && !discardFailMessage)
								{ }
                            }
                        }
                        catch (OperationInterruptedException )
                        {
                            break;
                        }
                    }
                }
            }
        }












        private DataGatherBaseMessage GetMessage(string rawMessage)
        {
            try
            {
                var msg = JsonConvert.DeserializeObject<DataGatherBaseMessage>(rawMessage);

                switch (msg.ActionType)
                {
                    case (int)MessageAction.ScanBuyer: msg = JsonConvert.DeserializeObject<ScanBuyerMessage>(rawMessage); break;
                    case (int)MessageAction.NewFavorite: msg = JsonConvert.DeserializeObject<FavoriteDataMessage>(rawMessage); break;
                }

                return msg;
            }
            catch
            {
                return null;
            }
        }

        public void Cancel()
        {
            if (_model != null)
                _model.Close();
        }
    }
}