using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
  public class RoleEntity : BaseEntity
    {
        public RoleEntity() { }


        public string Name { set; get; }
        public string Description { set; get; }
    }
}
