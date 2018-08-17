using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Enums.CommonModule;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class NotSkuGift : IValueObject
    {
        public NotSkuGift()
        {
            Id = Guid.NewGuid().ToString("N");
            AuditState = CommonAuditState.待审核;
        }
        public string Id { get; set; }

        public CommonAuditState AuditState { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public int Count { get; set; }

        public int InStockCount { get; set; }
        public string Remark { get; set; }

        public List<string> RelatedProductIds { get; set; }
    }
}
