using ERP.Domain.Context;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.FinancialBillModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using ERP.Domain.Enums.FinancialDailyModule;

namespace ERP.Domain.Model.FinancialDailyModule
{
    public class IncomeDaily : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal ReceiptAmount { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public decimal ReceivableAmount { get; set; }

        /// <summary>
        /// 销售费用
        /// </summary>
        public decimal SellingExpense { get; set; }

        /// <summary>
        /// 成本
        /// </summary>
        public decimal CostAmount { get; set; }

        /// <summary>
        /// 毛利
        /// </summary>
        public decimal GrossProfit { get; set; }

        /// <summary>
        /// 净利
        /// </summary>
        public decimal NetProfit { get; set; }

        /// <summary>
        /// 付款方式
        /// </summary>
        public DailyType DailyType { get; set; }

        /// <summary>
        /// 是否合作商日报
        /// </summary>
        public bool IsSupplier { get; set; }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }

        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法
        /*
        public static IncomeDaily GetIncomeDaily(int year, int month, int day,
            string productId, City city, DailyType settlement, ERPContext context, bool isSupplier = false)
        {
            IncomeDaily daily;
            if (string.IsNullOrWhiteSpace(productId))
                daily = context.IncomeDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == year && d.Month == month && d.Day == day
                    && d.City.Id == city.Id && d.DailyType == settlement);
            else
                daily = context.IncomeDailys.Include("Product").Include("City").FirstOrDefault(d => d.Year == year && d.Month == month && d.Day == day
                   && d.Product.Id == productId && d.City.Id == city.Id);
            if (daily == null)
            {
                daily = new IncomeDaily()
                   {
                       Year = year,
                       Month = month,
                       Day = day,
                       City = city,
                       IsSupplier = isSupplier
                   };
                if (string.IsNullOrWhiteSpace(productId))
                    daily.DailyType = settlement;
                else
                    daily.Product = context.Products.First(p => p.Id == productId);
                context.IncomeDailys.Add(daily);
            }

            return daily;
        }
        */
        #endregion
    }
}
