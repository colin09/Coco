using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PrizeNoteModule
{
    /// <summary>
    /// 兑奖总券（同一仓库、同一产品的兑奖券 自动汇总到一张总券中）
    /// </summary>
    public class RaffleTicketInfo : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 兑出数量
        /// </summary>
        public int RaffleOutCount { get; set; }

        /// <summary>
        /// 已兑入数量
        /// </summary>
        public int RaffleInCount { get; set; }

        /// <summary>
        /// 使用红包总金额
        /// </summary>
        public decimal TotalBounsAmount { get; set; }

        /// <summary>
        /// 已兑入金额
        /// </summary>
        public decimal RaffleInAmount { get; set; }

        /// <summary>
        /// 兑奖应收款
        /// </summary>
        public decimal RaffleReceivableAmount { get; set; }

        /// <summary>
        /// 是否特殊产品
        /// </summary>
        public bool IsParticularProduct { get; set; }

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("StoreHouse")]
        public string StoreHouse_Id { get; set; }
        public StoreHouse StoreHouse { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion
    }
}
