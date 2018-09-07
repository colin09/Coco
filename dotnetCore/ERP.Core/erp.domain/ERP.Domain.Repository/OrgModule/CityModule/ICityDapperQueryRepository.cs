using System;
using System.Collections.Generic;
using System.Linq;

using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model.OrgModule;

namespace ERP.Domain.Repository.OrgModule.CityModule
{
    public interface ICityQueryRepository : ICityRepository
    {

        IEnumerable<City> SearchOnLine(Query query, Sort sort = null);

        City GetById(string id);
    }
}
