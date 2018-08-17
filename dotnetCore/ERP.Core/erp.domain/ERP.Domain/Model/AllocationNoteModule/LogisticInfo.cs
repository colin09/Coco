using ERP.Domain.Enums.AllocationNoteModule;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.AllocationNoteModule
{
    public class LogisticInfo : IValueObject
    {
        /// <summary>
        /// 物流方式
        /// </summary>
        public LogisticMode LogisticMode { get; set; }

        /// <summary>
        /// 物流费
        /// </summary>
        public decimal LogisticsFee { get; set; }

        /// <summary>
        /// 物料公司
        /// </summary>
        [MaxLength(255)]
        public string LogisticCompany { get; set; }

        /// <summary>
        /// 物流单号
        /// </summary>
        [MaxLength(64)]
        public string LogisticNo { get; set; }

        [MaxLength(255)]
        public string Remark { get; set; }
    }
}
