using ERP.Domain.Enums.PurchaseRequisitionSettingModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.PurchaseRequisitionModule
{
    public enum SeparateBillType
    {
        普通 = 0,
        代理 = 1,
        产品 = 2,
        品牌 = 3,
        类目 = 4
    }

    public static class SeparateBillTypeExtensions
    {
        public static GlobalAuditSettingType? ToGlobalAuditSettingType(this SeparateBillType type)
        {
            switch (type)
            {
                case SeparateBillType.产品: return GlobalAuditSettingType.特定产品;
                case SeparateBillType.类目: return GlobalAuditSettingType.特定类目;
                case SeparateBillType.品牌: return GlobalAuditSettingType.特定品牌;
                default: return null;
            }
        }
    }
}
