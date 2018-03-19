using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;
using com.mh.mongo.iservice;

namespace com.mh.mongo.service
{
    public class PluginExecuteService : IPluginExecuteService
    {
        public List<StoreExecute> GetStoreExecute(List<int> storeIds)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var table = dataBase.GetCollection<StoreExecute>(nameof(StoreExecute));
            return table.AsQueryable().Where(m => storeIds.Contains(m.StoreId) ).ToList();
        }
    }

}