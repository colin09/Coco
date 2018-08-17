using ERP.Domain.Enums.PurchaseRequisitionSettingModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionSettingModule
{
    /// <summary>
    /// 审核配置项
    /// </summary>
    public class AuditSettingItem : BaseEntity
    {
        /// <summary>
        /// 配置Key， 特定类目Key 为类目名称；特定品牌Key为品牌名称
        /// </summary>
        [MaxLength(64)]
        public string Key { get; set; }

        [MaxLength(64)]
        [ForeignKey("AuditSetting")]
        public string AuditSetting_Id { get; set; }
        public GlobalAuditSettingType SettingType { get; set; }

        public PurchaseRequisitionGlobalAuditSetting AuditSetting { get; set; }
    }
}
