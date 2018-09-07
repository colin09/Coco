using ERP.Common.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    public class WriteOffPrepayBillItem : BaseEntity
    {
        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("PrepayWriteOffBill")]
        public string PrepayWriteOffBill_Id { get; set; }
        public PrepayWriteOffBill PrepayWriteOffBill { get; set; }
        [MaxLength(64)]
        [ForeignKey("PrepayBill")]
        public string PrepayBill_Id { get; set; }
        /// <summary>
        /// 预付单
        /// </summary>
        public PrepayBill PrepayBill { get; set; }

        #endregion

        #region 业务方法

        public virtual void WriteOff()
        {
            this.PrepayBill.WriteOffAmount += this.WriteOffAmount;

            if (this.PrepayBill.WriteOffAmount > this.PrepayBill.PrepayAmount - this.PrepayBill.RefundAmount)
                throw new BusinessException("核销总金额不能大于预付单的预付金额！");

            this.PrepayBill.ChangeWriteOffState();
        }

        public virtual void UnWriteOff()
        {
            this.PrepayBill.WriteOffAmount -= this.WriteOffAmount;

            this.PrepayBill.ChangeWriteOffState();
        }

        #endregion
    }
}
