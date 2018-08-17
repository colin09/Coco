using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP.Common.Infrastructure.Dapper.QueryObject
{
    public interface ICustomSearch
    {
        Query CustomQuery { get; set; }
    }
}
