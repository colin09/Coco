using System;
using System.Collections.Generic;

using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Repository.OrgModule.ProviderModule
{
    public interface IProviderRepository : IRepository<Provider>
    {
        IEnumerable<Provider> GetByIds(string[] ids);
    }
}
