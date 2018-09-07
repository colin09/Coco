using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrderModule
{
    public enum BigGoodsMode
    {
        非大批发货 = 0,
        不进仓自提 = 1,
        进仓自提 = 2,
        进仓送货 = 3,
        不进仓送货 = 4,
    }
}
