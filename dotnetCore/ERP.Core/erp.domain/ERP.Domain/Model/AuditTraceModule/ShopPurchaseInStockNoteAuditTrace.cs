using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.ShopModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AuditTraceModule
{
    [Table("ShopPurchaseInStockNoteAuditTrace")]
    public class ShopPurchaseInStockNoteAuditTrace : AuditTrace
    {
        #region 聚合属性

        /// <summary>
        /// 所属采购单
        /// </summary>
        public ShopPurchaseInStockNote ShopPurchaseInStockNote { get; set; }

        #endregion
    }
}
