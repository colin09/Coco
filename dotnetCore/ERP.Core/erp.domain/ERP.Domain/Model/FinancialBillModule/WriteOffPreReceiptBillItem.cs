using ERP.Common.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;


namespace ERP.Domain.Model.FinancialBillModule
{
    public class WriteOffPreReceiptBillItem : BaseEntity
    {
        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PreReceiptBill")]
        public string PreReceiptBill_Id { get; set; }
        /// <summary>
        /// 预收单
        /// </summary>
        public PreReceiptBill PreReceiptBill { get; set; }
        [MaxLength(64)]
        [ForeignKey("PreReceiptWriteOffBill")]
        public string PreReceiptWriteOffBill_Id { get; set; }
        /// <summary>
        /// 所属核销单据
        /// </summary>
        public PreReceiptWriteOffBill PreReceiptWriteOffBill { get; set; }

        #endregion

        public virtual void WriteOff()
        {
            this.PreReceiptBill.WriteOffAmount += this.WriteOffAmount;

            if (this.PreReceiptBill.WriteOffAmount > this.PreReceiptBill.PreReceiptAmount)
                throw new BusinessException("核销总金额不能超出预收单金额！");

            this.PreReceiptBill.ChangeWriteOffState();
        }

        public virtual void UnWriteOff()
        {
            this.PreReceiptBill.WriteOffAmount -= this.WriteOffAmount;

            this.PreReceiptBill.ChangeWriteOffState();
        }
    }
}
