using ERP.Domain.Enums.OrgModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.CommonModule
{
    public class UserRoleInfo : IValueObject
    {
        /// <summary>
        /// 系统
        /// </summary>
        public static UserRoleInfo System = new UserRoleInfo(1, "System", "系统");

        /// <summary>
        /// 仓库管理员
        /// </summary>
        public static UserRoleInfo StoreAdmin = new UserRoleInfo(5, OrgUserAuthType.StoreAdmin.ToString(), "仓库管理员");
        /// <summary>
        /// 出纳
        /// </summary>
        public static UserRoleInfo Cashier = new UserRoleInfo(6, OrgUserAuthType.Cashier.ToString(), "出纳");
        /// <summary>
        /// 采购经理(采购员)
        /// </summary>
        public static UserRoleInfo PurchasingManager = new UserRoleInfo(10, OrgUserAuthType.PurchasingManager.ToString(), "采购经理");
        /// <summary>
        /// 会计
        /// </summary>
        public static UserRoleInfo Accountant = new UserRoleInfo(15, OrgUserAuthType.Accountant.ToString(), "财务会计");
        /// <summary>
        /// 大区采购专员
        /// </summary>
        public static UserRoleInfo RegionalPurchaseStaff = new UserRoleInfo(17, OrgUserAuthType.RegionalPurchaseStaff.ToString(), "大区采购专员");
        /// <summary>
        /// 大区采购审核专员(大区审核员)
        /// </summary>
        public static UserRoleInfo RegionalPurchaseAndExamineStaff = new UserRoleInfo(20, OrgUserAuthType.RegionalPurchaseAndExamineStaff.ToString(), "大区采购审核专员");
        /// <summary>
        /// 大区采购经理
        /// </summary>
        public static UserRoleInfo RegionalPurchaseManager = new UserRoleInfo(30, OrgUserAuthType.RegionalPurchaseManager.ToString(), "大区采购经理");
        /// <summary>
        /// 大区经理(大区总监)
        /// </summary>
        public static UserRoleInfo RegionalManager = new UserRoleInfo(40, OrgUserAuthType.RegionalManager.ToString(), "大区经理");
        /// <summary>
        /// coo
        /// </summary>
        public static UserRoleInfo COO = new UserRoleInfo(99, OrgUserAuthType.COO.ToString(), "COO");
        /// <summary>
        /// ceo
        /// </summary>
        public static UserRoleInfo CEO = new UserRoleInfo(100, OrgUserAuthType.CEO.ToString(), "CEO");

        /// <summary>
        /// 一级审核（采购审核用）
        /// </summary>
        public static UserRoleInfo FirstAuditUser = new UserRoleInfo(100001, OrgUserAuthType.FirstAuditUser.ToString(), "一级审核");
        /// <summary>
        /// 二级审核（采购审核用）
        /// </summary>
        public static UserRoleInfo SecondAuditUser = new UserRoleInfo(100021, OrgUserAuthType.SecondAuditUser.ToString(), "二级审核");
        /// <summary>
        /// 城市经理
        /// </summary>
        public static UserRoleInfo CityManager = new UserRoleInfo(100000, OrgUserAuthType.CityManager.ToString(), "城市经理");

        public static UserRoleInfo GetUserRoleInfo(OrgUserAuthType? userAuthType)
        {
            switch (userAuthType)
            {
                case OrgUserAuthType.CEO:
                    return CEO;
                case OrgUserAuthType.COO:
                    return COO;
                case OrgUserAuthType.RegionalManager:
                    return RegionalManager;
                case OrgUserAuthType.RegionalPurchaseManager:
                    return RegionalPurchaseManager;
                case OrgUserAuthType.RegionalPurchaseAndExamineStaff:
                    return RegionalPurchaseAndExamineStaff;
                case OrgUserAuthType.PurchasingManager:
                    return PurchasingManager;
                case OrgUserAuthType.RegionalPurchaseStaff:
                    return RegionalPurchaseStaff;
                case OrgUserAuthType.FirstAuditUser:
                    return FirstAuditUser;
                case OrgUserAuthType.SecondAuditUser:
                    return SecondAuditUser;
                case OrgUserAuthType.Cashier:
                    return Cashier;
                case OrgUserAuthType.CityManager:
                    return CityManager;
                default:
                    return System;
            }
        }

        public UserRoleInfo() { }
        public UserRoleInfo(int level, string userRole, string userRoleName)
        {
            this.Level = level;
            this.UserRole = userRole;
            this.UserRoleName = userRoleName;
        }

        /// <summary>
        /// 级别（越大级别越高）
        /// </summary>
        public int Level { get; set; }

        /// <summary>
        /// 用户角色代码
        /// </summary>
        [MaxLength(64)]
        public string UserRole { get; set; }

        /// <summary>
        /// 用户角色名称
        /// </summary>
        [MaxLength(64)]
        public string UserRoleName { get; set; }

        public static UserRoleInfo GetUserRoleInfo(string userRole)
        {
            if (userRole == UserRoleInfo.System.UserRole) return UserRoleInfo.System;

            OrgUserAuthType auth = (OrgUserAuthType)Enum.Parse(typeof(OrgUserAuthType), userRole);
            switch (auth)
            {
                case OrgUserAuthType.PurchasingManager:
                    return UserRoleInfo.PurchasingManager;
                case OrgUserAuthType.RegionalManager:
                    return UserRoleInfo.RegionalManager;
                case OrgUserAuthType.RegionalPurchaseAndExamineStaff:
                    return UserRoleInfo.RegionalPurchaseAndExamineStaff;
                case OrgUserAuthType.RegionalPurchaseManager:
                    return UserRoleInfo.RegionalPurchaseManager;
                case OrgUserAuthType.COO:
                    return UserRoleInfo.COO;
                case OrgUserAuthType.CEO:
                    return UserRoleInfo.CEO;
                case OrgUserAuthType.RegionalPurchaseStaff:
                    return UserRoleInfo.RegionalPurchaseStaff;
                case OrgUserAuthType.FirstAuditUser:
                    return FirstAuditUser;
                case OrgUserAuthType.SecondAuditUser:
                    return SecondAuditUser;
                case OrgUserAuthType.CityManager:
                    return CityManager;
                default:
                    return UserRoleInfo.PurchasingManager;
            }
        }
    }
}
