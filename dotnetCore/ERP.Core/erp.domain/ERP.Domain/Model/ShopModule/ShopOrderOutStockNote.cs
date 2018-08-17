using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Enums.ShopModule;
using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.FinancialStockModule;
using ERP.Domain.Model.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ProductLotModule;
using ERP.Domain.Model.ProductModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.ShopModule
{
    /// <summary>
    /// 销售出库单
    /// </summary>
    [Table("ShopOrderOutStockNote")]
    public class ShopOrderOutStockNote : BaseEntity, IAggregationRoot, IDeleted
    {
        public ShopOrderOutStockNote()
        {
            this.State = ShopOrderOutStockNoteState.已完成;
            this.NoteType = ShopOrderOutStockNoteType.经销商出库单;
            this.Items = new List<ShopNoteItem>();
            this.IsDeleted = false;
        }

        #region 基本属性
        /// <summary>
        /// 单据状态
        /// </summary>
        public ShopOrderOutStockNoteState State { get; set; }

        /// <summary>
        /// 单据类型
        /// </summary>
        public ShopOrderOutStockNoteType NoteType { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [MaxLength(200)]
        public string Remark { get; set; }

        /// <summary>
        /// 出库日期
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 红包金额
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        public decimal CouponAmount { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal OddAmount { get; set; }

        /// <summary>
        /// 实收金额
        /// </summary>
        public decimal OrderAmount { get; set; }

        /// <summary>
        /// 订单金额
        /// </summary>
        public decimal TotalAmount { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 是否已删除
        /// </summary>
        public bool IsDeleted { get; set; }

        [MaxLength(64)]
        public string NoteNO { get; set; }

        /// <summary>
        /// 批次号
        /// </summary>
        [MaxLength(64)]
        public string BatchNo { get; set; }


        #region 冗余字段

        /// <summary>
        /// 订单编号
        /// </summary>
        [MaxLength(64)]
        public string OrderNo { get; set; }

        #endregion

        #endregion

        #region 聚合属性
        [ForeignKey("ShopPickupNote")]
        [MaxLength(64)]
        public string ShopPickupNote_Id { get; set; }
        /// <summary>
        /// 经销商提货单
        /// </summary>
        public ShopPickupNote ShopPickupNote { get; set; }

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public City City { get; set; }
        [ForeignKey("StoreHouse")]
        [MaxLength(64)]
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 存放仓库
        /// </summary>
        public StoreHouse StoreHouse { get; set; }
        [ForeignKey("Order")]
        [MaxLength(64)]
        public string Order_Id { get; set; }
        public Order Order { get; set; }
        [ForeignKey("User")]
        [MaxLength(64)]
        public string User_Id { get; set; }
        public User User { get; set; }
        [ForeignKey("BrokerUser")]
        [MaxLength(64)]
        public string BrokerUser_Id { get; set; }
        public OrgUser BrokerUser { get; set; }
        [ForeignKey("Shop")]
        [MaxLength(64)]
        public string Shop_Id { get; set; }
        public Shop Shop { get; set; }

        public List<ShopNoteItem> Items { get; set; }

        #endregion

        #region 方法

        // public virtual void Delete(ERPContext context)
        // {
        //     if (this.IsDeleted)
        //         return;

        //     if (this.Order != null)
        //     {
        //         this.Order.HasCreateNote = false;
        //     }

        //     this.IsDeleted = true;
        // }
        #endregion

    }
}
