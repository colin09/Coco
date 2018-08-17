using System;
using ERP.Common.Infrastructure.Data;

namespace ERP.Domain.Context
{
    /// <summary>
    /// ERP主库
    /// </summary>
    public class ERPContext_Master : ContextBase, IERPContext
    {
        protected override System.Data.IDbConnection CreateConnection()
        {
            return DbConnectionFactory.CreateDbConnection();
        }
    }
}
