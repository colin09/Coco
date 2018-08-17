using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class WriteOffPayableBillItem : BaseEntity
    {
        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PrepayWriteOffBill")]
        public string PrepayWriteOffBill_Id { get; set; }
        /// <summary>
        /// 预付核销单
        /// </summary>
        public PrepayWriteOffBill PrepayWriteOffBill { get; set; }
        [MaxLength(64)]
        [ForeignKey("PayableBill")]
        public string PayableBill_Id { get; set; }
        /// <summary>
        /// 应付单
        /// </summary>
        public PayableBill PayableBill { get; set; }

        #endregion

        #region 业务方法

        public void WriteOff(decimal hasPayCounterFee)
        {
            if (this.PayableBill.AdValoremAmount < 0)
                throw new BusinessException("负数应付单不能参与核销！");

            this.PayableBill.HasPaymentAmount += this.WriteOffAmount;

            //PayableBill.HasPaymentAmount  包括下推付款单的手续费，因此需要排序此项金额
            if (this.PayableBill.HasPaymentAmount - hasPayCounterFee > this.PayableBill.AdValoremAmount)
                throw new BusinessException("核销金额与下推金额合计不能超出应付单金额！");

            if (this.PayableBill.HasPaymentAmount - hasPayCounterFee == this.PayableBill.AdValoremAmount)
            {
                this.PayableBill.IsPayment = true;
                this.PayableBill.IsDownReason = true;
            }

            WriteOffUpdateReceivableBillItems();

            //todo:
            //var payableBill = context.PayableBills.Include("City").Include("AllocationNote.ToCity")
            // .Include("AllocationNote.FromCity").WhereNotDeleted()
            // .FirstOrDefault(b => b.Id == this.PayableBill.Id);
            //if (payableBill != null)
            //{
            //    payableBill.UpdateAllocationSettlementNoteWriteOffAmount(context, this.WriteOffAmount);
            //}
        }

        public void UnWriteOff()
        {
            this.PayableBill.HasPaymentAmount -= this.WriteOffAmount;
            this.PayableBill.IsPayment = false;
            this.PayableBill.IsDownReason = false;

            UnWriteOffUpdateReceivableBillItems();

            //todo:
            //var payableBill = context.PayableBills.Include("City").Include("AllocationNote.ToCity")
            //   .Include("AllocationNote.FromCity").WhereNotDeleted()
            //   .FirstOrDefault(b => b.Id == this.PayableBill.Id);

            //if (payableBill != null)
            //{
            //    payableBill.UpdateAllocationSettlementNoteWriteOffAmount(context, 0 - this.WriteOffAmount);
            //}
        }

        /// <summary>
        /// 核销更新应收单项
        /// 算法：根据未付款金额 分摊核销金额
        /// </summary>
        /// <param name="context"></param>
        public void WriteOffUpdateReceivableBillItems()
        {
            List<PayableBillItem> payableBillItems = this.PayableBill.Items;

            if (payableBillItems == null || !payableBillItems.Any())
                throw new BusinessException("未加载应付单项，无法核销");

            decimal writeOffAmount = this.WriteOffAmount;
            if (writeOffAmount > 0)
            {
                decimal totalItemWriteOffAmount = 0;
                decimal totalAmount = payableBillItems.Sum(t => t.AdValoremAmount - t.HasPaymentAmount);
                PayableBillItem item;
                for (int i = 0; i < payableBillItems.Count; i++)
                {
                    item = payableBillItems[i];
                    decimal itemWriteOffAmount;
                    //分摊核销金额有差异时，强制同步
                    if (i == payableBillItems.Count - 1)
                    {
                        itemWriteOffAmount = writeOffAmount - totalItemWriteOffAmount;
                    }
                    else
                    {
                        itemWriteOffAmount = Math.Round((item.AdValoremAmount - item.HasPaymentAmount) * writeOffAmount / totalAmount, 6);
                    }
                    totalItemWriteOffAmount += itemWriteOffAmount;
                    item.HasPaymentAmount += itemWriteOffAmount;
                    item.WriteOffAmount += itemWriteOffAmount;
                }
            }
        }

        /// <summary>
        /// 反核销更新应收单项
        /// 算法：根据应收单项总核销金额 反向分摊
        /// </summary>
        /// <param name="context"></param>
        public void UnWriteOffUpdateReceivableBillItems()
        {
            List<PayableBillItem> payableBillItems =this.PayableBill.Items;

            if (payableBillItems == null || !payableBillItems.Any())
                throw new BusinessException("未加载应付单项，无法反核销");
            //核销金额
            decimal writeOffAmount = this.WriteOffAmount;
            //应收单项总核销金额
            decimal totalWriteOffAmount = payableBillItems.Sum(i => i.WriteOffAmount);

            if (writeOffAmount > 0 && totalWriteOffAmount > 0)
            {
                decimal totalItemWriteOffAmount = 0;

                PayableBillItem item;
                for (int i = 0; i < payableBillItems.Count; i++)
                {
                    item = payableBillItems[i];
                    decimal itemWriteOffAmount = Math.Round(item.WriteOffAmount * writeOffAmount / totalWriteOffAmount, 6);
                    if (itemWriteOffAmount > item.HasPaymentAmount)
                    {
                        itemWriteOffAmount = item.HasPaymentAmount;
                    }
                    //分摊核销金额有差异时，强制同步
                    if (i == payableBillItems.Count - 1)
                    {
                        itemWriteOffAmount = writeOffAmount - totalItemWriteOffAmount;
                    }
                    totalItemWriteOffAmount += itemWriteOffAmount;
                    item.HasPaymentAmount -= itemWriteOffAmount;
                    item.WriteOffAmount -= itemWriteOffAmount;
                }
            }
        }

        #endregion
    }
}
