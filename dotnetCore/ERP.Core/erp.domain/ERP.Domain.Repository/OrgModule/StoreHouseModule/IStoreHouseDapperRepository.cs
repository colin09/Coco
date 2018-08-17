using System;
using System.Collections.Generic;

using ERP.Domain.Model.OrgModule;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Repository.OrgModule.StoreHouseModule
{
    public interface IStoreHouseRepository : IRepository<StoreHouse>
    {

        IEnumerable<StoreHouse> QueryIncludeCity(Query query);
        IEnumerable<StoreHouse> QueryIncludeCityByIds(string[] ids);

        IEnumerable<StoreHouse> GetByIds(string[] ids);
        StoreHouse GetIncludeCity(string id);
    }
}
