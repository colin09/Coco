using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Contract.AmoebaModule.AmoebaImprovementModule;

namespace ERP.Domain.Services.AmoebaModule.AmoebaImprovementModule
{
    public class AmoebaImprovementSearchQuery:AmoebaImprovementSearch,ISearch
    {
       public Query CreateQuery()
        {
            var query = Query.Create();

            if (!string.IsNullOrWhiteSpace(CityId))
                query.Add(Criterion.Create("CityId", CityId,CriteriaOperator.Equal,"AmoebaImprovement"));

            if (!string.IsNullOrWhiteSpace(DataMonth))
                query.Add(Criterion.Create("DataMonth", DataMonth, CriteriaOperator.Equal, "AmoebaImprovement"));

            return query;
        }
    }
}