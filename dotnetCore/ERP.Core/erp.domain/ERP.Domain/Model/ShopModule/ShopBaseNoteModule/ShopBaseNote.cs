using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.ProductStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.ShopModule.ShopBaseNoteModule
{
    [Table("ShopBaseNotes")]
    public class ShopBaseNote : BaseEntity, IDeleted, ICannotEditAndDelete
    {
        public ShopBaseNote()
        {
            this.Unit = "瓶";
            this.AuditState = NoteAuditState.待审核;
        }

        #region 基本属性
        [MaxLength(64)]
        public string NoteNo { get; set; }
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public NoteType NoteType { get; set; }

        /// <summary>
        /// 单据操作类型
        /// </summary>
        public NoteOperationType OperationType { get; set; }

        /// <summary>
        /// 价格
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        [Required]
        public int Num { get; set; }

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
        /// 审核状态
        /// </summary>
        public NoteAuditState AuditState { get; set; }

        /// <summary>
        /// 审核备注
        /// </summary>
        [MaxLength(200)]
        public string AuditRemark { get; set; }

        /// <summary>
        /// 是否已下推生成单据
        /// </summary>
        public bool IsDownReason { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// 是否不可编辑删除
        /// </summary>
        public bool CannotEditAndDelete { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }


        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary> 
        public StoreHouse StoreHouse { get; set; }

        [MaxLength(64)]
        [ForeignKey("BaseNoteInfo")]
        public string BaseNoteInfo_Id { get; set; }
        public BaseNoteInfo BaseNoteInfo { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 反审核
        /// </summary>
        /// <param name="context"></param>
        // public virtual void UnAudit(ERPContext context)
        // {
        //     if (this.AuditState != NoteAuditState.审核通过 && this.AuditState != NoteAuditState.已核算)
        //         throw new BusinessException("当前状态不能反审核");

        //     //处理库存 以及成本
        //     var productStock = context.ProductStocks.Include("StoreHouse").Include("Product.Info").FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
        //     if (productStock != null)
        //     {
        //         if (this.NoteType == NoteType.出库单)
        //             productStock.AddStock(this.Num, this.Price);
        //         else
        //             productStock.ReduceStock(this.Num, this.Price);
        //     }

        //     this.AuditState = NoteAuditState.待审核;
        //     this.AuditRemark = "";
        // }

        // public virtual void AuditAndAccounting(ERPContext context, NoteAuditState auditState, string auditRemark)
        // {
        //     if (this.AuditState != NoteAuditState.待审核 && this.AuditState != NoteAuditState.审核拒绝)
        //         throw new BusinessException("单据已审核");

        //     this.AuditState = auditState;
        //     this.AuditRemark = auditRemark;

        //     if (auditState == NoteAuditState.审核通过)
        //     {
        //         var productStock = HandleProductStock(context);

        //         this.AuditState = NoteAuditState.已核算;
        //     }
        // }

        // private ProductStock HandleProductStock(ERPContext context)
        // {
        //     var productStock = context.ProductStocks.Include("StoreHouse.City").Include("Product.Info")
        //     .FirstOrDefault(s => s.Product.Id == this.Product.Id && s.StoreHouse.Id == this.StoreHouse.Id);
        //     if (productStock == null)
        //     {
        //         if (this.NoteType == NoteType.出库单)
        //             throw new BusinessException(string.Format("仓库：{0} 产品：{1} 库存不足;", this.StoreHouse.Name, this.Product.Info.Desc.ProductName));
        //         else
        //         {
        //             productStock = new ProductStock()
        //             {
        //                 City_Id = this.StoreHouse.City.Id,
        //                 City = this.StoreHouse.City,
        //                 StoreHouse = this.StoreHouse,
        //                 Product = this.Product,
        //                 StockUnit = this.Unit,
        //                 PriceUnit = this.Unit,
        //                 CostPrice = this.Price,
        //                 StockNum = 0,
        //             };
        //             context.ProductStocks.Add(productStock);
        //         }
        //     }
        //     else
        //     {
        //         if (this.NoteType == NoteType.出库单)
        //             productStock.ReduceStock(this.Num, this.Price);
        //         else
        //             productStock.AddStock(this.Num, this.Price);
        //     }
        //     return productStock;
        // }

        public virtual void Delete()
        {
            if (this.IsDeleted) return;
            if (this.AuditState == NoteAuditState.审核通过 || this.AuditState == NoteAuditState.已核算)
                throw new BusinessException("请先反审核！");

            this.IsDeleted = true;
        }

        #endregion
    }
}
