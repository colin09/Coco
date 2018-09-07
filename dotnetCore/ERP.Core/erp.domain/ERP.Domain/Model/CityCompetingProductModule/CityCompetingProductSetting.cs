using ERP.Domain.Enums.CityCompetingProductModule;
using ERP.Domain.Model.OrgModule;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CityCompetingProductModule
{
    /// <summary>
    /// 城市竞品设置
    /// </summary>
    public class CityCompetingProductSetting : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 设置状态
        /// </summary>
        public CityCompetingProductSettingState State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [NotMapped]
        public string[] CompetingProductTypeContainer
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.CompetingProductTypes))
                    return new string[0];
                return JsonConvert.DeserializeObject<string[]>(this.CompetingProductTypes);
            }
            set
            {
                if (value == null)
                    CompetingProductTypes = null;
                else
                    CompetingProductTypes = JsonConvert.SerializeObject(value);
            }
        }

        /// <summary>
        /// 竞品类型（由CompetingProductTypeContainer设置）
        /// </summary>
        public string CompetingProductTypes { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }

        public List<CityCompetingProduct> CityCompetingProducts { get; set; }

        #endregion
    }
}
