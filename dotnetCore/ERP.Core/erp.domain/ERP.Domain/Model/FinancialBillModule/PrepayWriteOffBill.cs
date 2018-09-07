using ERP.Common.Infrastructure.Exceptions;
using ERP.Domain.Context;
using ERP.Domain.Model.CommonModule;
using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;



namespace ERP.Domain.Model.FinancialBillModule
{
    /// <summary>
    /// 预付应付核销单据
    /// </summary>
    public class PrepayWriteOffBill : BaseEntity, IAggregationRoot, IDeleted
    {
        [MaxLength(64)]
        public string BillNo { get; set; }

        /// <summary>
        /// 核销总金额
        /// </summary>
        public decimal WriteOffAmount { get; set; }

        public bool IsInternal { get; set; }

        public bool IsDeleted { get; set; }

        #region 聚合属性

        /// <summary>
        /// 核销预付单明细
        /// </summary>
        public List<WriteOffPrepayBillItem> PrepayBillItems { get; set; }

        /// <summary>
        /// 核销应付单明细
        /// </summary>
        public List<WriteOffPayableBillItem> PayableBillItems { get; set; }
        [MaxLength(64)]
        [ForeignKey("Provider")]
        public string Provider_Id { get; set; }
        /// <summary>
        /// 供应商
        /// </summary>
        public Provider Provider { get; set; }
        [MaxLength(64)]
        [ForeignKey("SupplierCity")]
        public string SupplierCity_Id { get; set; }
        public Org SupplierCity { get; set; }
        [MaxLength(64)]
        [ForeignKey("City")]
        public string City_Id { get; set; }
        public City City { get; set; }

        #endregion

        #region 业务方法

        public void UnWriteOff()
        {
            this.PayableBillItems.ForEach(i => i.UnWriteOff());
            this.PrepayBillItems.ForEach(i => i.UnWriteOff());
        }

        #endregion
    }
}
