using ERP.Domain.Model.AllocationNoteModule;
using ERP.Domain.Model.ProductModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.ReplenishmentRequisitionModule
{
    public class ReplenishmentRequisitionItem : BaseEntity, IAggregationRoot
    {
        public ReplenishmentRequisitionItem()
        {
            this.ReplenishmentState = ReplenishmentRequisitionModule.ReplenishmentState.待补货;
        }
        /// <summary>
        /// 补货状态
        /// </summary>
        public ReplenishmentState ReplenishmentState { get; set; }

        /// <summary>
        /// 申请补货数量
        /// </summary>
        public int ReplenishmenRequisitiontNum { get; set; }

        /// <summary>
        /// 调拨数量
        /// </summary>
        public int AllocationNum { get; set; }
        #region 聚合属性

        [ForeignKey("ReplenishmentRequisition")]
        [MaxLength(64)]
        public string ReplenishmentRequisition_Id { get; set; }

        /// <summary>
        /// 补货申请单
        /// </summary>
        public ReplenishmentRequisition ReplenishmentRequisition { get; set; }

        [ForeignKey("AllocationNote")]
        [MaxLength(64)]
        public string AllocationNote_Id { get; set; }
        /// <summary>
        /// 调拨单
        /// </summary>
        public AllocationNote AllocationNote { get; set; }

        [ForeignKey("Product")]
        [MaxLength(64)]
        public string Product_Id { get; set; }
        /// <summary>
        /// 产品
        /// </summary>
        public Product Product { get; set; }

        #endregion
    }
}
