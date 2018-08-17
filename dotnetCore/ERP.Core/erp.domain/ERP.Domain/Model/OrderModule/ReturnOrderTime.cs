using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model.OrderModule
{
    /// <summary>
    /// 退货单时间
    /// </summary>
    public class ReturnOrderTime : IValueObject
    {
        /// <summary>
        /// 退货单创建时间
        /// </summary>
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 退货单完成时间
        /// </summary>
        public DateTime? CompleteTime { get; set; }
    }
}
