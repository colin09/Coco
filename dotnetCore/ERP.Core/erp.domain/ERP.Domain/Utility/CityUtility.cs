using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Utility
{
    public class CityUtility
    {
        /// <summary>
        /// 查询总部城市
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        // public static City GetHQCity(ERPContext context)
        // {
        //     var city = context.Cities.FirstOrDefault(c => c.NewId == "898" || c.Name == "易酒批统采");
        //     return city;
        // }

        /// <summary>
        /// 查询总部代理城市
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        // public static City GetAgentCity(ERPContext context)
        // {
        //     var city = context.Cities.FirstOrDefault(c => c.NewId == "897" || c.Name == "易酒批代理");
        //     return city;
        // }

        public static bool IsAgentCity(City city)
        {
            return city.NewId == "897" || city.Name == "易酒批代理";
        }

        public static bool IsAgentCity(string cityId)
        {
            return cityId == "897";
        }

        /// <summary>
        /// 获取总部代理仓库（代理业务虚拟入库单用）
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        // public static StoreHouse GetAgentStoreHouse(ERPContext context)
        // {
        //     return context.StoreHouses.FirstOrDefault(sh => sh.NewId == "8971");
        // }
    }
}
