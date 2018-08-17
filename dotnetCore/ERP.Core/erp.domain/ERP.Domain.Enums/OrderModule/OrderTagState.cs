using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    public enum OrderTagState
    {
        无标记 = 0,
        部分配送 = 1,
        配送失败 = 2,
    }
}
