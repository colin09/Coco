using System.Linq;
using MongoDB.Driver;
using System.Collections.Generic;

using com.mh.model.enums;
using com.mh.model.mongo.dbMh;
using com.mh.mongo.iservice;

namespace com.mh.mongo.service
{
    public class CouponService : ICouponService
    {       

        public List<CouponInfo> GetCouponList(int storeId, List<string> codes)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<CouponInfo>(nameof(CouponInfo));
            var query = collection.AsQueryable().Where(m => codes.Contains(m.Code));
            if (storeId > 0)
                query = query.Where(m => m.StoreId == storeId);

            return query.ToList();
        }

        public void UseCouponInfo(List<string> codes)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<CouponInfo>(nameof(CouponInfo));

            //var filter = Builders<CouponInfo>.Filter.Where(c => codes.Contains(c.Code) && c.Status == CouponStatus.Available);
            var filter = Builders<CouponInfo>.Filter.Where(c => codes.Contains(c.Pwd) && c.Status != CouponStatus.Used);
            var update = Builders<CouponInfo>.Update.Set(p => p.Status, CouponStatus.Used);

            collection.UpdateManyAsync(filter, update);
        }
    }

}