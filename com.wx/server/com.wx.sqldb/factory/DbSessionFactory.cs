using System.Runtime.Remoting.Messaging;

namespace com.wx.sqldb.factory
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbContext(string dbName)
        {
            IDbSession dbSession = CallContext.GetData(string.Format("{0}_{1}",dbName,"DbSession")) as IDbSession;
            if (dbSession == null)
            {
                switch (dbName)
                {
                    case "HuiEntity": dbSession = new HuiDbSession(); break;
                    default: dbSession = new HuiDbSession(); break;
                }
                CallContext.SetData(string.Format("{0}_{1}",dbName,"DbSession"), dbSession);
                return dbSession;
            }
            return dbSession;
        }
    }
}
