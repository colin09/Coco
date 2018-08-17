using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Model
{
    public interface IHidden
    {
        bool IsHidden { get; set; }
    }
}
