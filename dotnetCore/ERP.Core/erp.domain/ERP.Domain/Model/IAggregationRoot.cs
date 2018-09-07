using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    /// <summary>
    /// 聚合根对象
    /// 核心的业务对象都需要从这个对象继承
    /// 整个业务都是围绕聚合根对象来进行的
    /// </summary>
    public interface IAggregationRoot : IEntity
    {
    }
}
