using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrgModule
{
    public enum OrgUserAuthType
    {
        OPAdmin = 0,
        OPUser = 1,
        ExecutiveOfficer = 2,//公司高层
        CEO = 3,
        COO = 4,
        /// <summary>
        /// 总部审核员
        /// </summary>
        HeadPurchaseStaff = 5,
        /// <summary>
        /// 大区经理
        /// </summary>
        RegionalManager = 6,
        /// <summary>
        /// 大区采购经理
        /// </summary>
        RegionalPurchaseManager = 7,
        RegionalPurchaseStaff = 8,//大区采购专员
        /// <summary>
        /// 大区采购审核专员
        /// </summary>
        RegionalPurchaseAndExamineStaff = 9,
        CityAdmin = 10,//城市管理员
        JoinCityAdmin = 11,//加盟城市管理员
        CityManager = 12, //城市经理
        ProvinceGeneral = 13,//省区总监
        /// <summary>
        /// 采购经理
        /// </summary>
        PurchasingManager = 14,
        /// <summary>
        /// 经纪人
        /// </summary>
        SaleUser = 15,
        DeliveryUser = 16, //配送员
        Cashier = 17,//   出纳人员
        /// <summary>
        /// 仓库管理员
        /// </summary>
        StoreAdmin = 18,
        Stevedore = 19,//装卸人员
        Developer = 20,//研发人员
        /// <summary>
        /// 会计人员
        /// </summary>
        Accountant = 21,
        HR = 22,//总部HR
        PartnerSaleUser = 23,//大商经纪人
        InvestorAdmin = 24,//投资人
        AllocateUser = 25,//调拨系统管理员
        CityAllocateUser = 26,//城市调拨人员
        /// <summary>
        /// 总部财务
        /// </summary>
        HQFinance = 27,
        AllocateConsignor = 28,//调拨发货人员

        /// <summary>
        /// ERP管理员
        /// </summary>
        ERPAdmin = 1000,
        /// <summary>
        /// 技术支持
        /// </summary>
        ERPSupport = 1001,
        /// <summary>
        /// ERP总部仓管
        /// </summary>
        ERPStoreAdmin = 1002,
        /// <summary>
        /// ERP总部采购
        /// </summary>
        ERPPurchaser = 1003,

        /// <summary>
        /// 一级审核(采购审核用）
        /// </summary>
        FirstAuditUser = 100001,
        /// <summary>
        /// 二级审核（采购审核用）
        /// </summary>
        SecondAuditUser = 100021
    }
}
