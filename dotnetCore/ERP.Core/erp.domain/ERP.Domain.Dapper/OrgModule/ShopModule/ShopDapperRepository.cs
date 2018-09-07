using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Context;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.ShopModule;

using ERP.Domain.Repository.OrgModule.ShopModule;

namespace ERP.Domain.Dapper.OrgModule.ShopModule
{
    public class ShopRepository : RepositoryBase<Shop>, IShopRepository, IShopQueryRepository
    {
        private ShopSqlResource resource = new ShopSqlResource();

        public ShopRepository(IERPContext conn)
            : base(conn)
        {
        }

        private Func<Shop, Address, Shop> shopMap = (shop, address) =>
        {
            shop.Address = address;
            return shop;
        };

        public override Shop FirstOrDefault(Query query, Sort sort = null)
        {
            object param;
            var whereSql = GetWhereSql(query, out param);
            return Conn.Query(SqlUtility.CreateTop1SelectSql(SelectSql, whereSql, sort == null ? null : sort.Translate())
                , shopMap, param, Tran, splitOn: "Province", commandTimeout: Default_CommandTimeout).FirstOrDefault();
        }

        public Shop GetByMobileNo(string mobileNo)
        {
            Query query = Common.Infrastructure.Dapper.QueryObject.Query.Create().Add(Criterion.Create("LeadUserMobileNo", mobileNo, CriteriaOperator.Equal));
            return FirstOrDefault(query);
        }
        public override IEnumerable<Shop> QueryPaged(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (param == null) param = new { PageIndex = pager.PageIndex, PageSize = pager.PageSize };
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(this.SelectSql, whereSql), param);
            }
            return Conn.Query(CombinePagedSql(SelectSql, whereSql, sort.Translate()), shopMap, param, Tran,
                splitOn: "Province", commandTimeout: Default_CommandTimeout);
        }

        public override void Update(Shop entity)
        {
            Execute(UpdateSql, CreateParamster(entity));
        }

        public override void Insert(Shop entity)
        {
            Execute(InsertSql, CreateParamster(entity));
        }

        private object CreateParamster(Shop entity)
        {
            return new
            {
                Id = entity.Id,
                Name = entity.Name,
                ShopName = entity.ShopName,
                Address_Province = entity.Address.Province,
                Address_City = entity.Address.City,
                Address_County = entity.Address.County,
                Address_DetailAddress = entity.Address.DetailAddress,
                State = entity.State,
                LeadUserName = entity.LeadUserName,
                LeadUserMobileNo = entity.LeadUserMobileNo,
                City_Id = entity.City_Id
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
            get { return this.resource.Delete; }
        }

        public override string SelectSql
        {
            get { return this.resource.Select; }
        }
    }
}
