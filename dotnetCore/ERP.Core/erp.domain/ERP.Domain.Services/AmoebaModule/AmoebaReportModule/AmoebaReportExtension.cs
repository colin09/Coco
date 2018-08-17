using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Contract.AmoebaModule.AmoebaReportModule;

namespace ERP.Domain.Services.AmoebaModule.AmoebaReportModule
{
    public static class AmoebaReportExtension
    {
        public static Query CreateQuery(this AmoebaReportSearch search)
        {
            var query = Query.Create();

            if (!string.IsNullOrWhiteSpace(search.Id))
                query.Add(Criterion.Create("Id", search.Id, CriteriaOperator.Equal, "AmoebaReport"));

            if (!string.IsNullOrWhiteSpace(search.CityId))
                query.Add(Criterion.Create("CityId", search.CityId, CriteriaOperator.Equal, "AmoebaReport"));

            if (!string.IsNullOrWhiteSpace(search.CityName))
                query.Add(Criterion.Create("Name", search.CityName, CriteriaOperator.Equal, "Orgs"));

            if (!string.IsNullOrWhiteSpace(search.DataMonth))
                query.Add(Criterion.Create("DataMonth", search.DataMonth, CriteriaOperator.Equal, "AmoebaReport"));


            if (search.Citys != null && search.Citys.Length > 0)
                query.Add(Criterion.Create("CityId", search.Citys, CriteriaOperator.In, "AmoebaReport"));
            if (search.Months != null && search.Months.Length > 0)
                query.Add(Criterion.Create("DataMonth", search.Months, CriteriaOperator.In, "AmoebaReport"));


            return query;
        }
    }
}