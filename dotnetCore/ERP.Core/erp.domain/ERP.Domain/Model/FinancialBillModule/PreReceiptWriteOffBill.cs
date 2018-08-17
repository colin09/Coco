using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
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
    /// 预收应收核销单据
    /// </summary>
    public class PreReceiptWriteOffBill : BaseEntity, IAggregationRoot, IDeleted
    {
        /// <summary>
        /// 单据编号
        /// </summary>
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 核销金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }
        public bool IsInternal { get; set; }
        public bool IsDeleted { get; set; }

        #region 聚合属性

        /// <summary>
        /// 核销预收单明细
        /// </summary>
        public List<WriteOffPreReceiptBillItem> PreReceiptBillItems { get; set; }

        /// <summary>
        /// 核销应收单明细
        /// </summary>
        public List<WriteOffReceivableBillItem> ReceivableBillItems { get; set; }

        [MaxLength(64)]
        [ForeignKey("CompanyUser")]
        public string CompanyUser_Id { get; set; }
        /// <summary>
        /// 预收客户 
        /// </summary>
        public User CompanyUser { get; set; }
        [MaxLength(64)]
        [ForeignKey("CompanyUserCity")]
        public string CompanyUserCity_Id { get; set; }
        public Org CompanyUserCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法

        //public virtual void WriteOff(ERPContext context)
        //{
        //    if (this.PreReceiptBillItems.Count == 0)
        //        throw new BusinessException("核销预收单不能为空!");

        //    if (this.ReceivableBillItems.Count == 0)
        //        throw new BusinessException("核销应收单不能为空！");

        //    //可核销总金额
        //    var canWriteOffAmount = this.PreReceiptBillItems.Sum(i => i.PreReceiptBill.PreReceiptAmount - i.PreReceiptBill.WriteOffAmount);
        //    //核销总金额
        //    var writeOffAmount = this.ReceivableBillItems.Sum(i => i.WriteOffAmount);

        //    if (canWriteOffAmount >= writeOffAmount)
        //    {
        //        //可核销总金额大于或等于核销总金额，按照PrepayBill的创建时间 升序开始核销
        //        foreach (var item in this.PreReceiptBillItems.OrderBy(i => i.PreReceiptBill.CreateTime).ToList())
        //        {
        //            item.WriteOffAmount = item.PreReceiptBill.GetCanWriteOffAmount();

        //            if (writeOffAmount > 0 && item.WriteOffAmount > 0)
        //            {
        //                if (writeOffAmount < item.WriteOffAmount)
        //                    item.WriteOffAmount = writeOffAmount;

        //                writeOffAmount -= item.WriteOffAmount;

        //                item.WriteOff();

        //                continue;
        //            }
        //            //PrepayBill无可用核销金额，直接移除
        //            this.PreReceiptBillItems.Remove(item);
        //        }

        //        this.ReceivableBillItems.ForEach(i =>
        //        {
        //            i.WriteOff(context);
        //        });

        //        this.WriteOffAmount = this.ReceivableBillItems.Sum(i => i.WriteOffAmount);
        //        return;
        //    }

        //    //可核销总金额小于核销总金额，按照ReceivableBill的创建时间 升序开始核销
        //    foreach (var item in this.ReceivableBillItems.OrderBy(i => i.ReceivableBill.CreateTime).ToList())
        //    {
        //        if (canWriteOffAmount > 0 && item.WriteOffAmount > 0)
        //        {
        //            if (canWriteOffAmount < item.WriteOffAmount)
        //                item.WriteOffAmount = canWriteOffAmount;

        //            canWriteOffAmount -= item.WriteOffAmount;

        //            item.WriteOff(context);

        //            continue;
        //        }
        //        //无可用核销金额，直接移除
        //        this.ReceivableBillItems.Remove(item);
        //    }

        //    this.PreReceiptBillItems.ForEach(i =>
        //    {
        //        i.WriteOffAmount = i.PreReceiptBill.GetCanWriteOffAmount();
        //        i.WriteOff();
        //    });
        //    this.WriteOffAmount = this.ReceivableBillItems.Sum(i => i.WriteOffAmount);
        //}

        public void UnWriteOff()
        {
            this.PreReceiptBillItems.ForEach(i => i.UnWriteOff());
            this.ReceivableBillItems.ForEach(i => i.UnWriteOff());
        }

        #endregion


    }
}
