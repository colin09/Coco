using ERP.Domain.Enums.PurchaseRequisitionSettingModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionSettingModule
{
    public class PurchaseRequisitionGlobalAuditSetting : BaseEntity, IAggregationRoot
    {
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 配置类型
        /// </summary>
        public GlobalAuditSettingType SettingType { get; set; }

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

        /// <summary>
        /// 只有 特定类目、特定品牌 有值
        /// </summary>
        public List<AuditSettingItem> AuditSettingItems { get; set; }

        /// <summary>
        /// 只有特定产品 有值
        /// </summary>
        public List<ProductInfoAuditSettingItem> ProductInfoAuditSettingItems { get; set; }
        /// <summary>
        /// 城市自定义配置
        /// </summary>
        public List<CityCustomAuditSetting> CityCustomAuditSettings { get; set; }

        #endregion
    }
}
