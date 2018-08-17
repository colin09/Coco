using ERP.Domain.Model.PurchaseInStockModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AuditTraceModule
{
    [Table("PurchaseInStockNoteAuditTrace")]
    public class PurchaseInStockNoteAuditTrace : AuditTrace
    {


        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PurchaseInStockNote")]
        public string PurchaseInStockNote_Id { get; set; }
        /// <summary>
        /// 所属采购单
        /// </summary>
        public PurchaseInStockNote PurchaseInStockNote { get; set; }

        #endregion
    }
}
