using System;

using com.mh.mysql.repository.dbSession;
using com.mh.common.extension;
using com.mh.mysql.iservice;

namespace com.mh.mysql.factory
{
    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbSession(string dbName)
        {
            IDbSession dbSession = CallContext.GetData($"{dbName}_DbSession") as IDbSession;
            if (dbSession == null)
            {
                switch (dbName)
                {
                    case "Hui": dbSession = new HuiSession(); break;
                    case "MagicHorse": dbSession = new MagicHorseSession(); break;
                    default: dbSession = new HuiSession(); break;
                }
                CallContext.SetData($"{dbName}_DbSession", dbSession);
                return dbSession;
            }
            return dbSession;
        }
    }
}