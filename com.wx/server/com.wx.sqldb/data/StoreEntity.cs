using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class StoreEntity : BaseEntity
    {
        public string Name { set; get; }
        public string Address { set; get; }
        public string Contact { set; get; }
        public string Mobile { set; get; }
        public string EMail { set; get; }
        public string Logo { set; get; }
        public string Description { set; get; }
    }
}
