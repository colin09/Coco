using ERP.Domain.Model.CommonModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    /// <summary>
    /// 采购申请单检查信息（序列化后存储）
    /// </summary>
   
    public class PurchaseRequisitionCheckInfo : IValueObject
    {
        /// <summary>
        /// 检查异常备注
        /// </summary>
        public string Remark { get; set; }
        
        /// <summary>
        /// 检查项
        /// </summary>
        public List<PurchaseRequisitionCheckInfoRole> CheckRoles { get; set; }
    }
}
