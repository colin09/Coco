using com.wx.common.helper;
using com.wx.common.logger;
using com.wx.ioc.IOCAdapter;
using com.wx.message;
using com.wx.message.messageHandler;
using com.wx.mq.message;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

[assembly: log4net.Config.XmlConfigurator(ConfigFile = @"Configuration\Log4Net.config", Watch = true)]
namespace com.wx.mq.client
{
    public class MqClient
    {
        MqClientContext Context = new MqClientContext();
        private ILog _log;

        public MqClient()
        {
            //_log = LocatorFacade.Current.Resolve<ILog>();
            _log = Logger.Current();
        }

        public void TriggerEventMessage(MqEventMessage eventMessage, string exChange = null/*, string queue = null*/)
        {
            exChange = exChange ?? Context.SendExchageName;
            /*queue = queue ?? Context.SendQueueName;*/

            Context.SendCollection = MqClientFactory.CreateConnection();
            using (Context.SendCollection)
            {
                Context.SendChannel = MqClientFactory.CreateModel(Context.SendCollection);

                //non-persistent:1 , persistent:2 
                const byte deliveryMode = 2;
                using (Context.SendChannel)
                {
                    Context.SendChannel.ExchangeDeclare(exchange: exChange, type: "direct");
                    //Context.SendChannel.QueueBind(queue: queue, exchange: exChange, routingKey: eventMessage.EventType.ToString());

                    /*
                    Context.SendChannel.QueueDeclare(
                        queue: queue,
                        durable: true,
                        exclusive: false,
                        autoDelete: false,
                        arguments: null);
                    */

                    var properties = Context.SendChannel.CreateBasicProperties();
                    properties.DeliveryMode = deliveryMode;//持久化消息

                    Context.SendChannel.BasicPublish(
                        exchange: exChange,
                        routingKey: eventMessage.EventType.ToString(),
                        basicProperties: properties,
                        body: eventMessage.ToJson().ToByte());

                    //Console.WriteLine("exChange：{0} , queue: {1} ,routingKey: {2}", exChange, "", eventMessage.EventType.ToString());
                    _log.Info(string.Format("send message ok ：{0}", eventMessage.ToJson()));
                }

            }
        }



        /// <summary>
        /// 按照routtingKey消费
        /// </summary>
        /// <param name="exChange"></param>
        /// <param name="routtingKey"></param>
        public void ListenEventMessage(string exChange = null, Array routtingKey = null)
        {
            exChange = exChange ?? Context.ListenExchageName;

            Context.ListenCollection = MqClientFactory.CreateConnection();
            Context.ListenCollection.ConnectionShutdown += (obj, args) =>
            {
                _log.Info($"ConnectionShutdown ==> {args.ReplyText}");
            };

            Context.ListenChannel = MqClientFactory.CreateModel(Context.ListenCollection);
            Context.ListenChannel.ExchangeDeclare(exchange: exChange, type: "direct");

            //多个routkey 建议使用此方式获取queueName ，在发送时可不指定具体的queueName
            var queueName = Context.ListenChannel.QueueDeclare().QueueName;

            //单一的routKey可用
            //Context.ListenChannel.QueueBind(queue: queueName, exchange: exChange, routingKey: routtingKey);

            if (routtingKey == null)
                routtingKey = Enum.GetValues(typeof(MqEventType));

            _log.Info(string.Format("exChange：{0} , queue: {1}", exChange, queueName));
            //多个routkey，则依次绑定
            foreach (var item in routtingKey)
            {
                Context.ListenChannel.QueueBind(queue: queueName, exchange: exChange, routingKey: item.ToString());
                _log.Info(string.Format("add routkey：{0} ", item.ToString()));
            }

            //一次只获取一个信息进行消费
            Context.ListenChannel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);
            var consumer = new EventingBasicConsumer(Context.ListenChannel);
            consumer.Received += consumer_Received;
            Context.ListenChannel.BasicConsume(queue: queueName, noAck: false, consumer: consumer);
        }




        /// <summary>
        /// 按照队列名称消费
        /// </summary>
        /// <param name="exChange"></param>
        /// <param name="queue">指定queue名称</param>
        public void ListenEventMessage_queue(string exChange = null, string queue = null)
        {
            exChange = exChange ?? Context.ListenExchageName;
            queue = queue ?? Context.ListenQueueName;

            Console.WriteLine("exChange：{0} , queue: {1} ", exChange, queue);

            Context.ListenCollection = MqClientFactory.CreateConnection();

            Context.ListenCollection.ConnectionShutdown += (obj, args) =>
            {
                Console.WriteLine(args.ReplyText);
            };

            Context.ListenChannel = MqClientFactory.CreateModel(Context.ListenCollection);

            Context.ListenChannel.ExchangeDeclare(exchange: exChange, type: "direct");

            //单一的routKey可用
            //Context.ListenChannel.QueueBind(queue: queue, exchange: exChange, routingKey: routKey);

            //多个routkey，则依次绑定
            foreach (var item in Enum.GetValues(typeof(MqEventType)))
            {
                Context.ListenChannel.QueueBind(queue: queue, exchange: exChange, routingKey: item.ToString());
                Console.WriteLine("add routkey：{0} ", item.ToString());
            }

            /*  若 BasicPublish 时，exchange="" 、routingKey=queueName ,可使用此方式
            Context.ListenChannel.QueueDeclare(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);
            */

            //一次只获取一个信息进行消费
            Context.ListenChannel.BasicQos(prefetchSize: 0, prefetchCount: 1, global: false);

            var consumer = new EventingBasicConsumer(Context.ListenChannel);
            consumer.Received += consumer_Received;

            Context.ListenChannel.BasicConsume(queue: queue, noAck: false, consumer: consumer);
        }


        private void consumer_Received(object sender, BasicDeliverEventArgs args)
        {
            try
            {
                var message = new MqEventMessage().BuildMsgResult(args.Body);

                //触发外部侦听事件
                //_log.Info("Consumer received message , ===============>>");
                //_log.Info("received message , ==>[{0}] {1}", args.RoutingKey, args.Body);
                _log.Info("received message , ==>[{0}] {1}", message.EventType, message.Body);


                var handler = GetMsgHandler(message.EventType);
                message.IsOperationOK = handler.Action(message.Body);
                _log.Info("message.IsOperationOK , ==> {0}", message.IsOperationOK);
                if (!message.IsOperationOK)
                {
                    //未能消费此消息，重新放入队列头
                    Context.ListenChannel.BasicReject(deliveryTag: args.DeliveryTag, requeue: true);
                }
                else if (!Context.ListenChannel.IsClosed)
                {
                    Context.ListenChannel.BasicAck(deliveryTag: args.DeliveryTag, multiple: false);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex.Message);
            }
        }


        private IMessageHandler GetMsgHandler(MqEventType eventType)
        {
            IMessageHandler handler;
            switch (eventType)
            {
                case MqEventType.Outsite:
                    handler = new OutsiteHandler();
                    //_log.Info("GetMsgHandler , ==> {0}", nameof(OutsiteHandler));
                    break;
                case MqEventType.StoreQr:
                    handler = new StoreQrHandler();
                    //_log.Info("GetMsgHandler , ==> {0}", nameof(StoreQrHandler));
                    break;
                case MqEventType.ScanQr:
                    handler = new ScanQrHandler();
                    break;
                default: handler = null; break;
            }
            return handler;
        }











        /// <summary>
        /// 官方 sender
        /// </summary>
        public void Sender()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //创建 connection
            using (var connection = factory.CreateConnection())
            //创建 chanel
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                                     durable: false,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                string message = "Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "task_queue",
                                     basicProperties: null,
                                     body: body);
                Console.WriteLine(" [x] Sent {0}", message);
            }
        }

        /// <summary>
        /// 官方 receive
        /// </summary>
        public void Receive()
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "hello",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: "hello",
                    noAck: true,
                    consumer: consumer);
            }
        }

    }
}
