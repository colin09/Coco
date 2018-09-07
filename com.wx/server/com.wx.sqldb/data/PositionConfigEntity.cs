using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class PositionConfigEntity : BaseEntity
    {

        //public string Code { set; get; }
        public int ParentId { set; get; }
        public string Name { set; get; }
        public string EnName { set; get; }
        public string Desc { set; get; }
        public string Remark { set; get; }
        public string Type { set; get; }
        public int Deep { set; get; }
        public int Sort { set; get; }

    }
}
