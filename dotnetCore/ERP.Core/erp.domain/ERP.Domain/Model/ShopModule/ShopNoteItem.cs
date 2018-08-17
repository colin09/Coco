using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    public class ShopNoteItem : BaseEntity
    {
        /// <summary>
        /// 单位
        /// </summary>
        [MaxLength(32)]
        public string Unit { get; set; }

        /// <summary>
        /// 货位
        /// </summary>
        [MaxLength(64)]
        public string GoodsPosition { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Num { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 折扣额
        /// </summary>
        public decimal DiscountAmount { get; set; }

        /// <summary>
        /// 补充利润（只用来计算折前净利）
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 是否赠品
        /// </summary>
        public bool IsGiveaway { get; set; }

        [MaxLength(200)]
        public string Remark { get; set; }

        #region 聚合属性
        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        [ForeignKey("ShopOrderOutStockNote")]
        [MaxLength(64)]
        public string ShopOrderOutStockNote_Id { get; set; }
        public ShopOrderOutStockNote ShopOrderOutStockNote { get; set; }
        [ForeignKey("ShopPickupNoteItem")]
        [MaxLength(64)]
        public string ShopPickupNoteItem_Id { get; set; }
        public ShopPickupNoteItem ShopPickupNoteItem { get; set; }

        #endregion
    }
}
