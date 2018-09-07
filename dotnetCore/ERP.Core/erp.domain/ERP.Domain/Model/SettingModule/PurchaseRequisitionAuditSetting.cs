using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Enums.SettingModule;

namespace ERP.Domain.Model.SettingModule
{
    /// <summary>
    /// 采购申请审核设置
    /// </summary>
    public class PurchaseRequisitionAuditSetting : BaseEntity, IAggregationRoot
    {

        #region 基本属性

        /// <summary>
        /// 设置类型
        /// </summary>
        public PurchaseRequisitionAuditSettingType SettingType { get; set; }

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

        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        
        [NotMapped]
        [MaxLength(64)]
        public string City_Name { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 设置城市
        /// </summary>
        public City City { get; set; }
        #endregion

    }
}
