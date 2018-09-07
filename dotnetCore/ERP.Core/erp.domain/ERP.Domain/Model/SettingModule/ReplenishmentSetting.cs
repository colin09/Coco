using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.SettingModule
{
    public class ReplenishmentSetting : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 起调数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 补货设置状态
        /// </summary>
        public ReplenishmentSettingState ReplenishmentSettingState { get; set; }

        #region 聚合属性

        [ForeignKey("ProductInfo")]
        [MaxLength(64)]
        public string ProductInfo_Id { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public ProductInfo ProductInfo { get; set; }


        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        #endregion
    }
}
