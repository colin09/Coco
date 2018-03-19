using System;
using Microsoft.EntityFrameworkCore;

using com.mh.mysql.db;
using com.mh.common.extension;


namespace com.mh.mysql.factory
{
    public class DbContextFactory
    {
        public static DbContext GetCurrentDbContext(string dbName)
        {
            DbContext db = CallContext.GetData($"{dbName}_DbContext") as DbContext;
            if (db == null)
            {
                switch (dbName)
                {
                    case "Hui" :db = new HuiContext();break;
                    case "MagicHorse" :db = new MagicHorseContext();break;
                    default: db = new HuiContext(); break;
                }
                CallContext.SetData($"{dbName}_DbContext", db);
            }
            return db;
        }
    }
}