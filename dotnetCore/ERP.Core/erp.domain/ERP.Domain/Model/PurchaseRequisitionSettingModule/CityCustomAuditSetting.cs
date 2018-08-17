using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionSettingModule
{
    public class CityCustomAuditSetting : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 城市经理审核设置
        /// </summary>
        [MaxLength(255)]
        public string CityManagerMobileNos { get; set; }

        /// <summary>
        /// 一级审核用户联系方式（多个用逗号分割）
        /// </summary>
        [MaxLength(255)]
        public string FirstAuditUserMobileNos { get; set; }

        /// <summary>
        /// 二级审核用户联系方式（多个用逗号分割）
        /// </summary>
        [MaxLength(255)]
        public string SecondAuditUserMobileNos { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        /// <summary>
        /// 审核规则名称（默认PurchaseRequisition）
        /// </summary>
        [MaxLength(64)]
        public string AuditRuleName { get; set; }

        #region 聚合属性

        [ForeignKey("GlobalAuditSetting")]
        [MaxLength(64)]
        public string GlobalAuditSetting_Id { get; set; }
        public PurchaseRequisitionGlobalAuditSetting GlobalAuditSetting { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion
    }
}
