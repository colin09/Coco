using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Enums.OrgModule
{
    /// <summary>
    /// 组织机构用户类型
    /// </summary>
    public enum OrgUserType
    {
        /// <summary>
        /// 运行商管理员
        /// </summary>
        OPAdmin = 1,

        /// <summary>
        /// 运营商用户
        /// </summary>
        OPUser = 2,

        /// <summary>
        /// 城市管理员
        /// </summary>
        CityAdmin = 3,

        /// <summary>
        /// 处理中心用户
        /// </summary>
        PCUser = 4,

        /// <summary>
        /// 仓库管理员
        /// </summary>
        StoreAdmin = 5,

        /// <summary>
        /// 配送员
        /// </summary>
        DeliveryUser = 6,

        /// <summary>
        /// 经销商管理员
        /// </summary>
        AGAdmin = 8,

        /// <summary>
        ///运营中心客服
        /// </summary>
        CSStaff = 9,

        /// <summary>
        /// 经纪人用户
        /// </summary>
        BrokerUser = 10,

        /// <summary>
        /// 综合管理员
        /// </summary>
        GeneralAdmin = 100,

        /// <summary>
        /// 生意经管理员
        /// </summary>
        ArticleAdmin = 11,

        /// <summary>
        /// 只看财务报表
        /// </summary>
        ReportAdmin = 12,

        /// <summary>
        /// 上报信息管理员
        /// </summary>
        ReportMessageAdmin = 13,

        /// <summary>
        /// 配送商管理员
        /// </summary>
        SupplierAdmin = 14,

        /// <summary>
        /// 合作商的分销商
        /// </summary>
        DistributorsAdmin = 15,

        /// <summary>
        /// 人力资源
        /// </summary>
        HRUser = 16,

        /// <summary>
        /// 报表查询角色
        /// </summary>
        ReportManager = 17,
        /// <summary>
        /// 寄售公司
        /// </summary>
        ConsignmentAdmin = 18,

        /// <summary>
        /// 城市经理
        /// </summary>
        CityDirector = 19,
        /// <summary>
        /// 财务出纳
        /// </summary>
        Finance = 20,
        /// <summary>
        /// 会计
        /// </summary>
        Accounting = 21,
        /// <summary>
        /// 技术人员
        /// </summary>
        ITAdmin = 22,

        /// <summary>
        /// 总部自运营人员
        /// </summary>
        OwnAdmin = 23,

        /// <summary>
        /// 仓库总账号
        /// </summary>
        OPStoreUser = 24,

        /// <summary>
        /// 区域经理
        /// </summary>
        AreaAdmin = 25,
        /// <summary>
        /// ERP管理员
        /// </summary>
        ERPAdmin = 1001,
        /// <summary>
        /// ERP总部仓管
        /// </summary>
        ERPStoreAdmin = 1002,

        /// <summary>
        /// ERP总部采购
        /// </summary>
        ERPPurchaser = 1003,
        /// <summary>
        /// 技术支持
        /// </summary>
        ERPSupport = 10000,

        /// <summary>
        /// 大区采购审核员
        /// </summary>
        RegionalPurchaseAndExamineStaff = 900,
    }
}
