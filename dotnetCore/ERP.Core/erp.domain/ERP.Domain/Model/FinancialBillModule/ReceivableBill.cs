using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.StockBillModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.StockBillModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 应收单据
    /// </summary>
    public class ReceivableBill : BaseEntity, IAggregationRoot, IDeleted, ICannotEditAndDelete
    {
        public ReceivableBill()
        {
            this.BusinessTime = DateTime.Now;
            this.OverdueTime = DateTime.Now;
            this.HasReceipt = false;

        }
        #region 基本属性

        public ReceivableBillType ReceivableBillType { get; set; }

        public SaleBusinessType SaleBusinessType { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        [Display(Name = "价税合计")]
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        [Display(Name = "实收金额")]
        public decimal PaidInAmount { get; set; }

        /// <summary>
        /// 现金收款金额
        /// </summary>
        public decimal CashAmount { get; set; }

        /// <summary>
        /// 银行收款金额
        /// </summary>
        public decimal BankAmount { get; set; }

        /// <summary>
        /// 微信收款金额
        /// </summary>
        public decimal WeiXinAmount { get; set; }
        /// <summary>
        /// 支付宝收款金额
        /// </summary>
        public decimal AlipayAmount { get; set; }

        /// <summary>
        /// 现金标记金额
        /// </summary>
        public decimal CashMarkAmount { get; set; }

        /// <summary>
        /// 银行付款标记金额
        /// </summary>
        public decimal BankMarkAmount { get; set; }

        /// <summary>
        /// 微信标记金额
        /// </summary>
        public decimal WeiXinMarkAmount { get; set; }
        /// <summary>
        /// 支付宝标记金额
        /// </summary>
        public decimal AlipayMarkAmount { get; set; }

        /// <summary>
        /// 是否处理标记数据
        /// </summary>
        [NotMapped]
        public bool HandleMarkData
        {
            get
            {
                return !(this.WeiXinAmount > this.WeiXinMarkAmount
                    || this.CashAmount > this.CashAmount
                    || this.BankAmount > this.BankMarkAmount
                    || this.AlipayAmount > this.AlipayMarkAmount);
            }
        }
        [NotMapped]
        public decimal TotalMarkAmount
        {
            get
            {
                return this.AlipayMarkAmount + this.BankMarkAmount + this.CashMarkAmount + this.WeiXinMarkAmount;
            }
        }
        [NotMapped]
        public decimal TotalReceiptAmount
        {
            get
            {
                return this.AlipayAmount + this.BankAmount + this.CashAmount + this.WeiXinAmount;
            }
        }

        /// <summary>
        /// 订单编号
        /// </summary>
        [MaxLength(64)]
        [Display(Name = "订单编号")]
        public string OrderNo { get; set; }

        /// <summary>
        /// 关联的 销售出库单或退货入库单NoteNo
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        [Display(Name = "审核状态")]
        public AuditState AuditState { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessTime { get; set; }
        /// <summary>
        /// 到期日
        /// </summary>
        public DateTime OverdueTime { get; set; }

        /// <summary>
        /// 是否已收款
        /// </summary>
        public bool HasReceipt { get; set; }

        /// <summary>
        /// 已收款金额
        /// </summary>
        public decimal HasReceiptAmount { get; set; }

        public bool CannotEditAndDelete { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public bool IsInternal { get; set; }
        #endregion

        #region 聚合属性

        /// <summary>
        /// 应收单项
        /// </summary>
        public List<ReceivableBillItem> Items { get; set; }
        [ForeignKey("CompanyUser")]
        [MaxLength(64)]
        public string CompanyUser_Id { get; set; }
        /// <summary>
        /// 应收客户
        /// </summary>
        public User CompanyUser { get; set; }

        [ForeignKey("CompanyUserCity")]
        [MaxLength(64)]
        public string CompanyUserCity_Id { get; set; }
        /// <summary>
        /// 只有内部往来的，才有值
        /// </summary>
        public Org CompanyUserCity { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [ForeignKey("AllocationNote")]
        [MaxLength(64)]
        public string AllocationNote_Id { get; set; }
        /// <summary>
        /// 调拨单
        /// </summary>
        public AllocationNote AllocationNote { get; set; }
        [ForeignKey("OrderOutStockNote")]
        [MaxLength(64)]
        public string OrderOutStockNote_Id { get; set; }
        public OrderOutStockNote OrderOutStockNote { get; set; }
        [ForeignKey("ReturnOrderInStockNote")]
        [MaxLength(64)]
        public string ReturnOrderInStockNote_Id { get; set; }
        public ReturnOrderInStockNote ReturnOrderInStockNote { get; set; }

        #endregion

        #region 方法

        public virtual void Audit(AuditState auditState)
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("单据已审核");

            this.AuditState = auditState;
        }

        public void CheckEdit()
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("已审核单据不可编辑！");

            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可编辑！");
        }

        // public virtual void UnAudit(ERPContext context)
        // {
        //     if (this.AuditState != AuditState.已审核)
        //         throw new BusinessException("当前状态不能反审核");

        //     if (context.WriteOffReceivableBillItems.Any(i => i.ReceivableBill.Id == this.Id && !i.PreReceiptWriteOffBill.IsDeleted))
        //         throw new BusinessException("已存在预收应收核销单据，不能反审核！");

        //     if (context.ReceiptBills.WhereNotDeleted().Any(b => b.ReceivableBillId == this.Id))
        //         throw new BusinessException("已存在收款单,不能反审核");

        //     this.AuditState = AuditState.待审核;
        // }
        public void CheckUnAudit()
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            if (this.HasReceipt || this.HasReceiptAmount != decimal.Zero)
                throw new BusinessException("已存在收款单,不能反审核");
        }
        // public virtual void Delete(ERPContext context)
        // {
        //     if (this.IsDeleted)
        //         return;
        //     if (this.AuditState == AuditState.已审核)
        //         throw new BusinessException("请先反审核");

        //     if (this.CannotEditAndDelete)
        //     {
        //         throw new BusinessException("该单据不可删除！");
        //     }

        //     if (context.ReceiptBills.WhereNotDeleted().Any(b => b.ReceivableBillId == this.Id))
        //         throw new BusinessException("已存在收款单，不能删除");

        //     this.IsDeleted = true;

        //     if (this.OrderOutStockNote != null)
        //         this.OrderOutStockNote.IsDownReason = false;
        //     if (this.ReturnOrderInStockNote != null)
        //         this.ReturnOrderInStockNote.IsDownReason = false;
        // }

        #endregion


        public void CheckDelete()
        {
            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");

            if (this.CannotEditAndDelete)
                throw new BusinessException("该单据不可删除！");

            if (this.HasReceipt || this.HasReceiptAmount != 0)
                throw new BusinessException("已存在收款单，不能删除");
        }
    }
}
