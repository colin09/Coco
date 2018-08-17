using ERP.Domain.Model.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class PurchaseRequisitionCheckInfoRole : IValueObject
    {
        public PurchaseRequisitionCheckInfoRole() { }

        public PurchaseRequisitionCheckInfoRole(PurchaseRequisitionCheckInfoRole role)
        {
            this.UserRole = new UserRoleInfo(role.UserRole.Level,role.UserRole.UserRole,role.UserRole.UserRoleName);
            this.CheckItems =role.CheckItems.Select(i=>new PurchaseRequisitionCheckInfoItem(i)).OrderBy(o=>o.SequenceNo).ToList();

        }
        /// <summary>
        /// 审核的人员角色
        /// </summary>
        public UserRoleInfo UserRole { get; set; }

        /// <summary>
        /// 检查项
        /// </summary>
        public List<PurchaseRequisitionCheckInfoItem> CheckItems { get; set; }
    }
}
