using System.Linq;
using System.Collections.Generic;
using MongoDB.Driver;

using com.mh.model.mongo.dbMh;
using com.mh.mongo.iservice;

namespace com.mh.mongo.service
{
    public class PrivilegeService : IPrivilegeService
    {
        public void UsePrivilegeCoupon(List<string> codes)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<UserPrivilege>(nameof(UserPrivilege));
            
            var filter = Builders<UserPrivilege>.Filter.Where(p => p.PrivilegeItem.Any(c => codes.Contains(c.CouponPwd)) &&
                                                                   p.Status != UserPrivilegeStatusEnum.Used);
            var update = Builders<UserPrivilege>.Update.Set(p => p.Status, UserPrivilegeStatusEnum.Used);

            collection.UpdateManyAsync(filter, update);
        }
    }

}