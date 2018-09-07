

namespace ERP.Domain.Contract.AmoebaModule.AmoebaImprovementModule
{
    public class AmoebaImprovementSearch //: ISearch
    {
        public string CityId { set; get; }

        public string DataMonth { set; get; }
        /*
        public Query CreateQuery()
        {
            var query = Query.Create();

            if (!string.IsNullOrWhiteSpace(CityId))
                query.Add(Criterion.Create("CityId", CityId,CriteriaOperator.Equal,"AmoebaImprovement"));

            if (!string.IsNullOrWhiteSpace(DataMonth))
                query.Add(Criterion.Create("DataMonth", DataMonth, CriteriaOperator.Equal, "AmoebaImprovement"));

            return query;
        } */
    }
}
