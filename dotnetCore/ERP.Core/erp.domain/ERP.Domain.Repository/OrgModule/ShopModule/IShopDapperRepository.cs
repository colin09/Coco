using System;
using System.Collections.Generic;

using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ShopModule;
using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Repository.OrgModule.ShopModule
{
    public interface IShopRepository : IRepository<Shop>
    {
        Shop GetByMobileNo(string mobileNo);
    }
}
