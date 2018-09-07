using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    /// <summary>
    /// 退货订单项目
    /// </summary>
    [Table("ReturnOrderItem")]
    public class ReturnOrderItem : BaseEntity
    {
        #region 基本属性

        /// <summary>
        /// 退货数量
        /// </summary>
        /// <remarks>
        /// 不能超过购买数量
        /// </remarks>
        public int ReturnCount { get; set; }

        /// <summary>
        /// 成本金额
        /// </summary>
        public decimal ConstAmount { get; set; }

        /// <summary>
        /// 补充利润
        /// </summary>
        public decimal AdditionalProfit { get; set; }

        [NotMapped]
        public decimal TotalReturnPrice
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (OrderItem.SaleUnit == OrderItem.Specifications)
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount * OrderItem.PackageQuantity : OrderItem.Price * ReturnCount;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount : OrderItem.Price * ((decimal)1.0) / OrderItem.PackageQuantity;
                }

                return totalPrice;
            }
        }

        #endregion

        #region 聚合属性
        [MaxLength(64)]
        [ForeignKey("OrderItem")]
        public string OrderItem_Id { get; set; }
        public OrderItem OrderItem { get; set; }

        [MaxLength(64)]
        [ForeignKey("ReturnOrder")]
        public string ReturnOrder_Id { get; set; }

        public ReturnOrder ReturnOrder { get; set; }
        #endregion

        #region 方法

        public decimal TotalAmount
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (OrderItem.SaleUnit == OrderItem.Specifications)
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount * OrderItem.PackageQuantity : OrderItem.Price * ReturnCount;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount : OrderItem.Price * ((decimal)1.0) / OrderItem.PackageQuantity;
                }
                return totalPrice;
            }
        }

        public decimal TotalOriAmount
        {
            get
            {
                //如果是按大单位出售， 实际销售此项居多
                if (OrderItem.SaleUnit == OrderItem.Specifications)
                    return ReturnCount * OrderItem.OriginalPrice * (OrderItem.PriceUnit == OrderItem.PackageUnit ? OrderItem.PackageQuantity : 1);
                else
                    return ReturnCount * OrderItem.OriginalPrice / (OrderItem.PriceUnit == OrderItem.PackageUnit ? 1 : OrderItem.PackageQuantity == 0 ? 1 : OrderItem.PackageQuantity);
            }
        }

        public decimal TotalPrice
        {
            get
            {
                decimal totalPrice = 0;

                //如果是按大单位出售， 实际销售此项居多
                if (OrderItem.SaleUnit == OrderItem.Specifications)
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount * OrderItem.PackageQuantity : OrderItem.Price * ReturnCount;
                }
                else //按小单位出售 , 一般赠品按瓶计算较多
                {
                    totalPrice = OrderItem.IsUnitPrice ? OrderItem.Price * ReturnCount : OrderItem.Price * ((decimal)1.0) / OrderItem.PackageQuantity;
                }
                return totalPrice;
            }
        }

        public decimal OriginalTotalPrice
        {
            get
            {
                return ReturnCount * OrderItem.OriginalPrice * (OrderItem.PriceUnit == OrderItem.PackageUnit ? OrderItem.PackageQuantity : 1);
            }
        }
        #endregion
    }
}
