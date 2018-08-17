using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using ERP.Domain.Context;
using ERP.Domain.Enums.CashierDiaryModule;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Domain.Model.CashierDiaryModule
{
    /// <summary>
    /// 出纳日记账
    /// </summary>
    public class CashierDiary : BaseEntity, IAggregationRoot, IDeleted
    {
        #region 基本属性

        /// <summary>
        /// 摘要名称
        /// </summary>
        [MaxLength(64)]
        public string Name { get; set; }

        /// <summary>
        /// 钱流入流出
        /// </summary>
        public MoneyFlowType MoneyFlowType { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public CashierDiaryType Type { get; set; }

        /// <summary>
        /// 会计结算年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 会计结算月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 业务时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 收入金额
        /// </summary>
        public decimal IncomeAmount { get; set; }


        /// <summary>
        /// 支出金额
        /// </summary>
        public decimal SpendingAmount { get; set; }


        /// <summary>
        /// 结存金额
        /// </summary>
        public decimal BalanceAmount { get; set; }


        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }
        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        /// <summary>
        /// 记账城市
        /// </summary>
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("SourceCity")]
        public string SourceCity_Id { get; set; }
        /// <summary>
        /// 账务来源城市（收付平台使用费或收付借款利息时必填）
        /// </summary>
        public City SourceCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("PaymentTypeSetting")]
        public string PaymentTypeSetting_Id { get; set; }
        /// <summary>
        /// 账套
        /// </summary>
        public PaymentTypeSetting PaymentTypeSetting { get; set; }
        [MaxLength(64)]
        [ForeignKey("PaymentBillItem")]
        public string PaymentBillItem_Id { get; set; }
        /// <summary>
        /// 付款单
        /// </summary>
        public PaymentBillItem PaymentBillItem { get; set; }
        [MaxLength(64)]
        [ForeignKey("ReceiptBillItem")]
        public string ReceiptBillItem_Id { get; set; }
        /// <summary>
        /// 收款单
        /// </summary>
        public ReceiptBillItem ReceiptBillItem { get; set; }

        #endregion

        #region 业务方法
        /*
        /// <summary>
        /// 计算结算金额
        /// </summary>
        /// <param name="context"></param>
        public virtual void SetBalanceAmount(ERPContext context)
        {
            var model = context.CashierDiary.AsNoTracking().Where(c => c.Type == this.Type && c.City.Id == this.City.Id).OrderByDescending(c => c.CreateTime).FirstOrDefault();
            decimal initialBalanceAmount = 0;
            if (model != null && !string.IsNullOrEmpty(model.Id))
            {
                initialBalanceAmount = model.BalanceAmount;
            }
            if (this.MoneyFlowType == MoneyFlowType.支出)
                this.BalanceAmount = initialBalanceAmount - this.SpendingAmount;
            if (this.MoneyFlowType == MoneyFlowType.收入)
                this.BalanceAmount = initialBalanceAmount + this.IncomeAmount;

        }

        /// <summary>
        /// 删除生成抵消记录
        /// </summary>
        /// <param name="context"></param>
        public virtual void Counteract(CashierDiary oldModel)
        {
            this.Name = oldModel.Name;
            this.BusinessTime = DateTime.Now;
            this.Year = DateTime.Now.Year;
            this.Month = DateTime.Now.Month;
            this.City = oldModel.City;
            this.PaymentTypeSetting = oldModel.PaymentTypeSetting;
            this.MoneyFlowType = oldModel.MoneyFlowType;
            this.Type = oldModel.Type;
            this.Remark = "删除操作后生成的冲抵记录";
            this.SpendingAmount = oldModel.SpendingAmount * -1;
            this.IncomeAmount = oldModel.IncomeAmount * -1;
        }*/
        #endregion

    }
}
