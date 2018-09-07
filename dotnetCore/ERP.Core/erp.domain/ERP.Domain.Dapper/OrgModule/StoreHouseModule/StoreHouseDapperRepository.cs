using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;

using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Enums.OrgModule;

using ERP.Domain.Repository.OrgModule.StoreHouseModule;

namespace ERP.Domain.Dapper.OrgModule.StoreHouseModule
{
    public class StoreHouseRepository : RepositoryBase<StoreHouse>, IStoreHouseRepository, IStoreHouseQueryRepository
    {
        private StoreHouseSqlResource resource = new StoreHouseSqlResource();
        private OrgSqlResource orgSqlResource = new OrgSqlResource();

        public StoreHouseRepository(IERPContext conn)
            : base(conn)
        {
        }
        public StoreHouseSqlResource SqlResource
        {
            get { return resource; }
        }

        public override string TableAliasName
        {
            get
            {
                return "StoreHouse";
            }
        }

        private Func<StoreHouse, OrgAddress, StoreHouse> storeHouseMap = (house, address) =>
        {
            house.Address = address ?? new OrgAddress();
            return house;
        };

        private Func<StoreHouse, OrgAddress, City, OrgAddress, StoreHouse> storeHouseCityMap = (sh, shAddress, c, cAddress) =>
        {
            sh.Address = shAddress;
            c.Address = cAddress;
            sh.City = c;
            return sh;
        };

        public bool Exists(string id, EnableState enable)
        {
            var result = this.Count(SqlResource.Exists, new { id, enable });
            return result > 0;
        }

        public override StoreHouse FirstOrDefault(Query query, Sort sort = null)
        {
            object param;
            var whereSql = GetWhereSql(query, out param);
            return Conn.Query(SqlUtility.CreateTop1SelectSql(SelectSql, whereSql, sort == null ? null : sort.Translate())
                , storeHouseMap, param, Tran, splitOn: "Province", commandTimeout: Default_CommandTimeout).FirstOrDefault();
        }

        public override IEnumerable<StoreHouse> QueryPaged(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (param == null) param = new { PageIndex = pager.PageIndex, PageSize = pager.PageSize };
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(this.SelectSql, whereSql), param);
            }
            return Conn.Query(CombinePagedSql(SelectSql, whereSql, sort.Translate()), storeHouseMap, param, Tran,
                splitOn: "Province", commandTimeout: Default_CommandTimeout);
        }

        public override void Update(StoreHouse entity)
        {
            Execute(orgSqlResource.Update, CreateParamster(entity));
            Execute(UpdateSql, CreateParamster(entity));
        }

        public override void Insert(StoreHouse entity)
        {
            Execute(orgSqlResource.Insert, CreateParamster(entity));
            Execute(InsertSql, CreateParamster(entity));
        }

        private object CreateParamster(StoreHouse entity)
        {
            return new
            {
                Id = entity.Id,
                City_Id = entity.City_Id,
                IsSupplierStore = entity.IsSupplierStore,
                StoreHouseType = entity.StoreHouseType,
                Shop_Id = entity.Shop_Id,
                Name = entity.Name,
                Enable = entity.Enable,
                Address_Province = entity.Address.Province,
                Address_City = entity.Address.City,
                Address_County = entity.Address.County,
                Address_DetailAddress = entity.Address.DetailAddress,
                Address_Latitude = entity.Address.Latitude,
                Address_Longitude = entity.Address.Longitude,
                CreateTime = entity.CreateTime,
                LastUpdateTime = entity.LastUpdateTime,
                NewId = entity.NewId,
                OrgType = entity.OrgType
            };
        }

        public override string InsertSql
        {
            get { return this.resource.Insert; }
        }

        public override string UpdateSql
        {
            get { return this.resource.Update; }
        }

        public override string DeleteSql
        {
            get { throw new NotImplementedException(); }
        }

        public override string SelectSql
        {
            get { return this.resource.Select; }
        }

        public IEnumerable<StoreHouse> QueryIncludeCity(Query query)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);

            return Conn.Query(CombineSql(SqlResource.SelectIncludeCity, whereSql), storeHouseCityMap, param, Tran,
                splitOn: "Province,Id,Province", commandTimeout: Default_CommandTimeout);
        }

        public StoreHouse GetIncludeCity(string id)
        {
            return QueryIncludeCity(Common.Infrastructure.Dapper.QueryObject.Query.Create()
                .Add(Criterion.Create("Id", id, CriteriaOperator.Equal, "StoreHouse"))).FirstOrDefault();
        }

        public IEnumerable<StoreHouse> QueryIncludeCityByIds(string[] ids)
        {
            return QueryIncludeCity(Common.Infrastructure.Dapper.QueryObject.Query.Create(QueryOperator.Or)
                .Add(Criterion.Create("Id", ids, CriteriaOperator.In, "Orgs"))
                .AddSubQuery(
                Common.Infrastructure.Dapper.QueryObject.Query.Create().Add(Criterion.Create("NewId", null, CriteriaOperator.IsNotNull, "Orgs"))
                .Add(Criterion.Create("NewId", ids, CriteriaOperator.In, "Orgs"))
                ));
        }

        public override IEnumerable<StoreHouse> Query(Query query, Sort sort)
        {
            object param;
            var sql = CombineSql(resource.Select, GetWhereSql(query, out param));
            return Conn.Query(sql, storeHouseMap, param, Tran, splitOn: "Province");
        }

        public IEnumerable<StoreHouse> GetByIds(string[] ids)
        {
            return this.Query(
                Common.Infrastructure.Dapper.QueryObject.Query.Create()
                    .Add(Criterion.Create("Id", ids, CriteriaOperator.In, "StoreHouse")),
                Sort.Create().Add(SortCondition.Create("CreateTime", false, "Orgs")));
        }
    }
}
