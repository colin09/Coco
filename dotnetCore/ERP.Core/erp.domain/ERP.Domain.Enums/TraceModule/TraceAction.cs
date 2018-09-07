using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.TraceModule
{
    public enum TraceAction
    {
        新增 = 0,
        修改 = 1,

        下推单据 = 2,
        下推退货单 = 21,

        审核 = 3,
        反审核 = 4,
        启用 = 5,
        停用 = 6,
        删除 = 7,
        成本核算 = 8,
        作废 = 9,

        结账 = 31,
        反结账 = 32,
        打印 = 33,
        录入 = 34,

        ERP上线 = 41,
        ERP下线 = 42,

        期初数据初始化 = 50,
        期初数据初始化结束 = 51,
        期初数据反初始化 = 52,

        强制上传图片 = 61,
        取消强制上传图片 = 62,
        启用采购申请 = 63,
        禁用采购申请 = 64,

        代销售单据补货 = 71,
        代销售单据补钱 = 72,
        代销售单据确认收货 = 73,
        代销售单据确认收款 = 74,

        易久采付款 = 81,

        恢复 = 99
    }
}
