using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ERP.Domain.Model.OrgModule;

namespace ERP.Domain.Model.SettingModule
{
    public class PurchaseRequisitionAuditMobileSetting : BaseEntity, IAggregationRoot
    {
        #region 基本属性
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
        /// 作用人手机号码（冗余字段）
        /// </summary>
        [MaxLength(255)]
        public string UserMobile { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(255)]
        public string Remark { get; set; }

        [MaxLength(64)]
        [ForeignKey("User")]
        public string User_Id { get; set; }
        #endregion

        #region 聚合属性
        /// <summary>
        /// 作用人
        /// </summary>
        public OrgUser User { get; set; }
        #endregion

    }
}
