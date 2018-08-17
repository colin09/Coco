using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Domain.Model.PurchaseRequisitionModule
{
    public class RebatePolicyInfo
    {
        /// <summary>
        /// 基数
        /// </summary>
        public string BaseNum { get; set; }

        /// <summary>
        /// 返利数
        /// </summary>
        public string RebateNum { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Desc { get; set; }
    }
}
