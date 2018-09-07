using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Repository.OrgModule.CityModule;

namespace ERP.Domain.Dapper.OrgModule.CityModule
{
    public class CityRepository : RepositoryBase<City>, ICityRepository, ICityQueryRepository
    {
        private CitySqlResource resource = new CitySqlResource();

        private OrgSqlResource orgSqlResource = new OrgSqlResource();
        public CityRepository(IERPContext conn)
            : base(conn)
        {
        }

        public CitySqlResource SqlResource
        {
            get { return resource; }
        }

        public override string InsertSql
        {
            get { return this.SqlResource.Insert; }
        }

        public override string UpdateSql
        {
            get { return this.SqlResource.Update; }
        }

        public override string DeleteSql
        {
            get { throw new NotImplementedException(); }
        }

        public override string SelectSql
        {
            get { return this.SqlResource.Select; }
        }

        public override string TableAliasName { get { return "City"; } }

        private Func<City, OrgAddress, City> cityMap = (city, address) =>
        {
            city.Address = address;
            return city;
        };

        public override City FirstOrDefault(Query query, Sort sort = null)
        {
            object param;
            var whereSql = GetWhereSql(query, out param);
            return Conn.Query(SqlUtility.CreateTop1SelectSql(SelectSql, whereSql, sort == null ? null : sort.Translate())
                , cityMap, param, Tran, splitOn: "Province").FirstOrDefault();
        }

        public City GetHQCity()
        {
            return FirstOrDefault(Common.Infrastructure.Dapper.QueryObject.Query.Create(QueryOperator.Or)
                .Add(Criterion.Create("NewId", "898", CriteriaOperator.Equal))
                .Add(Criterion.Create("Name", "易酒批统采", CriteriaOperator.Equal)));
        }

        public City GetAgentCity()
        {
            return FirstOrDefault(Common.Infrastructure.Dapper.QueryObject.Query.Create(QueryOperator.Or)
                .Add(Criterion.Create("NewId", "897", CriteriaOperator.Equal))
                .Add(Criterion.Create("Name", "易酒批代理", CriteriaOperator.Equal)));
        }

        public override IEnumerable<City> QueryPaged(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (param == null) param = new { PageIndex = pager.PageIndex, PageSize = pager.PageSize };
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(this.SelectSql, whereSql), param);
            }
            return Conn.Query(CombinePagedSql(SelectSql, whereSql, sort.Translate()), cityMap, param, Tran,
                splitOn: "Province", commandTimeout: Default_CommandTimeout);
        }

        public IEnumerable<City> SearchOnLine(Query query, Sort sort = null)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            sort = sort == null ? Sort.DefaultSort : sort;
            return Conn.Query(CombineSql(SqlResource.SearchOnLine, whereSql, sort.Translate()), cityMap, param, Tran,
                splitOn: "Province", commandTimeout: Default_CommandTimeout);
        }


        public override IEnumerable<City> Query(Query query, Sort sort)
        {
            object param;
            string sql = GetQuerySql(query, sort, out param);
            return Conn.Query(sql, cityMap, param, Tran, splitOn: "Province", commandTimeout: Default_CommandTimeout);
        }

        public override void Insert(City entity)
        {
            Execute(orgSqlResource.Insert, CreateOrgParameters(entity));
            Execute(resource.Insert, entity);
        }

        public override void Update(City entity)
        {
            Execute(resource.Update, entity);
            Execute(orgSqlResource.Update, CreateOrgParameters(entity));
        }

        private object CreateOrgParameters(City city)
        {
            return new
            {
                Id = city.Id,
                NewId = city.NewId,
                Name = city.Name,
                Enable = city.Enable,
                Address_Province = city.Address == null ? null : city.Address.Province,
                Address_City = city.Address == null ? null : city.Address.City,
                Address_County = city.Address == null ? null : city.Address.County,
                Address_DetailAddress = city.Address == null ? null : city.Address.DetailAddress,
                Address_Latitude = city.Address == null ? null : city.Address.Latitude,
                Address_Longitude = city.Address == null ? null : city.Address.Longitude,
                OrgType = city.OrgType
            };
        }

        public IEnumerable<City> GetByIds(string[] ids)
        {
            return this.Query(Common.Infrastructure.Dapper.QueryObject.Query.Create()
                .Add(Criterion.Create("Id", ids, CriteriaOperator.In, "City")),
                    Sort.Create().Add(SortCondition.Create("CreateTime", false, "Orgs")));
        }

        /// <summary>
        /// id , newId
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public City GetById(string id)
        {
            return Conn.Query<City>(SqlResource.SelectById, new { id = id }, Tran, commandTimeout: Default_CommandTimeout).FirstOrDefault();
        }


    }
}
