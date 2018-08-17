using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    public interface IHasCreateRelatedNote
    {
        /// <summary>
        /// 是否已创建关联单据
        /// </summary>
        bool HasCreateRelatedNote { get; set; }
    }
}
