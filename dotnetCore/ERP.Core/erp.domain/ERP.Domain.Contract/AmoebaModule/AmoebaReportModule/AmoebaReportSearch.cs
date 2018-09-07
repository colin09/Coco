using ERP.Common.Infrastructure.QueryObject;
using ERP.Domain.Model.OrgModule;

namespace ERP.Domain.Contract.AmoebaModule.AmoebaReportModule
{
    public class AmoebaReportSearch //: ISearch
    {
        public string Id { set; get; }
        public string CityId { set; get; }
        public string CityName { set; get; }
        public string DataMonth { set; get; }
        //public OrgType OrgType { set; get; }


        public string[] Citys { set; get; }
        public string[] Months { set; get; }

        /*
        public Query CreateQuery()
        {
            var query = Query.Create();

            if (!string.IsNullOrWhiteSpace(Id))
                query.Add(Criterion.Create("Id", Id, CriteriaOperator.Equal, "AmoebaReport"));

            if (!string.IsNullOrWhiteSpace(CityId))
                query.Add(Criterion.Create("CityId", CityId, CriteriaOperator.Equal, "AmoebaReport"));

            if (!string.IsNullOrWhiteSpace(CityName))
                query.Add(Criterion.Create("Name", CityName, CriteriaOperator.Equal, "Orgs"));

            if (!string.IsNullOrWhiteSpace(DataMonth))
                query.Add(Criterion.Create("DataMonth", DataMonth, CriteriaOperator.Equal, "AmoebaReport"));


            if (Citys != null && Citys.Length > 0)
                query.Add(Criterion.Create("CityId", Citys, CriteriaOperator.In, "AmoebaReport"));
            if (Months != null && Months.Length > 0)
                query.Add(Criterion.Create("DataMonth", Months, CriteriaOperator.In, "AmoebaReport"));


            return query;
        }
        */
    }
}
