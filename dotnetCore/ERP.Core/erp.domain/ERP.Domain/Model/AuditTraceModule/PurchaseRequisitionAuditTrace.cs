using ERP.Domain.Model.PurchaseRequisitionModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AuditTraceModule
{

    [Table("PurchaseRequisitionAuditTrace")]
    public class PurchaseRequisitionAuditTrace : AuditTrace
    {
        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PurchaseRequisition")]
        public string PurchaseRequisition_Id { get; set; }
        /// <summary>
        /// 所属采购申请单
        /// </summary>
        public PurchaseRequisition PurchaseRequisition { get; set; }

        #endregion
    }
}
