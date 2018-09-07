
using ERP.Domain.Model.CashierDiaryModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.BorrowingInterestModule
{
    public class BorrowingInterest : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 本金
        /// </summary>
        public decimal Capital { get; set; }

        /// <summary>
        /// 利息
        /// </summary>
        public decimal Interest { get; set; }

        #endregion

        #region 聚合属性
        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        public City City { get; set; }
        [ForeignKey("SourceCity")]
        [MaxLength(64)]
        public string SourceCity_Id { get; set; }
        public City SourceCity { get; set; }
        public List<BorrowingInterestItem> Items { get; set; }

        #endregion
    }
}

