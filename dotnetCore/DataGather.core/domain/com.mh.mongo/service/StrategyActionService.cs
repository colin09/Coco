using System;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Linq;
using System.Collections.Generic;

using com.mh.model.mongo.dbMh;
using com.mh.mongo.iservice;

namespace com.mh.mongo.service
{
    public class StrategyActionService : IStrategyActionService
    {
        public void WriteConversionRate(Dictionary<string, int> dic)
        {
            
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StrategyAction>(nameof(StrategyAction));

            var ids = dic.Keys.Select(k => new ObjectId(k)).ToList();
            var mList = collection.AsQueryable().Where(s => ids.Contains(s.Id));

            if (mList.Any())
            {
                foreach (var item in mList.ToList())
                {
                    int count = dic[item.StringId];
                    var rate = (item.CustomerCostCount + count) / (double)item.CustomerCount;

                    var filter = Builders<StrategyAction>.Filter.Eq(s => s.Id, item.Id);
                    var update = Builders<StrategyAction>.Update.Inc(s => s.CustomerCostCount, count).Set(s => s.ConversionRate, rate);
                    collection.UpdateOneAsync(filter, update);
                }
            }
        }

        public void WriteMemberBack(List<ActionEffect> list)
        {
            
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StrategyEffect>(nameof(StrategyEffect));

            var date = list.Min(m => m.PaymentTime).AddDays(-30);
            var memberIds = list.Select(l => l.MemberId).ToList();
            var effectList = collection.AsQueryable().Where(e => memberIds.Contains(e.MemberId) && e.CreateDate > date).ToList();
            list.ForEach(l =>
            {
                var eDate = l.PaymentTime.AddDays(-30);
                var effects = effectList.Where(e => e.MemberId == l.MemberId && e.CreateDate > eDate && e.CreateDate < l.PaymentTime);

                if (!string.IsNullOrEmpty(l.ActionId))
                    effects = effects.Where(e => e.Action.Id == l.ActionId);

                if (effects.Any())
                {
                    foreach (var effect in effects.ToList())
                    {
                        var filter = Builders<StrategyEffect>.Filter.Eq("_id", effect.Id);
                        var backlist = new List<BackInfo>();

                        if (effect.BackList != null && effect.BackList.Any())
                            backlist = effect.BackList;

                        backlist.Add(new BackInfo()
                        {
                            BackDate = l.PaymentTime,
                            BackDateGroup = new DateGroup(l.PaymentTime)
                        });

                        var update = Builders<StrategyEffect>.Update
                            .Set(m => m.UserIsBack, true)
                            .Set(m => m.UpdateDate, DateTime.Now)
                            .Set(m => m.BackDate, l.PaymentTime)
                            .Set(m => m.BackDateGroup, new DateGroup(l.PaymentTime))
                            .Set(m => m.BackList, backlist);

                        collection.UpdateOne(filter, update);
                    }
                }
            });
        }
    }

}