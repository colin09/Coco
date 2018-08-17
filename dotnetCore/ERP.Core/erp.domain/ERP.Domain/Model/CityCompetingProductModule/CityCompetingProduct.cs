using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CityCompetingProductModule
{
    /// <summary>
    /// 城市竞品sku
    /// </summary>
    public class CityCompetingProduct : BaseEntity
    {
        /// <summary>
        /// 城市竞品设置
        /// </summary>
        public CityCompetingProductSetting CityCompetingProductSetting { get; set; }

        /// <summary>
        /// 关联产品
        /// </summary>
        public Product Product { get; set; }
    }
}
