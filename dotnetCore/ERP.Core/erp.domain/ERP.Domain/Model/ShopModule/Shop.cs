using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    /// <summary>
    /// 经销商（入驻商）
    /// </summary>
    public class Shop : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }
        /// <summary>
        /// 商铺名称
        /// </summary>
        [MaxLength(64)]
        public string ShopName { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        ///  状态
        /// </summary>
        public EnableState State { get; set; }
        /// <summary>
        /// 负责人姓名
        /// </summary>
        [MaxLength(32)]
        public string LeadUserName { get; set; }
        /// <summary>
        /// 负责人手机号码
        /// </summary>
       [MaxLength(32)]
        public string LeadUserMobileNo { get; set; }

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }

        #endregion

        #region 聚合属性

        public City City { get; set; }

        #endregion
    }
}
