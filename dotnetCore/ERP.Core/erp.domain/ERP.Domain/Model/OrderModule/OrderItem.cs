using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.OrderModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    /// <summary>
    /// 订单项
    /// </summary>
    [Table("OrderItem")]
    public class OrderItem : BaseEntity, IEntity
    {
        #region 基本属性

        public OrderItem()
        {
        }

        /// <summary>
        /// 产品名称拷贝 防止后台改错
        /// </summary>
        [MaxLength(100)]
        public string ProductName { get; set; }
        /// <summary>
        /// 商品的原价
        /// </summary>
        public decimal OriginalPrice { get; set; }

        ///<summary>
        ///实际价格
        ///</summary>
        public decimal Price { get; set; }

        /// <summary>
        /// 成本总金额
        /// </summary>
        public decimal CostAmount { get; set; }

        /// <summary>
        /// 该订单项总立减金额
        /// </summary>
        public decimal ReducePrice { get; set; }

        /// <summary>
        /// 价格单位
        /// </summary>
        [MaxLength(32)]
        public string PriceUnit { get; set; }

        public OrderItemSaleMode SaleMode { get; set; }

        /// <summary>
        /// ERP销售模式
        /// </summary>
        public ERPSaleMode ERPSaleMode { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        [MaxLength(32)]
        public string Specifications { get; set; }

        /// <summary>
        /// 包装单位
        /// </summary>
        [MaxLength(32)]
        public string PackageUnit { get; set; }

        /// <summary>
        /// 包装数量
        /// </summary>
        public int PackageQuantity { get; set; }

        ///<summary>
        ///数量
        ///</summary>
        public int Count { get; set; }

        /// <summary>
        /// 销售单位, 赠品可能为瓶， 普通售卖都是件
        /// </summary>
        [MaxLength(32)]
        public string SaleUnit { get; set; }

        public decimal UseBonus { get; set; }
        public decimal UseCoupon { get; set; }
        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        /// <summary>
        /// 兑奖红包分摊金额
        /// </summary>
        public decimal UseRewardBonusAmount { get; set; }

        #endregion

        #region 聚合属性

        [MaxLength(64)]
        [ForeignKey("Product")]
        public string Product_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("Order")]
        public string Order_Id { get; set; }

        public Order Order { get; set; }

        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }
        [MaxLength(64)]
        [ForeignKey("SaleProduct")]
        public string SaleProduct_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product SaleProduct { get; set; }
        #endregion

        #region 方法

        /// <summary>
        /// 是否为最小单位
        /// </summary>
        [NotMapped]
        public bool IsUnitPrice
        {
            get
            {
                return PriceUnit == PackageUnit;
            }
        }

        /// <summary>
        /// 最小单位成本价
        /// </summary>
        [NotMapped]
        public decimal PackageUnitOriginalPrice
        {
            get
            {
                return IsUnitPrice ? OriginalPrice : OriginalPrice / PackageQuantity;
            }
        }

        /// <summary>
        /// 最小单位总数量
        /// </summary>
        [NotMapped]
        public int PackageUnitCount
        {
            get
            {
                int num = 0;

                //如果是按大单位出售
                if (SaleUnit != PackageUnit)
                    num = Count * PackageQuantity;
                else
                    num = Count;

                return num;
            }
        }

        /// <summary>
        /// 以saleunit为单位的价格
        /// </summary>
        [NotMapped]
        public decimal RealPrice
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (SaleUnit == Specifications && SaleUnit != PackageUnit)
                {
                    totalPrice = IsUnitPrice ? Price * PackageQuantity : Price;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = IsUnitPrice ? Price : Price * ((decimal)1.0) / PackageQuantity;
                }

                return totalPrice;
            }
        }

        [NotMapped]
        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (SaleUnit == Specifications && SaleUnit != PackageUnit)
                {
                    totalPrice = IsUnitPrice ? Price * Count * PackageQuantity : Price * Count;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = IsUnitPrice ? Price * Count : Price * ((decimal)1.0) / PackageQuantity;
                }

                return totalPrice;
            }
        }

        [NotMapped]
        public decimal RealOriginalPrice
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (SaleUnit == Specifications && SaleUnit != PackageUnit)
                {
                    totalPrice = IsUnitPrice ? OriginalPrice * PackageQuantity : OriginalPrice;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = IsUnitPrice ? OriginalPrice : OriginalPrice * ((decimal)1.0) / PackageQuantity;
                }

                return totalPrice;
            }
        }

        [NotMapped]
        public decimal TotalAmount
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (SaleUnit == Specifications && SaleUnit != PackageUnit)
                {
                    totalPrice = IsUnitPrice ? Price * Count * PackageQuantity : Price * Count;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = IsUnitPrice ? Price * Count : Price * ((decimal)1.0) / PackageQuantity;
                }

                return totalPrice;
            }
        }

        [NotMapped]
        public decimal TotalOriAmount
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (SaleUnit == Specifications && SaleUnit != PackageUnit)
                {
                    totalPrice = IsUnitPrice ? OriginalPrice * Count * PackageQuantity : OriginalPrice * Count;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = IsUnitPrice ? OriginalPrice * Count : OriginalPrice * Count * ((decimal)1.0) / PackageQuantity;
                }

                return totalPrice;
            }
        }

        /// <summary>
        /// 实付金额
        /// </summary>
        [NotMapped]
        public decimal PaymentAmount
        {
            get
            {
                return this.TotalOriAmount - this.UseCoupon - this.UseBonus - this.ReducePrice;
            }
        }
        #endregion
    }
}
