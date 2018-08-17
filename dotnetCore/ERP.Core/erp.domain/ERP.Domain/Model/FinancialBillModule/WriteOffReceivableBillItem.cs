using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class WriteOffReceivableBillItem : BaseEntity
    {
        public decimal WriteOffAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PreReceiptWriteOffBill")]
        public string PreReceiptWriteOffBill_Id { get; set; }
        /// <summary>
        /// 所属预收核销单
        /// </summary>
        public PreReceiptWriteOffBill PreReceiptWriteOffBill { get; set; }
        [MaxLength(64)]
        [ForeignKey("ReceivableBill")]
        public string ReceivableBill_Id { get; set; }
        /// <summary>
        /// 应收单
        /// </summary>
        public ReceivableBill ReceivableBill { get; set; }

        #endregion

        public void WriteOff(decimal hasReceiptCounterFee)
        {
            if (this.ReceivableBill.PaidInAmount < 0)
                throw new BusinessException("负数应收单不能参与核销！");

            this.ReceivableBill.HasReceiptAmount += this.WriteOffAmount;

            if (this.ReceivableBill.HasReceiptAmount + hasReceiptCounterFee > this.ReceivableBill.PaidInAmount)
                throw new BusinessException("核销金额与下推付款单合计金额不能大于应收单金额！");

            if (this.ReceivableBill.HasReceiptAmount + hasReceiptCounterFee == this.ReceivableBill.PaidInAmount)
            {
                this.ReceivableBill.HasReceipt = true;
            }

            WriteOffUpdateReceivableBillItems();
        }

        /// <summary>
        /// 核销更新应收单项
        /// 算法：根据未付款金额 分摊核销金额
        /// </summary>
        /// <param name="context"></param>
        public void WriteOffUpdateReceivableBillItems()
        {
            if (this.ReceivableBill.Items == null || !this.ReceivableBill.Items.Any())
            {
                throw new BusinessException("未加载应收单项，无法核销计算");
            }

            decimal writeOffAmount = this.WriteOffAmount;
            if (writeOffAmount > 0)
            {
                decimal totalItemWriteOffAmount = 0;
                decimal totalAmount = this.ReceivableBill.Items.Sum(t => t.AdValoremAmount - t.HasReceiptAmount);
                ReceivableBillItem item;
                for (int i = 0; i < this.ReceivableBill.Items.Count; i++)
                {
                    item = this.ReceivableBill.Items[i];
                    decimal itemWriteOffAmount;
                    //分摊核销金额有差异时，强制同步
                    if (i == this.ReceivableBill.Items.Count - 1)
                    {
                        itemWriteOffAmount = writeOffAmount - totalItemWriteOffAmount;
                    }
                    else
                    {
                        itemWriteOffAmount = Math.Round((item.AdValoremAmount - item.HasReceiptAmount) * writeOffAmount / totalAmount, 6);
                    }
                    totalItemWriteOffAmount += itemWriteOffAmount;
                    item.HasReceiptAmount += itemWriteOffAmount;
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
            List<ReceivableBillItem> receivableBillItems = this.ReceivableBill.Items;

            if (receivableBillItems == null || !receivableBillItems.Any())
                throw new BusinessException("未加载应收单项，无法反核销");

            //核销金额
            decimal writeOffAmount = this.WriteOffAmount;
            //应收单项总核销金额
            decimal totalWriteOffAmount = receivableBillItems.Sum(i => i.WriteOffAmount);

            if (writeOffAmount > 0 && totalWriteOffAmount > 0)
            {
                decimal totalItemWriteOffAmount = 0;

                ReceivableBillItem item;
                for (int i = 0; i < receivableBillItems.Count; i++)
                {
                    item = receivableBillItems[i];
                    decimal itemWriteOffAmount = Math.Round(item.WriteOffAmount * writeOffAmount / totalWriteOffAmount, 6);
                    if (itemWriteOffAmount > item.HasReceiptAmount)
                    {
                        itemWriteOffAmount = item.HasReceiptAmount;
                    }
                    //分摊核销金额有差异时，强制同步
                    if (i == receivableBillItems.Count - 1)
                    {
                        itemWriteOffAmount = writeOffAmount - totalItemWriteOffAmount;
                    }
                    totalItemWriteOffAmount += itemWriteOffAmount;
                    item.HasReceiptAmount -= itemWriteOffAmount;
                    item.WriteOffAmount -= itemWriteOffAmount;
                }
            }
        }
        public void UnWriteOff()
        {
            this.ReceivableBill.HasReceiptAmount -= this.WriteOffAmount;
            this.ReceivableBill.HasReceipt = false;

            UnWriteOffUpdateReceivableBillItems();
        }
    }
}
