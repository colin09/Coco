using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Enums.FinancialSettlementModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialSettlementModule
{
    public class FinancialSettlement : BaseEntity, IAggregationRoot
    {
        public FinancialSettlement()
        {
            this.State = SettlementState.未结账;
            DateTime now = DateTime.Now;

            Year = now.Year;
            Month = now.Month;
            StartDate = new DateTime(now.Year, now.Month, 1);
            EndDate = StartDate.AddMonths(1).AddSeconds(-1);
        }

        public FinancialSettlement(int year, int month)
        {
            this.State = SettlementState.未结账;

            Year = year;
            Month = month;
            StartDate = new DateTime(year, month, 1);
            EndDate = StartDate.AddMonths(1).AddSeconds(-1);
        }

        public int Year { get; set; }
        public int Month { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        /// <summary>
        /// 结账状态
        /// </summary>
        public SettlementState State { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }
        [MaxLength(64)]
        [ForeignKey("FinancialSettlementCheck")]
        public string FinancialSettlementCheck_Id { get; set; }
        public FinancialSettlementCheck FinancialSettlementCheck { get; set; }

        #endregion

        #region 业务方法

        /// <summary>
        /// 结账
        /// </summary>
        public void Settlement()
        {
            if (this.State == SettlementState.已结账)
                throw new BusinessException("已结账");

            this.State = SettlementState.已结账;
            this.LastUpdateTime = DateTime.Now;
        }

        /// <summary>
        /// 反结账
        /// </summary>
        public void UnSettlement()
        {
            if (this.State != SettlementState.已结账)
                throw new BusinessException("当前状态不能反结账");

            this.State = SettlementState.未结账;
            this.LastUpdateTime = DateTime.Now;
        }

        #endregion
    }
}
