using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.AllocationNoteModule;
using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.FinancialBillModule;
using ERP.Domain.Enums.PurchaseInStockModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.PurchaseInStockModule;
using ERP.Domain.Model.PurchaseOutStockModule;
using ERP.Domain.Model.StockBillModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.AccessControl;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 应付单
    /// </summary>
    public class PayableBill : BaseEntity, IAggregationRoot, IDeleted
    {
        public PayableBill()
        {
            this.IsDeleted = false;
        }

        #region 基本属性

        public PayableBillType PayableBillType { get; set; }

        /// <summary>
        /// 采购业务类型
        /// </summary>
        public PurchaseBusinessType PurchaseBusinessType { get; set; }

        /// <summary>
        /// 关联的采购入库单或采购退货单编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }
        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 价税合计
        /// </summary>
        [Display(Name = "价税合计")]
        public decimal AdValoremAmount { get; set; }

        /// <summary>
        /// 币种
        /// </summary>
        [Display(Name = "币种")]
        public MoneyType MoneyType { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public AuditState AuditState { get; set; }

        /// <summary>
        /// 到期时间
        /// </summary>
        [Display(Name = "业务日期")]
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 是否已付款
        /// </summary>
        public bool IsPayment { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal HasPaymentAmount { get; set; }

        /// <summary>
        /// 是否已下推生成单据
        /// </summary>
        public bool IsDownReason { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        public bool IsInternal { get; set; }

        #endregion

        #region 聚合属性
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
        [ForeignKey("PurchaseInStockNote")]
        [MaxLength(64)]
        public string PurchaseInStockNote_Id { get; set; }
        /// <summary>
        /// 采购入库单
        /// </summary>
        public PurchaseInStockNote PurchaseInStockNote { get; set; }
        [ForeignKey("PurchaseOutStockNote")]
        [MaxLength(64)]
        public string PurchaseOutStockNote_Id { get; set; }
        /// <summary>
        /// 采购退货单
        /// </summary>
        public PurchaseOutStockNote PurchaseOutStockNote { get; set; }

        [ForeignKey("Supplier")]
        [MaxLength(64)]
        public string Supplier_Id { get; set; }
        /// <summary>
        /// 收款供应商
        /// </summary>
        public Provider Supplier { get; set; }

        [ForeignKey("SupplierCity")]
        [MaxLength(64)]
        public string SupplierCity_Id { get; set; }

        public Org SupplierCity { get; set; }

        /// <summary>
        /// 应付单明细
        /// </summary>
        public List<PayableBillItem> Items { get; set; }

        #endregion

        #region 业务方法

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
            if (!string.IsNullOrWhiteSpace(this.PurchaseInStockNote_Id) || !string.IsNullOrWhiteSpace(this.PurchaseOutStockNote_Id))
                throw new BusinessException("入库单或退货单下推的应付单不可编辑");
        }

        public void CheckUnAudit()
        {
            if (this.AuditState != AuditState.已审核)
                throw new BusinessException("当前状态不能反审核");

            if (this.IsDownReason)
                throw new BusinessException("请先删除付款单");
        }

        public virtual void CheckDelete()
        {
            if (this.IsDeleted)
                return;

            if (this.AuditState == AuditState.已审核)
                throw new BusinessException("请先反审核");

            if (this.IsDownReason)
                throw new BusinessException("请先删除付款单");
        }
        /*
        /// <summary>
        /// 更新调拨结算单结算金额
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public virtual void UpdateAllocationSettlementNoteSettlementAmount(ERPContext context, decimal amount)
        {
            var settlementNote = this.GetSettlementNote(context);
            if (settlementNote != null)
            {
                settlementNote.SettlementAmount += amount;
                settlementNote.UpdateState(context, this);
            }
        }
        /// <summary>
        /// 更新调拨单结算单 核销金额（预付金额）
        /// </summary>
        /// <param name="context"></param>
        /// <param name="amount"></param>
        public virtual void UpdateAllocationSettlementNoteWriteOffAmount(ERPContext context, decimal amount)
        {
            var settlementNote = this.GetSettlementNote(context);
            if (settlementNote != null)
            {
                settlementNote.PrepayAmount += amount;
                settlementNote.UpdateState(context, this);
            }
        }

        /// <summary>
        /// 获取应付单对应的结算单
        /// </summary>
        /// <returns></returns>
        private AllocationSettlementNote GetSettlementNote(ERPContext context)
        {
            if (this.PurchaseBusinessType == PurchaseBusinessType.城市采购 ||
                this.PurchaseBusinessType == PurchaseBusinessType.代理采购)
                return null;
            if (this.AllocationNote == null || this.AllocationNote.ToCity == null)
                throw new BusinessException("调拨单信息未加载，无法获取结算单信息");
            switch (this.AllocationNote.Type)
            {
                case AllocationType.城市到城市:
                    return GetCityToCitySettlementNote(context);
                case AllocationType.供应商到城市:
                    return GetProviderToCitySettlementNote(context);
                case AllocationType.供应商到总部:
                    return GetProviderToHQSettlementNote(context);
                case AllocationType.总部到城市:
                    return GetHQToCitySettlementNote(context);
                default:
                    return null;
            }
        }
        /// <summary>
        /// 获取供应商-城市的结算单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private AllocationSettlementNote GetProviderToCitySettlementNote(ERPContext context)
        {
            if (this.City.Id == this.AllocationNote.ToCity.Id)
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.总部到城市);
            }
            else
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.供应商到总部);
            }
        }
        /// <summary>
        /// 获取城市-城市的结算单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private AllocationSettlementNote GetCityToCitySettlementNote(ERPContext context)
        {
            if (this.City.Id == this.AllocationNote.ToCity.Id)
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.总部到城市);
            }
            else
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.城市到总部);
            }
        }
        /// <summary>
        /// 获取总部-城市的结算单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private AllocationSettlementNote GetHQToCitySettlementNote(ERPContext context)
        {
            if (this.City.Id == this.AllocationNote.ToCity.Id)
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.总部到城市);
            }
            return null;
        }
        /// <summary>
        /// 获取供应商-总部的结算单
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        private AllocationSettlementNote GetProviderToHQSettlementNote(ERPContext context)
        {
            if (this.City.Id == this.AllocationNote.ToCity.Id)
            {
                return context.AllocationSettlementNotes.FirstOrDefault(n =>
                      n.AllocationNote.Id == this.AllocationNote.Id &&
                    n.SettlementType == AllocationSettlementType.供应商到总部);
            }
            return null;
        }
         */
        #endregion
    }
}
