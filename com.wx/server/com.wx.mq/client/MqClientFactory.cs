using com.wx.common.config;
using com.wx.common.logger;
using RabbitMQ.Client;
using System;

namespace com.wx.mq.client
{
    public class MqClientFactory
    {
      static  ILog log = Logger.Current();
       public static IConnection CreateConnection()
       {
           var factory = new ConnectionFactory()
           {
               HostName =AppSettingConfig.MqHost,
               //Port = 15672,
               UserName = AppSettingConfig.MqUser,
               Password = AppSettingConfig.MqPwd
           };
            log.Info("host:{0}, user:{1}, pwd:{2}", AppSettingConfig.MqHost, AppSettingConfig.MqUser, AppSettingConfig.MqPwd);
           return factory.CreateConnection();
       }


       public static IModel CreateModel(IConnection connection)
       {
           return connection.CreateModel();
       } 


    }
}
