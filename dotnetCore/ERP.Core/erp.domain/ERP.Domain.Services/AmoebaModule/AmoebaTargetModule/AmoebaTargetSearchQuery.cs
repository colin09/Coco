using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Contract.AmoebaModule.AmoebaTargetModule;

namespace ERP.Domain.Services.AmoebaModule.AmoebaTargetModule
{
    public class AmoebaTargetSearchQuery:AmoebaTargetSearch,ISearch
    {
       public Query CreateQuery()
        {
            var query = Query.Create();

            if (!string.IsNullOrWhiteSpace(CityId))
                query.Add(Criterion.Create("CityId", CityId, CriteriaOperator.Equal, "AmoebaTarget"));

            if (!string.IsNullOrWhiteSpace(DataMonth))
                query.Add(Criterion.Create("DataMonth", DataMonth, CriteriaOperator.Equal, "AmoebaTarget"));

            if (Months != null)
                query.Add(Criterion.Create("DataMonth", Months, CriteriaOperator.In, "AmoebaTarget"));

            return query;
        }
    }
}