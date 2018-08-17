using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERP.Domain.Utility
{
    public class IdCreator
    {
        public static string CreateIdByInt()
        {
            return Guid.NewGuid().ToString("N");
        }
    }
}
