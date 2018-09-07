using System;
using MongoDB.Bson;
using MongoDB.Driver;
using com.mh.common.configuration;
using com.mh.common.extension;

namespace com.mh.mongo
{
    public class MongoDBClient
    {
        private static MongoClient _client;

        private static IMongoDatabase _mongoDbDataBase;
        private static IMongoDatabase _mongoTSourceDataBase;
        private static IMongoDatabase _mongoRankAndKittyDataBase;
        private static IMongoDatabase _mongoMagicalHorseStatDataBase;



        public static IMongoDatabase GetDataBase()
        {
            return GetDataBase(MongoDataBase.Default);
        }

        public static IMongoDatabase GetDataBase(MongoDataBase dataBase)
        {
            if (_client == null)
            {
                var url = new MongoUrl(ConfigManager.MongoDBConnectionString);
                var setting = MongoClientSettings.FromUrl(url);
                setting.ConnectTimeout = new TimeSpan(0, 10, 0);//设置5分钟超时
                //setting.MaxConnectionPoolSize = 2000;
                //setting.WaitQueueSize = 2000;
                //#if !DEBUG
                //                setting.ConnectionMode = ConnectionMode.ShardRouter;
                //                setting.ReadPreference = ReadPreference.SecondaryPreferred;
                //#endif
                _client = new MongoClient(setting);
            }
            var mongoDataBase = dataBase;
            switch (mongoDataBase)
            {
                case MongoDataBase.Default:
                    if (_mongoDbDataBase == null) _mongoDbDataBase = _client.GetDatabase(ConfigManager.MongoChatServer);
                    return _mongoDbDataBase;

                case MongoDataBase.MagicalHorse:
                    if (_mongoRankAndKittyDataBase == null) _mongoRankAndKittyDataBase = _client.GetDatabase(ConfigManager.MongoMagichorse);
                    return _mongoRankAndKittyDataBase;

                case MongoDataBase.MagicalHorseStat:
                    if (_mongoMagicalHorseStatDataBase == null) _mongoMagicalHorseStatDataBase = _client.GetDatabase(ConfigManager.MongoMagicalHorseStat);
                    return _mongoMagicalHorseStatDataBase;

                case MongoDataBase.MagicalHorseThird:
                    if (_mongoTSourceDataBase == null) _mongoTSourceDataBase = _client.GetDatabase(ConfigManager.MongoDataSource);
                    return _mongoTSourceDataBase;

                default:
                    if (_mongoDbDataBase == null) _mongoDbDataBase = _client.GetDatabase(ConfigManager.MongoChatServer);
                    return _mongoDbDataBase;
            }
        }


    }


    public enum MongoDataBase
    {
        /// <summary>
        /// 默认的数据库
        /// </summary>
        Default = 0,

        /// <summary>
        /// 排行榜  Kitty相关数据库
        /// </summary>
        MagicalHorse = 1,

        /// <summary>
        /// 统计数据库
        /// </summary>
        MagicalHorseStat = 2,

        /// <summary>
        /// 第三方订单、会员相关数据库
        /// </summary>
        MagicalHorseThird = 3,
    }


}