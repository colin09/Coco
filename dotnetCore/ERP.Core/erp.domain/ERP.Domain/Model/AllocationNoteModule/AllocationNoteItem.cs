using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseRequisitionModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.AllocationNoteModule
{
    public class AllocationNoteItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 调拨数量（小单位）
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 已入库数量（小单位）
        /// </summary>
        public int InStockCount { get; set; }

        /// <summary>
        /// 已审核入库数量
        /// </summary>
        public int AuditInStockCount { get; set; }

        /// <summary>
        /// 已发货数量（小单位）
        /// </summary>
        public int OutStockCount { get; set; }

        /// <summary>
        /// 小单位调拨价格
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// 大单位调拨价格（用以计算金额）
        /// </summary>
        public decimal SpecificationsPrice { get; set; }

        /// <summary>
        /// 小单位采购价格
        /// </summary>
        public decimal PurchaseUnitPrice { get; set; }
        /// <summary>
        /// 大单位采购价格
        /// </summary>
        public decimal PurchaseSpecificationsPrice { get; set; }

        [MaxLength(64)]
        public string Unit { get; set; }

        [MaxLength(64)]
        public string Specifications { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 总采购金额
        /// </summary>
        public decimal TotalPurchaseAmount { get; set; }
        /// <summary>
        /// 是否赠品
        /// </summary>
        public bool IsGiveaway { get; set; }

        /// <summary>
        /// 单件毛重（前台输入）
        /// </summary>
        public decimal Weight { get; set; }
        /// <summary>
        /// 单件体积（前台输入）
        /// </summary>
        public decimal Volume { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }
        /// <summary>
        /// 关联预售订单号（单项可能关联多个预售订单）
        /// </summary>
        [MaxLength(64)]
        public string RelateOrderId { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("PurchaseRequisitionItem")]
        public string PurchaseRequisitionItem_Id { get; set; }
        /// <summary>
        /// 关联的采购申请单项（大区联采 有该选项）
        /// </summary>
        public PurchaseRequisitionItem PurchaseRequisitionItem { get; set; }
        [MaxLength(64)]
        [ForeignKey("AllocationNote")]
        public string AllocationNote_Id { get; set; }
        public AllocationNote AllocationNote { get; set; }
        #endregion

        #region 业务方法

        public virtual void UpdateAmount()
        {
            if (this.Product == null || this.Product.Info == null)
            {
                throw new BusinessException("信息加载不全，无法计算金额！");
            }

            this.TotalAmount = this.Count * this.SpecificationsPrice / this.Product.Info.Desc.PackageQuantity;
            this.TotalPurchaseAmount = this.Count * this.PurchaseSpecificationsPrice / this.Product.Info.Desc.PackageQuantity;
        }

        #endregion
    }
}
