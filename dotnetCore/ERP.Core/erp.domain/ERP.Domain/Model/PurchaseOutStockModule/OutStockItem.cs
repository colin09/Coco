using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseOutStockModule
{
    /// <summary>
    /// 出库明细
    /// </summary>
    public class OutStockItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Num { get; set; }

        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 单位(瓶)
        /// </summary>
        [Required]
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否是采购赠品
        /// </summary>
        public bool IsGiveaway { get; set; } 

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Note")]
        public string Note_Id { get; set; }
        /// <summary>
        /// 采购入库单
        /// </summary>
        public PurchaseOutStockNote Note { get; set; }
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        #endregion
    }
}
