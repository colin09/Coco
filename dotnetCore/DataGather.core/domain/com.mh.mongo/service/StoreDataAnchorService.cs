
using System;
using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;
using com.mh.mongo.iservice;

namespace com.mh.mongo.service
{
    public class StoreDataAnchorService : IStoreDataAnchorService
    {
        public void AddStoreDataPressLog(StoreDataPressLog log)
        {
            var statDataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var cl = statDataBase.GetCollection<StoreDataPressLog>(nameof(StoreDataPressLog));

            var l = cl.Find(f => f.StoreId == log.StoreId && f.Time == log.Time.Date && f.Type == log.Type && f.Status == StoreDataPressStatus.UnDeal)
                    .FirstOrDefault();
            if (l == null)
            {
                cl.InsertOneAsync(log);
            }
        }

        public void AddStoreDataPressLogList(List<StoreDataPressLog> list)
        {
            var now = DateTime.Now;
            var dList = list.Select(l => new StoreDataPressLog
            {
                CreateDate = now,
                Time = l.Time,
                Status = l.Status,
                StoreId = l.StoreId,
                Type = l.Type
            }).Distinct().ToList();
            foreach (var log in dList)
            {
                AddStoreDataPressLog(log);
            }
        }
    }

}