using ERP.Domain.Enums.PrizeNoteModule;
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
    /// 兑出奖券
    /// </summary>
    public class RaffleOutTicket : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 奖券编码
        /// </summary>
        public string TicketNo { get; set; }

        /// <summary>
        /// 关联的订单编号
        /// </summary>
        public string OrderNo { get; set; }

        /// <summary>
        /// 兑出数量
        /// </summary>
        public int OutCount { get; set; }

        /// <summary>
        /// 兑出金额
        /// </summary>
        public decimal OutAmount { get; set; }

        /// <summary>
        /// 兑入数量
        /// </summary>
        public int InCount { get; set; }

        /// <summary>
        /// 过期日期
        /// </summary>
        public DateTime ExpireTime { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public RaffleOutTicketState State { get; set; }

        /// <summary>
        /// 是否特殊产品
        /// </summary>
        public bool IsParticularProduct { get; set; }

        #endregion

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

        #region 业务方法

        public virtual void UpdateState()
        {
            if (this.InCount == 0)
                this.State = RaffleOutTicketState.未兑入;
            else if (this.InCount == this.OutCount)
                this.State = RaffleOutTicketState.已兑入;
            else
                this.State = RaffleOutTicketState.部分兑入;
        }

        #endregion
    }
}
