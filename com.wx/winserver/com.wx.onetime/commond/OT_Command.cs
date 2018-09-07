using CLAP;
using com.wx.common.helper;
using com.wx.sqldb;
using com.wx.sqldb.data;
using System.Linq;

namespace com.wx.onetime.commond
{
    partial class OT_Command
    {


        [Verb]
        public static void InitUser()
        {
            var db = new HuiDbSession();

            var m = db.PositionConfigRepository.Where(pc => true).FirstOrDefault();
            System.Console.WriteLine(m.ToJson());


            db.UserRepository.Create(new UserEntity
            {
                UserLever = UserLevel.Manager,
                Name = "Manager",
                NickName = "system.manager",
                Password = CryptoHelper.MD5Encrypt("123456"),
                Mobile = "18900001111",
                EMail = "",
                Logo = "",
                Gender = 0,
                Description = "system manager by init",
                Country = "cn",
                Province = "hb",
                City = "wh"
            });
            db.SaveChange();

        }




    }
}
