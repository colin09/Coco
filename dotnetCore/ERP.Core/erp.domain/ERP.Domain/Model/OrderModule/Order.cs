using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Model.OrgModule;
using ERP.Domain.Model.ShopModule;
using ERP.Domain.Model.UserModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Toolbelt.ComponentModel.DataAnnotations.Schema; // for Annotations Index
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    /// <summary>
    /// 订单
    /// </summary>
    public class Order : BaseEntity, IAggregationRoot
    {
        #region 基本属性

        /// <summary>
        /// 是否下推生成了 销售出库单
        /// </summary>
        public bool HasCreateNote { get; set; }

        /// <summary>
        /// 是否代销售订单
        /// </summary>
        public bool IsAgencySaleOrder { get; set; }

        /// <summary>
        /// 只有招商会员订单 区域配送的  有改字段
        /// </summary>
        [MaxLength(64)]
        public string ToStoreHouseId { get; set; }

        ///<summary>
        ///订单编号
        ///</summary>
        [MaxLength(50)]
        //[Index(IsClustered = false)]
        [Index]
        public string OrderNo { get; set; }

        /// <summary>
        /// 订单类型
        /// </summary>
        //[Index(IsClustered = false)]
        [Index]
        public OrderType OrderType { get; set; }

        /// <summary>
        /// 大货批发模式
        /// </summary>
        public BigGoodsMode BigGoodsMode { get; set; }

        /// <summary>
        /// 使用的红包金额,为0说明不使用红包
        /// </summary>
        public decimal Bonus { get; set; }

        /// <summary>
        /// 使用的优惠券金额
        /// </summary>
        public decimal Coupons { get; set; }

        /// <summary>
        /// 商品总金额: 商品的O2O售价*数量
        /// </summary>
        public decimal ProductAmount { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 订单总金额: 
        /// 1. O2O订单：商品活动价（O2O售价）* 数量
        /// 2. B2C订单: 商品活动价（B2C售价）* 数量 + 运费
        /// 3. 定制酒订单：商品价格* 数量 + 运费
        /// </summary>
        public decimal OrderAmount { get; set; }

        ///<summary>
        ///应付金额
        ///</summary>
        public decimal PayableAmount { get; set; }

        /// <summary>
        /// 现金标记金额
        /// </summary>
        public decimal CashMarkAmount { get; set; }

        /// <summary>
        /// 银行付款标记金额
        /// </summary>
        public decimal BankMarkAmount { get; set; }

        /// <summary>
        /// 微信标记金额
        /// </summary>
        public decimal WeiXinMarkAmount { get; set; }
        /// <summary>
        /// 支付宝标记金额
        /// </summary>
        public decimal AlipayMarkAmount { get; set; }

        /// <summary>
        /// 订单减免金额
        /// </summary>
        public decimal ReduceAmount { get; set; }

        /// <summary>
        /// 兑奖红包金额
        /// </summary>
        public decimal UseRewardBonusAmount { get; set; }

        /// <summary>
        /// 抹零金额
        /// </summary>
        public decimal OddAmount { get; set; }

        /// <summary>
        /// 完成配送时间
        /// </summary>
        public DateTime CompleteTime { get; set; }

        #endregion

        #region 聚合属性

        /// <summary>
        /// 订单项
        /// </summary>
        public List<OrderItem> Items { get; set; }

        public string User_Id { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        [ForeignKey("User_Id")]
        public User User { get; set; }

        public string BrokerUser_Id { get; set; }

        /// <summary>
        /// 下单当时的经纪人关系
        /// </summary>
        [ForeignKey("BrokerUser_Id")]
        public OrgUser BrokerUser { get; set; }

        public string City_Id { get; set; }

        /// <summary>
        /// 组织机构
        /// </summary>
        [ForeignKey("City_Id")]
        [Index(IsUnique = false)]
        public City City { get; set; }

        [ForeignKey("SaleCity")]
        [MaxLength(64)]
        public string SaleCity_Id { get; set; }
        /// <summary>
        /// 销售城市
        /// </summary>
        public City SaleCity { get; set; }

        public string Shop_Id { get; set; }

        /// <summary>
        /// 经销商
        /// </summary>
        [ForeignKey("Shop_Id")]
        public Shop Shop { get; set; }
        public string StoreHouse_Id { get; set; }
        /// <summary>
        /// 仓库
        /// </summary>
        [ForeignKey("StoreHouse_Id")]
        public StoreHouse StoreHouse { get; set; }
        #endregion

    }
}
