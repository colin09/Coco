using ERP.Domain.Enums.PurchaseInStockModule;
using ERP.Domain.Enums.StockBillModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.AllocationNoteModule
{
    /// <summary>
    /// 调拨业务类型
    /// </summary>
    public enum AllocationBusinessType
    {
        城际调拨 = 0,
        统采调拨 = 1,
        大区联采 = 2,
    }

    public static class AllocationBusinessTypeExtensions
    {
        public static SaleBusinessType ToSaleBusinessType(this AllocationBusinessType type)
        {
            switch (type)
            {
                case AllocationBusinessType.城际调拨:
                    return SaleBusinessType.城际调拨;
                case AllocationBusinessType.大区联采:
                    return SaleBusinessType.大区联采;
                case AllocationBusinessType.统采调拨:
                    return SaleBusinessType.总部统采;
                default:
                    return SaleBusinessType.城市销售;
            }
        }

        public static PurchaseBusinessType ToPurchaseBusinessType(this AllocationBusinessType type)
        {
            switch (type)
            {
                case AllocationBusinessType.城际调拨:
                    return PurchaseBusinessType.城际调拨;
                case AllocationBusinessType.大区联采:
                    return PurchaseBusinessType.大区联采;
                case AllocationBusinessType.统采调拨:
                    return PurchaseBusinessType.总部统采;
                default:
                    return PurchaseBusinessType.城市采购;
            }
        }
    }
}
