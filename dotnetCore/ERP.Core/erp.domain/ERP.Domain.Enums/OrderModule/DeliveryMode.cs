using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    public enum DeliveryMode
    {
        酒批配送 = 0, 合作商配送 = 1, 配送商配送 = 2,
        第三方配送 = 3, 客户自提 = 4, 总部物流 = 5, 区域代配送 = 6
    }
}
