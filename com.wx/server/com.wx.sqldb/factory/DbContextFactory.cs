using System.Data.Entity;
using System.Runtime.Remoting.Messaging;

namespace com.wx.sqldb.factory
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext(string dbName)
        {
            DbContext db = CallContext.GetData(string.Format("{0}_{1}",dbName,"DbContext")) as DbContext;
            if (db == null)
            {
                switch (dbName)
                {
                    case "HuiEntity" :db = new HuiDBContext();break;
                    default: db = new HuiDBContext(); break;
                }
                CallContext.SetData(string.Format("{0}_{1}", dbName, "DbContext"), db);
            }
            return db;
        }
    }
}
