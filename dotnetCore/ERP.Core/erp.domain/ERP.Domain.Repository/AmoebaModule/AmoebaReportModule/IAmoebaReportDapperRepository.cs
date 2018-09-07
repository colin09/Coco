using System;
using System.Collections.Generic;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model.AmoebaReportModule;
using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Repository.AmoebaModule.AmoebaReportModule
{
    public interface IAmoebaReportRepository : IRepository<AmoebaReport>
    {
        IEnumerable<string> QueryMonthCityIds(string month, string[] Ids);

        IEnumerable<DateTime> QueryImportDate(string id);
        IEnumerable<string> QueryLastMonth(string id);

        IEnumerable<TAny> QueryDataRanking<TAny>(string cloumnName, string month);

        IEnumerable<AmoebaReport> QuerySimpleInfo(Query query, Pager pager, Sort sort);

        IEnumerable<AmoebaReport> QueryAll(Query query, Pager pager, Sort sort);
    }
}
