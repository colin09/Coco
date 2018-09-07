using ERP.Domain.Context;
using ERP.Domain.Enums.AllocationNoteModule;
using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.PurchaseInStockModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AllocationNoteModule
{
    /// <summary>
    /// 调拨结算单
    /// </summary>
    public class AllocationSettlementNote : BaseEntity, IAggregationRoot
    {

        /// <summary>
        /// 调拨结算类型
        /// </summary>
        public AllocationSettlementType SettlementType { get; set; }

        /// <summary>
        /// 业务日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 业务类型
        /// </summary>
        public AllocationBusinessType BusinessType { get; set; }

        /// <summary>
        /// 调拨类型
        /// </summary>
        public AllocationType AllocationType { get; set; }
        /// <summary>
        /// 结算状态
        /// </summary>
        public AllocationSettlementState SettlementState { get; set; }

        /// <summary>
        /// 调拨单总金额
        /// </summary>
        public decimal TotalAmount { get; set; }
        /// <summary>
        /// 预付金额
        /// </summary>
        public decimal PrepayAmount { get; set; }

        /// <summary>
        /// 已结算金额
        /// </summary>
        public decimal SettlementAmount { get; set; }

        #region 冗余字段

        /// <summary>
        /// 关联的调拨单编号
        /// </summary>
        [MaxLength(64)]
        public string AllocationNoteNo { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 关联的调拨单
        /// </summary>
        public AllocationNote AllocationNote { get; set; }

        /// <summary>
        /// 只有类型为供应商-总部，供应商-城市类型 有值
        /// </summary>
        public Provider FromProvider { get; set; }
        /// <summary>
        /// 除 供应商-总部，供应商-城市外的
        /// </summary>
        public City FromCity { get; set; }
        public City ToCity { get; set; }

        #endregion

        #region 业务方法
        /*
        /// <summary>
        /// 更新结算状态
        /// </summary>
        /// <param name="inStockNotes">调拨单生成的所有和当前结算单相关的入库单</param>
        /// <param name="payableBill">当前生成、删除 核销单或付款单的  应付单</param>
        /// <param name="payableBills">调拨单生成的入库单生成的应付单（排除当前应付单）</param>
        public virtual void UpdateState(ERPContext context, PayableBill payableBill)
        {
            //调拨单所有的采购入库单
            var purchaseInStockNotes = context.PurchaseInStockNotes.Include("City").AsNoTracking().WhereNotDeleted()
                  .Where(n => n.AllocationNote.Id == this.AllocationNote.Id).ToList();

            //采购入库单id
            string[] inStockNoteIds = purchaseInStockNotes.Select(n => n.Id).ToArray();
            //采购入库单产生的应收单
            var payableBills = context.PayableBills.AsNoTracking().Include("City").WhereNotDeleted()
                .Where(b => b.PurchaseInStockNote.AllocationNote.Id == this.AllocationNote.Id
                && b.Id != payableBill.Id && inStockNoteIds.Contains(b.PurchaseInStockNote.Id)).ToList();
            //当前结算单是否所有入库单据均已下推应付单完毕（包含虚拟入库单）
            bool allCreatePayableBills = this.AllocationNote.IsAllCreatePayableBills(purchaseInStockNotes);
            //是否调入城市调拨单
            bool isToCitySettlement = payableBill.City.Id == this.AllocationNote.ToCity.Id;
            bool settlementCityAllCreatePayableBills = false;

            //结算相关应付单
            PayableBill[] settlementPayableBills;
            if (isToCitySettlement)
            {
                settlementCityAllCreatePayableBills = (this.AllocationNote.State == AllocationState.待结算 ||
                this.AllocationNote.State == AllocationState.已结算) && purchaseInStockNotes.Where(n => n.City.Id == this.AllocationNote.ToCity.Id).All(n => n.IsDownReason);
                settlementPayableBills = payableBills.Where(b => b.City.Id == this.AllocationNote.ToCity.Id).ToArray();
            }
            else
            {
                //调出城市 入库是否全部
                settlementCityAllCreatePayableBills = (((this.AllocationNote.State == AllocationState.待结算 ||
                this.AllocationNote.State == AllocationState.已结算) && purchaseInStockNotes.Where(n => n.City.Id == this.AllocationNote.ToCity.Id).All(n => n.HasCreateRelatedNote))
                || this.AllocationNote.Type == AllocationType.城市到城市 && (this.AllocationNote.State == AllocationState.待结算 ||
                this.AllocationNote.State == AllocationState.已结算 || this.AllocationNote.State == AllocationState.收货中)
                )
                && purchaseInStockNotes.Where(n => n.City.Id != this.AllocationNote.ToCity.Id).All(n => n.IsDownReason);
                settlementPayableBills = payableBills.Where(b => b.City.Id != this.AllocationNote.ToCity.Id).ToArray();
            }
            //结算单已结算金额
            var amount = this.SettlementAmount + this.PrepayAmount;
            //1.结算金额为0：未结算
            //2.调拨单为待结算或已结算状态，入库单全部下推应付单，应付单全部已付款：已结算；
            // 或  调拨单为城市-城市，状态为已发货或待结算或已结算，入库单全部下推应付单，应付单全部付款：已结算
            //3. 其他为待结算状态
            if (amount == decimal.Zero)
            {
                this.SettlementState = AllocationSettlementState.未结算;
            }
            else if (settlementCityAllCreatePayableBills && payableBill.IsPayment
                && settlementPayableBills != null
                && settlementPayableBills.All(b => b.IsPayment))
            {
                this.SettlementState = AllocationSettlementState.已结算;
            }
            else
            {
                this.SettlementState = AllocationSettlementState.部分结算;
            }


            //调拨单结算状态变更
            if ((this.AllocationNote.State == AllocationState.待结算 || this.AllocationNote.State == AllocationState.已结算)
                && allCreatePayableBills && payableBill.IsPayment && payableBills.All(p => p.IsPayment))
            {
                this.AllocationNote.State = AllocationState.已结算;
            }
            else if (this.AllocationNote.State == AllocationState.已结算)
            {
                //原单据为已结算，现不是，应为删除核销或付款单
                this.AllocationNote.State = AllocationState.待结算;
            }
        }*/



        #endregion
    }
}
