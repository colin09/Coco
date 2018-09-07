using MongoDB.Driver;
using MongoDB.Bson;

namespace com.fs.mongo.client
{
    public class MongoDbClient
    {
        private static MongoClient _client;

        public static IMongoDatabase GetDatabase()
        {
            var conn = @"mongodb://root:dev_mongo@182.92.7.70/?maxConnections=2000";
            var dbName = "magichorse_develop";
            if (_client == null)
            {
                var url = new MongoUrl(conn);
                var setting = MongoClientSettings.FromUrl(url);
                //setting.ConnectTimeout = new TimeSpan(0,10,0);
                //setting.MaxConnectionPoolSize = 2000;
                //setting.WaitQueueSize = 2000;
                _client = new MongoClient(setting);
            }
            return _client.GetDatabase(dbName);
        }

    }
}