using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    /// <summary>
    /// 实体，有主键的对象
    /// </summary>
    public interface IEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        string Id { get; set; }
    }
}
