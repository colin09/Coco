using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    /// <summary>
    /// 入库明细
    /// </summary>
    public class ShopInStockItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 入库数量
        /// </summary>
        public int InStockNum { get; set; }

        /// <summary>
        /// 单位(瓶)
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Unit { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("ShopPurchaseInStockNote")]
        public string ShopPurchaseInStockNote_Id { get; set; }
        /// <summary>
        /// 采购入库单
        /// </summary>
        public ShopPurchaseInStockNote ShopPurchaseInStockNote { get; set; }

        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// 实际入库明细
        /// </summary>
        public List<ShopActualInStockItem> ActualInStockItems { get; set; }

        #endregion
    }
}
