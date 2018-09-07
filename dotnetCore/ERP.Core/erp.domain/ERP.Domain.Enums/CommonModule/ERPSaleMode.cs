using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Enums.CommonModule
{
    public enum ERPSaleMode
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

        总部代理 = 98,
        区域代理 = 99,
    }
}
