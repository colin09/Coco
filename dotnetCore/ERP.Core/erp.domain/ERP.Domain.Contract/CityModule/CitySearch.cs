using System;
using System.Collections.Generic;
using System.Linq;

using ERP.Domain.Enums.SettingModule;
using ERP.Domain.Model.OrgModule;

namespace ERP.Domain.Contract.CityModule
{
    public class CitySearch //: ISearch
    {
        /// <summary>
        /// 包含的城市Id集合
        /// </summary>
        public List<string> CityIds { get; set; }

        public string CityId { get; set; }
        /// <summary>
        /// 城市名称
        /// </summary>
        public string Name { get; set; }
        public string[] Names { get; set; }

        public List<OrgType> OrgTypes { get; set; }
        /// <summary>
        /// 不包含城市Id 
        /// </summary>
        public string FilterCityId { get; set; }

        /* 
        public Query CreateQuery()
        {
            var query = Query.Create();
            if (OrgTypes != null && OrgTypes.Any())
                query.Add(Criterion.Create("OrgType", OrgTypes, CriteriaOperator.In, "Orgs"));
            if (!string.IsNullOrWhiteSpace(CityId))
                query.Add(Criterion.Create("Id", CityId, CriteriaOperator.Equal, "[Orgs]"));
            if (CityIds != null && CityIds.Any())
                query.Add(Criterion.Create("Id", CityIds, CriteriaOperator.In, "[Orgs]"));

            if (!string.IsNullOrWhiteSpace(FilterCityId))
                query.Add(Criterion.Create("Id", FilterCityId, CriteriaOperator.NotEqual, "[Orgs]"));

            if (!string.IsNullOrWhiteSpace(Name))
                query.Add(Criterion.Create("Name", Name, CriteriaOperator.Like, "Orgs"));

            if (this.Names != null && this.Names.Any())
                query.Add(Criterion.Create("Name", this.Names, CriteriaOperator.In, "Orgs"));

            return query;
        }*/

    }
}
