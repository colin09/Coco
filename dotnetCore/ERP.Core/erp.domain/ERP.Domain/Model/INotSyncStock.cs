using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    public interface INotSyncStock
    {
        bool IsNotSyncStock { get; set; }
    }
}
