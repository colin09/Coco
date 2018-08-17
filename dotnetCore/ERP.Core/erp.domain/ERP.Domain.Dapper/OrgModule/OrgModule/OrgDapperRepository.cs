using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;

using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Repository.OrgModule;

namespace ERP.Domain.Dapper.OrgModule
{
    public class OrgRepository : RepositoryBase<Org>, IOrgRepository, IOrgQueryRepository
    {
        private OrgSqlResource sqlResource = new OrgSqlResource();
        public OrgRepository(IERPContext context)
            : base(context)
        {

        }
        public OrgSqlResource SqlResource
        {
            get { return sqlResource; }
        }

        public override string InsertSql
        {
            get { throw new NotImplementedException(); }
        }

        public override string UpdateSql
        {
            get { throw new NotImplementedException(); }
        }

        public override string DeleteSql
        {
            get { throw new NotImplementedException(); }
        }

        public override string SelectSql
        {
            get { return this.SqlResource.Select; }
        }
        private Func<Org, OrgAddress, Org> map = (org, address) =>
        {
            org.Address = address;
            return org;
        };
        public override Org FirstOrDefault(Query query, Sort sort = null)
        {
            object param;
            var whereSql = GetWhereSql(query, out param);
            return Conn.Query(SqlUtility.CreateTop1SelectSql(SelectSql, whereSql, sort == null ? null : sort.Translate())
                , map, param, Tran, splitOn: "Province").FirstOrDefault();
        }

        public override IEnumerable<Org> QueryPaged(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (param == null) param = new { PageIndex = pager.PageIndex, PageSize = pager.PageSize };
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(this.SelectSql, whereSql), param);
            }
            return Conn.Query(CombinePagedSql(SelectSql, whereSql, sort.Translate()), map, param, Tran, splitOn: "Province");
        }

        public override IEnumerable<Org> Query(Query query, Sort sort)
        {
            object param;
            string sql = GetQuerySql(query, sort, out param);
            return Conn.Query(sql, map, param, Tran, splitOn: "Province");
        }
    }
}
