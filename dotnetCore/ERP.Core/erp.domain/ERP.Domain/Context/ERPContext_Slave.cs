
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ERP.Common.Infrastructure.Data;

namespace ERP.Domain.Context
{
    /// <summary>
    /// ERP从库
    /// </summary>
    public class ERPContext_Slave : ContextBase, IERPSlaveContext
    {
        protected override System.Data.IDbConnection CreateConnection()
        {
            return DbConnectionFactory.CreateDbQueryConnection();
        }
    }
}
