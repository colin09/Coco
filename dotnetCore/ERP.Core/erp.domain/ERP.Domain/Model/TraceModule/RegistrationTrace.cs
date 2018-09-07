using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Domain.Model.OrgModule;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ERP.Domain.Enums.TraceModule;

namespace ERP.Domain.Model.TraceModule
{
    /// <summary>
    /// 签到拜访
    /// </summary>
    public class RegistrationTrace : BaseEntity, IAggregationRoot
    {
        #region 基本属性
        /// <summary>
        /// 拜访类型
        /// </summary>
        public RegistrationType RegistrationType { get; set; }
        /// <summary>
        /// 拜访时间
        /// </summary>
        public DateTime BusinessTime { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [MaxLength(255)]
        public string Address { get; set; }
        /// <summary>
        /// 供应商 or 市场名称
        /// </summary>
        [MaxLength(64)]
        public string ContactName { get; set; }

        /// <summary>
        /// 是否新供应商
        /// </summary>
        public bool IsNewProvider { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        [MaxLength(32)]
        public string ContactUserName { get; set; }
        /// <summary>
        /// 联系人电话
        /// </summary>
        [MaxLength(32)]
        public string ContactMobileNo { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 采购用户Id
        /// </summary>
        [MaxLength(64)]
        public string PurchaseUserId { get; set; }

        /// <summary>
        /// 采购人员
        /// </summary>
        [MaxLength(32)]
        public string PurchaseName { get; set; }

        /// <summary>
        /// 采购联系方式
        /// </summary>
        [MaxLength(32)]
        public string PurchaseMobileNo { get; set; }

        #endregion

        #region 聚合属性

        [ForeignKey("Provider")]
        [MaxLength(64)]
        public string Provider_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        #endregion
    }
}
