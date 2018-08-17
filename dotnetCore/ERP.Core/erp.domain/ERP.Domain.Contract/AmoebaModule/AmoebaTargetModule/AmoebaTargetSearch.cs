using ERP.Common.Infrastructure.QueryObject;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaTargetModule
{
    public class AmoebaTargetSearch //: ISearch
    {

        public string CityId { set; get; }
        public string DataMonth { set; get; }
        public string[] Months { set; get; }



        /*
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
        } */
    }
}
