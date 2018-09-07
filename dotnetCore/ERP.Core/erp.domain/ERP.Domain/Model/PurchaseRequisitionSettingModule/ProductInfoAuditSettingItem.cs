using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.PurchaseRequisitionSettingModule
{
    public class ProductInfoAuditSettingItem : BaseEntity
    {
        [MaxLength(64)]
        [ForeignKey("ProductInfo")]
        public string ProductInfo_Id { get; set; }
        [MaxLength(64)]
        [ForeignKey("AuditSetting")]
        public string AuditSetting_Id { get; set; }
        public ProductInfo ProductInfo { get; set; }

        public PurchaseRequisitionGlobalAuditSetting AuditSetting { get; set; }
    }
}
