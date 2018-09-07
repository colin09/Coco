using ERP.Domain.Enums.FinancialStockModule;
using ERP.Domain.Enums.TraceModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.StockBillModule
{
    /// <summary>
    /// 操作类型
    /// </summary>
    public enum NoteOperationType
    {
        无 = 0,
        库存盘点 = 4,
        日常破损 = 6,
        招待 = 7,
        福利 = 8,
        客情 = 9,

    }

    public static class NoteOperationTypeExtensions
    {
        public static FinancialStockType ToFinancialStockType(this NoteOperationType operationType, NoteType noteType)
        {
            switch (operationType)
            {
                case NoteOperationType.日常破损:
                    return FinancialStockType.破损出库;
                case NoteOperationType.库存盘点:
                    if (noteType == NoteType.出库单)
                        return FinancialStockType.盘亏单;
                    else
                        return FinancialStockType.盘盈单;
                case NoteOperationType.招待:
                    return FinancialStockType.招待;
                case NoteOperationType.客情:
                    return FinancialStockType.客情;
                case NoteOperationType.福利:
                    return FinancialStockType.福利;
                default:
                    return FinancialStockType.破损出库;
            }
        }

        public static TraceType ToTraceType(this NoteOperationType operationType, NoteType noteType)
        {
            if (noteType == NoteType.入库单)
                return TraceType.库存盘点单;

            switch (operationType)
            {
                case NoteOperationType.日常破损:
                    return TraceType.破损出库单;
                case NoteOperationType.库存盘点:
                    return TraceType.库存盘点单;
                case NoteOperationType.招待:
                    return TraceType.招待出库单;
                case NoteOperationType.客情:
                    return TraceType.客情出库单;
                case NoteOperationType.福利:
                    return TraceType.客情出库单;
                default:
                    return TraceType.破损出库单;
            }
        }

        public static string ToDisplayString(this NoteOperationType operationType, NoteType noteType)
        {
            switch (operationType)
            {
                case NoteOperationType.日常破损:
                    return "破损出库单";
                case NoteOperationType.库存盘点:
                    if (noteType == NoteType.出库单)
                        return "盘亏单";
                    else
                        return "盘盈单";
                default:
                    return operationType.ToString();
            }
        }

    }
}
