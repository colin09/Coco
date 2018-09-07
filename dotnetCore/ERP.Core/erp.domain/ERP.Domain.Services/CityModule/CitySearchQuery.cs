using System.Linq;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Contract.CityModule;

namespace ERP.Domain.Services.CityModule {


    public class CitySearchQuery : CitySearch, ISearch {


        public Query CreateQuery () {


            var query = Query.Create ();
            if (OrgTypes != null && OrgTypes.Any ())
                query.Add (Criterion.Create ("OrgType", OrgTypes, CriteriaOperator.In, "Orgs"));
            if (!string.IsNullOrWhiteSpace (CityId))
                query.Add (Criterion.Create ("Id", CityId, CriteriaOperator.Equal, "[Orgs]"));
            if (CityIds != null && CityIds.Any ())
                query.Add (Criterion.Create ("Id", CityIds, CriteriaOperator.In, "[Orgs]"));

            if (!string.IsNullOrWhiteSpace (FilterCityId))
                query.Add (Criterion.Create ("Id", FilterCityId, CriteriaOperator.NotEqual, "[Orgs]"));

            if (!string.IsNullOrWhiteSpace (Name))
                query.Add (Criterion.Create ("Name", Name, CriteriaOperator.Like, "Orgs"));

            if (this.Names != null && this.Names.Any ())
                query.Add (Criterion.Create ("Name", this.Names, CriteriaOperator.In, "Orgs"));

            return query;
        }
    }
}