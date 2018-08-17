using ERP.Domain.Model.OrgModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.ReplenishmentRequisitionModule
{
    public class ReplenishmentRequisition : BaseEntity, IAggregationRoot
    {
        /// <summary>
        /// 申请编号
        /// </summary>
        [MaxLength(64)]
        public string NoteNo { get; set; }

        /// <summary>
        /// 申请时间
        /// </summary>
        public DateTime BusinessTime { get; set; }

        /// <summary>
        /// 申请状态
        /// </summary>
        public ReplenishmentRequisitionState ReplenishmentRequisitionState { get; set; }

        /// <summary>
        /// 取消原因
        /// </summary>
        [MaxLength(256)]
        public string CancelReason { get; set; }

        #region 聚合属性

        [ForeignKey("City")]
        [MaxLength(64)]
        public string City_Id { get; set; }

        /// <summary>
        /// 申请城市
        /// </summary>
        public City City { get; set; }
        [ForeignKey("User")]
        [MaxLength(64)]
        public string User_Id { get; set; }
        /// <summary>
        /// 申请用户
        /// </summary>
        public OrgUser User { get; set; }

        public List<ReplenishmentRequisitionItem> Items { get; set; }

        #endregion
    }
}
