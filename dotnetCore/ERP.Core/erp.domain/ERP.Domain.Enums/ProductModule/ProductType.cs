using ERP.Domain.Enums.CommonModule;
using ERP.Domain.Enums.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.ProductModule
{
    /// <summary>
    /// 产品类型
    /// </summary>
    public enum ProductType
    {
        //代营(0), 自营(1), 合作(2), 寄售(3), 大商转自营(4), 大商转配送(5), 入驻(6), 总部寄售(7),
        总部寄售 = 2,
        代运营产品 = 4,
        寄售产品 = 6,
        合作商产品 = 7,
        自营产品 = 8,
        入驻 = 9,
        独家包销 = 10,
        经销商直配 = 11,
    }

    public static class ProductTypeExtensions
    {
        public static OrderItemSaleMode ToSaleMode(this ProductType type)
        {
            switch (type)
            {
                case ProductType.自营产品:
                    return OrderItemSaleMode.自营;
                case ProductType.寄售产品:
                    return OrderItemSaleMode.寄售;
                case ProductType.代运营产品:
                    return OrderItemSaleMode.代运营;
                case ProductType.入驻:
                    return OrderItemSaleMode.入驻;
                case ProductType.总部寄售:
                    return OrderItemSaleMode.总部寄售;
                case ProductType.合作商产品:
                    return OrderItemSaleMode.合作;
                case ProductType.经销商直配:
                    return OrderItemSaleMode.经销商直配;
                default:
                    return OrderItemSaleMode.无;
            }
        }
        public static ERPSaleMode ToERPSaleMode(this ProductType type)
        {
            switch (type)
            {
                case ProductType.自营产品:
                    return ERPSaleMode.自营;
                case ProductType.寄售产品:
                    return ERPSaleMode.寄售;
                case ProductType.代运营产品:
                    return ERPSaleMode.代运营;
                case ProductType.入驻:
                    return ERPSaleMode.入驻;
                case ProductType.总部寄售:
                    return ERPSaleMode.总部寄售;
                case ProductType.合作商产品:
                    return ERPSaleMode.合作;
                case ProductType.独家包销:
                    return ERPSaleMode.独家包销;
                default:
                    return ERPSaleMode.无;
            }
        }

    }
}
