using System;
using System.Collections.Generic;
using System.Linq;

using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Repository.OrgModule.CityModule;

using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Repository.OrgModule.CityModule
{
    public interface ICityRepository : IRepository<City>
    {
        
        IEnumerable<City> GetByIds(string[] ids);
        City GetHQCity();
        City GetAgentCity();
    }
}
