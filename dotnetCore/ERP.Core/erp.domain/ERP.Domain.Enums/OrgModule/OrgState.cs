using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrgModule
{
    /// <summary>
    /// 组织机构状态
    /// </summary>
    public enum OrgState
    {
        申请增加 = 0,

        申请修改 = 1,

        //申请删除 = 2,

        审核通过 = 3,

        审核不通过 = 4,

        //已删除 = 4 

        待审核 = 5,

        已审核 = 6,
    }
}
