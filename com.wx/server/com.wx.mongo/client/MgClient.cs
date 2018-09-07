using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using System.Linq.Expressions;
using com.wx.common.config;
using com.wx.mongo.data;
using com.wx.common.logger;

namespace com.wx.mongo.client
{
    public class MgClient
    {
        private static string _conn = AppSettingConfig.MgConn; //"mondodb://192.168.1.130:270117";
        private static string _dbName = AppSettingConfig.MgDBName; //"mg_coco";

        private static IMongoClient _client;
        private static IMongoDatabase _database;


        private static ILog _log = Logger.Current();

        public static void getClient()
        {
            _client = new MongoClient(_conn);
            _database = _client.GetDatabase(_dbName);

            Console.WriteLine("conn : " + _conn);
            Console.WriteLine("dbName : " + _dbName);

            _log.Info($"conn : {_conn} , dbName : {_dbName}");
        }


        /*
        public static IMongoClient GetClient()
        {
            return _client;
        }

        public static IMongoDatabase GetDB()
        {
            return _database;
        }*/




        public static void Insert<T>(T model) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            collection.InsertOneAsync(model);
            _log.Info($"mongo.InsertOneAsync");
        }

        public static List<T> Search<T>(FilterDefinition<T> filter) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            return collection.Find(filter).ToList();
        }

        public static List<T> Search<T>(FilterDefinition<T> filter, Expression<Func<T, object>> sort, bool isAsc, int pageSize, int pageIndex, out long total) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var find = collection.Find(filter);
            total = find.Count();

            if (isAsc)
                return find.SortBy(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
            else
                return find.SortByDescending(sort).Skip((pageIndex - 1) * pageSize).Limit(pageSize).ToList();
        }


        public static List<T> Search<T>() where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var filter = new BsonDocument();
            return collection.Find(filter).ToList();
        }

        public static long Update<T>(FilterDefinition<T> filter, UpdateDefinition<T> update) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var result = collection.UpdateOne(filter, update);
            return result.ModifiedCount;
        }

        public static long Delete<T>(FilterDefinition<T> filter) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var result = collection.DeleteMany(filter);
            return result.DeletedCount;
        }

        public static string Index<T>(IndexKeysDefinition<T> indexKeys) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var result = collection.Indexes.CreateOne(indexKeys);
            return result;
        }

        public static List<BsonDocument> Aggregate<T>(FilterDefinition<T> filter, ProjectionDefinition<T> group) where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<T>(new T().GetCollectionName());
            var result = collection.Aggregate().Match(filter).Group(group);
            return result.ToList();
        }


        public static void ListCollections()
        {
            if (_database == null)
                getClient();

            var result = _database.ListCollections();
            var list = result.ToList();
            list.ForEach(item =>
            {
                Console.WriteLine(item.ToJson());
            });
        }













        public static void CreateDefaultCounter<T>() where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();

            var collection = _database.GetCollection<counters>("counters");
            var filter = Builders<counters>.Filter.Eq("_id", typeof(T).Name.ToLower());
            var result = collection.Find(filter);
            if (result.FirstOrDefault() != null)
                return;
            collection.InsertOneAsync(new counters()
            {
                _id = typeof(T).Name.ToLower(),
                seq = 0
            });
        }


        public static int CreateNewId<T>() where T : MgBaseModel, new()
        {
            if (_database == null)
                getClient();
            var filter = Builders<counters>.Filter.Eq("_id", typeof(T).Name.ToLower());
            var update = Builders<counters>.Update.Inc("seq", 1);
            var options = new FindOneAndUpdateOptions<counters, counters>() { IsUpsert = true };

            var cancelToken = new System.Threading.CancellationTokenSource();
            var result = _database.GetCollection<counters>("counters").FindOneAndUpdate(filter, update, options, cancelToken.Token);
            return result.seq;

        }












        private async void Search_Demo<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).ToString());
            var filter = new BsonDocument();
            var count = 0;

            _collection.FindAsync(filter);

            using (var cursor = await _collection.FindAsync(filter))
            {
                while (await cursor.MoveNextAsync())
                {
                    var batch = cursor.Current;
                    foreach (var document in batch)
                    {
                        //process document
                        count++;
                    }
                }
            }

            var filter1 = Builders<T>.Filter.Eq("address.zipcode", "10075");
            var result1 = await _collection.Find(filter1).ToListAsync();

            var builder = Builders<T>.Filter;
            var filter2 = builder.Eq("cuisine", "Italian") & builder.Eq("address.zipcode", "10075");
            var result2 = await _collection.Find(filter2).ToListAsync();

            var builder2 = Builders<T>.Filter;
            var filter3 = builder2.Eq("cuisine", "Italian") | builder2.Eq("address.zipcode", "10075");
            var result3 = await _collection.Find(filter3).ToListAsync();

            //using FluentAssertions; 
            //result3.Count().Should().Be(1153);
        }

        private async void Update_Demo<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).ToString());

            //Update Top-Level Fields
            var filter = Builders<T>.Filter.Eq("name", "Juni");
            var update = Builders<T>.Update.Set("cuisine", "American (New)").CurrentDate("lastModified");
            var result = await _collection.UpdateOneAsync(filter, update);

            //Update an Embedded Field
            var filter2 = Builders<T>.Filter.Eq("restaurant_id", "41156888");
            var update2 = Builders<T>.Update.Set("address.street", "East 31st Street");
            var result2 = await _collection.UpdateOneAsync(filter2, update2);

            //Update Multiple Documents
            var builder3 = Builders<T>.Filter;
            var filter3 = builder3.Eq("address.zipcode", "10016") & builder3.Eq("cuisine", "Other");
            var update3 = Builders<T>.Update.Set("cuisine", "Category To Be Determined").CurrentDate("lastModified");
            var result3 = await _collection.UpdateOneAsync(filter3, update3);

        }

        private async void Delete_Demo<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).ToString());

            //Remove All Documents That Match a Condition
            var filter1 = Builders<T>.Filter.Eq("borough", "Manhattan");
            var result1 = await _collection.DeleteManyAsync(filter1);

            //Remove All Documents
            //To remove all documents from a collection, pass an empty conditions document to the DeleteManyAsync method.
            var filter = new BsonDocument();
            var result = await _collection.DeleteManyAsync(filter);

        }

        private async void Drop_Demo()
        {
            await _database.DropCollectionAsync("restaurants");


            //====>> for Assertions
            using (var cursor = await _database.ListCollectionsAsync())
            {
                var collections = await cursor.ToListAsync();
                //collections.Should().NotContain(document => document["name"] == "restaurants");
            }
        }

        private async void Aggregate_Demo<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).ToString());

            var aggregate1 = _collection.Aggregate().Group(new BsonDocument { { "_id", "$borough" }, { "count", new BsonDocument("$sum", 1) } });
            var results1 = await aggregate1.ToListAsync();


            var aggregate2 = _collection.Aggregate()
                .Match(new BsonDocument { { "borough", "Queens" }, { "cuisine", "Brazilian" } })
                .Group(new BsonDocument { { "_id", "$address.zipcode" }, { "count", new BsonDocument("$sum", 1) } });
            var results2 = await aggregate2.ToListAsync();
        }

        private async void Index_Demo<T>()
        {
            var _collection = _database.GetCollection<T>(typeof(T).ToString());

            var keys = Builders<T>.IndexKeys.Ascending("field1").Descending("field2");
            await _collection.Indexes.CreateOneAsync(keys);


            //===>> for Assertions
            using (var cursor = await _collection.Indexes.ListAsync())
            {
                var indexes = await cursor.ToListAsync();
                //indexes.Should().Contain(index => index["name"] == "cuisine_1");
            }
        }


    }
}
