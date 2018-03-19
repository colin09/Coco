
using System.Collections.Generic;
using System.Linq;
using com.mh.model.enums;
using com.mh.model.mongo.dbMhSt;
using com.mh.model.mongo.mgBase;
using com.mh.mongo.iservice;
using MongoDB.Driver;

namespace com.mh.mongo.service
{
    public class StorePreferenceService : IStorePreferenceService
    {
        private List<dim_storepreference> StorePreferenceForVipLevel;


        public string GetVipLevelName(int storeId, string vipLevelStr)
        {
            if (StorePreferenceForVipLevel == null || StorePreferenceForVipLevel.Count == 0)
            {
                var mongoDb = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorseStat);
                var table = mongoDb.GetCollection<dim_storepreference>(MagicHorseStBase.CollectionName<dim_storepreference>());

                var query = table.Find(t => t.storeid == storeId && t.preference_type_id == (int)MemberPreferenceType.PreferenceVipcardLevel && t.sub_preference_type_name != "");

                StorePreferenceForVipLevel = query.ToList();
            }

            var vipLevel = 0;
            bool flag = int.TryParse(vipLevelStr, out vipLevel);

            if (flag)
            {
                var m = StorePreferenceForVipLevel.FirstOrDefault(a => a.sub_preference_type_id == vipLevel);
                if (m != null)
                    return m.sub_preference_type_name;
            }
            return null;
        }
    }

}