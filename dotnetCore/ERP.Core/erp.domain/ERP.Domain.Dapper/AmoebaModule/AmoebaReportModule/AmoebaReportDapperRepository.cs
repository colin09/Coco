using Dapper;
using System;
using System.Collections.Generic;

using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.AmoebaReportModule;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Dapper.QueryObject;

using ERP.Domain.Repository.AmoebaModule.AmoebaReportModule;

namespace ERP.Domain.Dapper.AmoebaModule.AmoebaReportModule
{
    public class AmoebaReportRepository : RepositoryBase<AmoebaReport>, IAmoebaReportRepository, IAmoebaReportQueryRepository
    {
        private AmoebaReportSqlResource sqlResource = new AmoebaReportSqlResource();
        public AmoebaReportRepository(IERPContext conn)
            : base(conn)
        {
        }
        public override string SelectSql { get { return this.sqlResource.Select; } }

        public override string InsertSql { get { return this.sqlResource.Insert; } }

        public override string UpdateSql { get { return this.sqlResource.Update; } }

        public override string DeleteSql { get { return this.sqlResource.Delete; } }


        public IEnumerable<string> QueryMonthCityIds(string month, string[] Ids)
        {
            return Conn.Query<string>(sqlResource.SelectMonthCitys, new { dataMonth = month, CityIds = Ids }, commandTimeout: Default_CommandTimeout);
        }

        public IEnumerable<DateTime> QueryImportDate(string id)
        {
            return Conn.Query<DateTime>(sqlResource.SelectImportDate, new { CityId = id }, commandTimeout: Default_CommandTimeout);
        }
        public IEnumerable<string> QueryLastMonth(string id)
        {
            return Conn.Query<string>(sqlResource.SelectLastMonth, new { CityId = id }, commandTimeout: Default_CommandTimeout);

            //var result = Conn.Query<object>(sqlResource.SelectLastMonth, new { CityId = id });
            //return null;
        }

        public IEnumerable<TAny> QueryDataRanking<TAny>(string cloumnName, string month)
        {
            var sql = string.Format(sqlResource.SelectRanking, cloumnName);
            return Conn.Query<TAny>(sql, new { dataMonth = month }, commandTimeout: Default_CommandTimeout);
        }

        public IEnumerable<AmoebaReport> QuerySimpleInfo(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            param = AddPagedParams(param, pager);
            if (pager.IsGetTotalCount)
            {
                pager.TotalCount = this.Count(CombineCountSql(sqlResource.SelectSimpleInfo, whereSql), param);
            }
            return Conn.Query<AmoebaReport, City, AmoebaReport>(CombinePagedSql(sqlResource.SelectSimpleInfo, whereSql, sort.Translate()),
                (report, city) => { report.City = city; return report; }, param, Tran, splitOn: "Name", commandTimeout: Default_CommandTimeout);
        }



        public IEnumerable<AmoebaReport> QueryAll(Query query, Pager pager, Sort sort)
        {
            object param;
            string whereSql = GetWhereSql(query, out param);
            var querySql = CombineSql(sqlResource.SelectAll, whereSql, sort.Translate());
            if (pager != null)
            {
                param = AddPagedParams(param, pager);
                if (pager.IsGetTotalCount)
                {
                    pager.TotalCount = this.Count(CombineCountSql(sqlResource.SelectAll, whereSql), param);
                }
                querySql = CombinePagedSql(sqlResource.SelectAll, whereSql, sort.Translate());
            }
            return Conn.Query<AmoebaReport, City, AmoebaReport>(querySql,
                (report, city) => { report.City = city; return report; }, param, Tran, splitOn: "Name", commandTimeout: Default_CommandTimeout);
        }




    }
}
