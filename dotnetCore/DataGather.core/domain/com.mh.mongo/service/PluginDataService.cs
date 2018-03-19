using System;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;

using com.mh.mongo.iservice;
using com.mh.model.mongo.dbMh;
using com.mh.model.mongo.dbSource;
using com.mh.rabbit;
using com.mh.model.mongo.mgBase;
using com.mh.common.Logger;
using com.mh.common.ioc;

namespace com.mh.mongo.service
{
    public class PluginDataService : IPluginDataService
    {
        private static ILog log = IocProvider.GetService<ILog>();

        public List<StoreDataGather> GetStoreDataRules()
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StoreDataGather>(nameof(StoreDataGather));

            var list = collection.AsQueryable().Where(d => !d.LastSyncTime.HasValue || (d.LastSyncTime > DateTime.MinValue && d.LastSyncTime < DateTime.Now.Date)).ToList();

            return list;
        }

        public void ModifyStoreDataRuleLastId(int storeId, double lastId)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StoreDataGather>(nameof(StoreDataGather));

            var filter = Builders<StoreDataGather>.Filter.Eq(r => r.StoreId, storeId);
            var update = Builders<StoreDataGather>.Update.Set(r => r.LastId, lastId);

            collection.UpdateOne(filter, update);
        }

        public void ModifyStoreLastSyncTime(int storeId)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StoreDataGather>(nameof(StoreDataGather));

            var filter = Builders<StoreDataGather>.Filter.Eq(r => r.StoreId, storeId);
            var update = Builders<StoreDataGather>.Update.Set(r => r.LastSyncTime, DateTime.Now);

            collection.UpdateOneAsync(filter, update);
        }


        public List<StoreVipLevel> GetStoreVipLevels(int storeId)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<StoreVipLevel>(nameof(StoreVipLevel));

            var filter = Builders<StoreVipLevel>.Filter.Eq(m => m.StoreId, storeId);
            return collection.Find(filter).ToList();
        }





        public void AddMemberInfos(List<MemberInfo> list)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfo>(nameof(MemberInfo));

            collection.InsertManyAsync(list);
        }

        public void ModifyMemberAllInfos(List<MemberInfo> list)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfo>(nameof(MemberInfo));

            list.ForEach(l =>
            {
                var filter = Builders<MemberInfo>.Filter.Eq("_id", l.Id);
                var update = Builders<MemberInfo>.Update
                    .Set(m => m.SaleData, l.SaleData)
                    //.Set(m => m.VipCode, l.VipCode)
                    .Set(m => m.VipCode, l.VipCode)
                    .Set(m => m.MobilePhone, l.MobilePhone)
                    .Set(m => m.Birthday, l.Birthday)
                    .Set(m => m.BirthdayOfYear, l.BirthdayOfYear)
                    //.Set(m => m.SectionCode, l.SectionCode)
                    //.Set(m => m.SectionId, l.SectionId)
                    //.Set(m => m.OutSite.Detail, l.OutSite.Detail)
                    .Set(m => m.SubScribe, l.SubScribe)
                    .Set(o => o.OutSite, l.OutSite)
                    //.Set(o => o.OutSite.LoginName, l.OutSite.LoginName)
                    .Set(o => o.UpdateTime, DateTime.Now)
                    .Set(o => o.RegisterTime, l.RegisterTime);
                collection.UpdateOne(filter, update);


                MessagelistenerClient.SyncMemberInfoByMemberId(l.StringId);
            });
        }

        public void AddMemberVipChangeList(List<MemberInfoVipChange> list)
        {
            if (list == null || list.Count == 0)
                return;
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfoVipChange>(nameof(MemberInfoVipChange));
            collection.InsertMany(list);
        }

        public void AddMemberManagerList(List<MemberInfoManager> list, int storeId)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfoManager>(nameof(MemberInfoManager));

            Console.WriteLine($"new manager count:{list.Count}");

            list.ForEach(mem =>
            {
                if (string.IsNullOrEmpty(mem.StringId))
                {
                    mem.Id = mem.CreateObjectId();
                }
                collection.InsertOne(mem);

                MessagelistenerClient.SyncMemberInfoByManagerId(mem.StringId);
            });
            //collection.InsertMany(list);
        }

        public List<string> GetMemberManager(List<string> ids, int storeId)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfoManager>(nameof(MemberInfoManager));

            var query = collection.AsQueryable().Where(m => m.Status == MemberManagerState.Normal && ids.Contains(m.MemberId));
            if (storeId > 0)
                query = query.Where(m => m.StoreId == storeId);
            var list = query.Select(m => m.MemberId).ToList();
            return list;
        }
        public List<MemberInfo> GetMemberList(int storeId, List<string> ids = null, List<string> memberIds = null, List<string> openIds = null, List<string> vipCodes = null, List<int> userIds = null, List<string> loginNames = null)
        {
            var filter = Builders<MemberInfo>.Filter.Where(m => true);
            if (storeId > 0)
                filter = filter & Builders<MemberInfo>.Filter.Eq(m => m.StoreId, storeId);
            if (ids != null)
            {
                var mIds = ids.Select(id => new ObjectId(id)).ToList();
                filter = filter & Builders<MemberInfo>.Filter.In(m => m.Id, mIds);
            }
            else if (memberIds != null) filter = filter & Builders<MemberInfo>.Filter.In(m => m.MemberId, memberIds);
            else if (openIds != null) filter = filter & Builders<MemberInfo>.Filter.In(m => m.OutSite.OutSiteUId, openIds);
            else if (vipCodes != null) filter = filter & Builders<MemberInfo>.Filter.In(m => m.VipCode, vipCodes);
            else if (userIds != null) filter = filter & Builders<MemberInfo>.Filter.In(m => m.CustomerId, userIds);
            else if (loginNames != null) filter = filter & Builders<MemberInfo>.Filter.In(m => m.OutSite.LoginName, loginNames);
            else return null;

            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<MemberInfo>(nameof(MemberInfo));
            return collection.Find(filter).ToList();
        }

        public void AddMemberInfoList(List<SyncMemberInfoTemp> syncMemberTemps)
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<SyncMemberInfoTemp>(nameof(SyncMemberInfoTemp));

            collection.InsertManyAsync(syncMemberTemps);
        }






        public void AddPluginOrderInfoList<T>(List<T> list) where T : PluginOrderBase
        {
            if (list == null || list.Count < 1)
                return;
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<T>("pluginorderinfo");

            if (list.Any())
                collection.InsertMany(list);
            //collection.InsertManyAsync(list);
        }


        public List<string> GetPluginOrderList<T>(List<string> list, int storeId) where T : PluginOrderBase
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<T>("pluginorderinfo");

            var result = collection.AsQueryable().Where(o => list.Contains(o._result.md5key) && o.storeid == storeId).Select(o => o._result.md5key);
            if (result.Any())
                return result.ToList();
            return null;
        }


        public List<string> GetPluginOrderList<T>(List<string> list) where T : PluginOrderBase
        {
            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorse);
            var collection = dataBase.GetCollection<T>("pluginorderinfo");

            var result = collection.AsQueryable().Where(o => list.Contains(o._result.md5key)).Select(o => o._result.md5key);
            if (result.Any())
                return result.ToList();
            return null;
        }

        public List<string> ReadMidOrderIds(DateTime startTime, List<string> sectionCodes, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;
            var secCodes = $"['{string.Join("','", sectionCodes)}']";
            //_log.Info($"mg-query secCodes : {secCodes}");

            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorseThird);
            var collection = dataBase.GetCollection<dw_order_source>(MagicHorseStBase.CollectionName<dw_order_source>());

            var isoDate = $"ISODate('{startTime.AddHours(8).ToString("yyyy-MM-dd")}T{startTime.AddHours(8).ToString("HH:mm:dd")}.000+08:00')";

            var pipeLine = new BsonDocument[] {
                BsonDocument.Parse("{$match:{'cal_dt':{$gt:"+isoDate+"},'pay_time':{$ne:null},'settle_price':{$gt:0},'shop_no':{$in:"+secCodes+"} }}"),
                BsonDocument.Parse("{$sort:{'_id':1}}"),
                BsonDocument.Parse("{$skip:"+skip+"}"),
                BsonDocument.Parse("{$limit:"+pageSize+"}"),
                BsonDocument.Parse("{$project:{'_id':1}}"),
            };
            var query = collection.Aggregate<dynamic>(pipeLine);

            if(page==1)
                log.Info(pipeLine);
            var list = query.ToList().Select(l => new IdClass { id = l._id.ToString() }).Select(l => l.id).ToList();
            return list;
        }

        public List<PluginOrderTxt> ReadMidOrderList(int startIndex, List<string> ids)
        {
            var idStr = $"[ObjectId('{string.Join("'),ObjectId('", ids)}')]";

            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorseThird);
            var collection = dataBase.GetCollection<dw_order_source>(MagicHorseStBase.CollectionName<dw_order_source>());

            var match = "{$match:{'_id':{$in:" + idStr + "}} }";
            //_log.Info(match);

            var pipeLine = new BsonDocument[] {
                BsonDocument.Parse(match)
            };

            var query = collection.Aggregate<dynamic>(pipeLine);
            var list = query.ToList().Select(l => new PluginOrderTxt
            {
                index = startIndex++,
                ordercode = l.order_no,
                ordertype = l.order_type?.ToString(),
                businesstype = l.business_type?.ToString(),
                sku = l.item_code,
                productcode = l.item_no,
                productname = l.item_name,
                producttype = l.item_flag?.ToString(),
                color = l.color_name,
                size = l.size_no,
                brandcode = l.brand_no,
                quantityExt = l.qty?.ToString(),
                unitprice = l.tag_price?.ToString(),
                disount = l.item_discount?.ToString(),
                couponcode = l.ticket_code?.ToString(),

                paymentprice = l.item_amount?.ToString(),

                saleprice = l.sale_price?.ToString(),
                paymenttime = l.pay_time?.ToString(),
                customerid = l.memberid,
                //openid = l.member_openid,
                buyercode = l.assistant_no,
                remark = l.remark,
                storesection = l.shop_name,
                sectioncode = l.shop_no,
                ctime = l.cal_dt,
                billingcode = l.billing_code,

                //mainid = l.main_id,
                dtlid = l.dtl_id,
                settleprice = l.settle_price?.ToString(),
                inventory = l._id.ToString(),
            }).ToList();
            return list;
        }


        public List<dw_memberinfo_source> ReadMidMemberInfo(DateTime startTime, List<string> sectionCodes, int page, int pageSize)
        {
            var skip = (page - 1) * pageSize;

            var dataBase = MongoDBClient.GetDataBase(MongoDataBase.MagicalHorseThird);
            var collection = dataBase.GetCollection<dw_memberinfo_source>(MagicHorseStBase.CollectionName<dw_memberinfo_source>());

            startTime = startTime.AddHours(8);
            //var query = collection.Aggregate().Match(m => m.cal_dt > startTime && m.store_no != null).SortBy(m => m.Id).ThenBy(m => m.update_date).Skip(skip).Limit(pageSize);
            var query = collection.Aggregate()
                .Match(m => m.cal_dt > startTime && m.store_no != null && sectionCodes.Contains(m.store_no))
                .SortBy(m => m.Id)
                .Skip(skip)
                .Limit(pageSize);
            return query.ToList();
        }
    }



    public class IdClass
    {
        public string id { set; get; }
    }


}