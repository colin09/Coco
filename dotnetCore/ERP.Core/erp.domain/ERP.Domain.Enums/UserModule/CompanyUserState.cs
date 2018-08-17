using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.UserModule
{
    /// <summary>
    /// 企业会员状态
    /// </summary>
    public enum CompanyUserState
    {
        未审核 = 1,
        审核失败 = 2,
        启用 = 3,
        冻结 = 4,
    }
}
