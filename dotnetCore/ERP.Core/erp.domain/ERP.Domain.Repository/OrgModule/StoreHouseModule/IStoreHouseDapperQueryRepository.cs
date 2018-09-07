using System;
using System.Collections.Generic;

using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;


namespace ERP.Domain.Repository.OrgModule.StoreHouseModule
{
    public interface IStoreHouseQueryRepository : IStoreHouseRepository
    {
        bool Exists(string id, EnableState enable);
    }
}
