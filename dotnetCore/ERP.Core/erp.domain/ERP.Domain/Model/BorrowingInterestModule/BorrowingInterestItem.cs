
using ERP.Domain.Enums.BorrowingInterestModule;
using ERP.Domain.Model.CashierDiaryModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.BorrowingInterestModule
{
    public class BorrowingInterestItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal Amount { get; set; }

        public BorrowingInterestType BorrowingInterestType { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("BorrowingInterest")]
        [MaxLength(64)]
        public string BorrowingInterest_Id { get; set; }
        public BorrowingInterest BorrowingInterest { get; set; }
        [ForeignKey("CashierDiary")]
        [MaxLength(64)]
        public string CashierDiary_Id { get; set; }
        public CashierDiary CashierDiary { get; set; }
        [ForeignKey("PaymentTypeSetting")]
        [MaxLength(64)]
        public string PaymentTypeSetting_Id { get; set; }
        /// <summary>
        /// 账套
        /// </summary>
        public PaymentTypeSetting PaymentTypeSetting { get; set; }
        #endregion
    }
}

