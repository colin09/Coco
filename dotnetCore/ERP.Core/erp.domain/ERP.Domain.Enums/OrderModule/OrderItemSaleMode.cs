using ERP.Domain.Enums.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    /// <summary>
    /// 订单项销售模式
    /// </summary>
    public enum OrderItemSaleMode
    {
        无 = 0,
        自营 = 1,
        合作 = 2,
        寄售 = 3,
        大商转自营 = 4,
        大商转配送 = 5,
        代运营 = 6,
        入驻 = 7,
        总部寄售 = 8,
        独家包销 = 9,
        经销商直配 = 10,
    }
    public static class OrderItemSaleModeExtensions
    {
        public static ERPSaleMode ToERPSaleMode(this OrderItemSaleMode saleMode)
        {
            switch (saleMode)
            {
                case OrderItemSaleMode.自营:
                    return ERPSaleMode.自营;
                case OrderItemSaleMode.合作:
                    return ERPSaleMode.合作;
                case OrderItemSaleMode.寄售:
                    return ERPSaleMode.寄售;
                case OrderItemSaleMode.大商转自营:
                    return ERPSaleMode.大商转自营;
                case OrderItemSaleMode.大商转配送:
                    return ERPSaleMode.大商转配送;
                case OrderItemSaleMode.代运营:
                    return ERPSaleMode.代运营;
                case OrderItemSaleMode.入驻:
                    return ERPSaleMode.入驻;
                case OrderItemSaleMode.总部寄售:
                    return ERPSaleMode.总部寄售;
                case OrderItemSaleMode.独家包销:
                    return ERPSaleMode.独家包销;
                case OrderItemSaleMode.经销商直配:
                    return ERPSaleMode.经销商直配;
                default:
                    return ERPSaleMode.无;
            }
        }
    }
}
