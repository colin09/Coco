using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.data
{
    public class UserEntity : BaseEntity
    {
        public UserLevel UserLever { set; get; }
        public string Name { set; get; }
        public string NickName { set; get; }
        public string Password { set; get; }

        public string Mobile { set; get; }
        public string EMail { set; get; }
        public string Logo { set; get; }
        public int Gender { set; get; }
        public string Description { set; get; }

        public string Country { set; get; }
        public string Province { set; get; }
        public string City { set; get; }
    }





   public enum UserLevel
    {
        Manager=0,
        Store=600,
        Customer=900
    }


}
