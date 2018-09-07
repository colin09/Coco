using System;
using MongoDB.Driver;
using System.Linq.Expressions;
using System.Collections.Generic;

namespace com.fs.mongo.client
{

    public class MongoDbExecute
    {

        public static void Insert<T>(T t)
        {
            var client = MongoDbClient.GetDatabase();
            var collection = client.GetCollection<T>(nameof(T));

            collection.InsertOne(t);
        }

        public static void InsertMany<T>(List<T> list)
        {
            var client = MongoDbClient.GetDatabase();
            var collection = client.GetCollection<T>(nameof(T));

            collection.InsertMany(list);
        }

        public static List<T> Find<T>(FilterDefinition<T> filter)
        {
            var client = MongoDbClient.GetDatabase();
            var collection = client.GetCollection<T>(nameof(T));

            var query = collection.Find(filter);
            return query.ToList();
        }

        public static List<T> Find<T>(FilterDefinition<T> filter, bool isAsc, Expression<Func<T, object>> sort)
        {
            var client = MongoDbClient.GetDatabase();
            var collection = client.GetCollection<T>(nameof(T));

            var query = collection.Find(filter);
            if (isAsc)
                return query.SortBy(sort).ToList();
            else
                return query.SortByDescending(sort).ToList();
        }
    }
}