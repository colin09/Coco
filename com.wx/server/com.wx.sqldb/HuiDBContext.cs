using com.wx.sqldb.data;
using com.wx.sqldb.mappings;
using System.Data.Common;
using System.Data.Entity;

namespace com.wx.sqldb
{
    /// <summary>
    /// 默认情况下，Code First会在连接字符串的配置节中寻找与你自定的DbContext的子类同名的连接字符串。
    /// </summary>
    public class HuiDBContext : DbContext
    {

        #region  --  Construct  --


        //同默认使用的连接字符串
        private static string dbName = "HuiDBContext";


        /// <summary>
        /// 
        /// </summary>
        public HuiDBContext()
            : base(dbName)
        {
            //System.Data.Entity.Database.SetInitializer<HuiDBContext>(new DropCreateDatabaseIfModelChanges<HuiDBContext>());
            System.Data.Entity.Database.SetInitializer<HuiDBContext>(new CreateDatabaseIfNotExists<HuiDBContext>());
            //System.Data.Entity.Database.SetInitializer<HuiDBContext>(new DropCreateDatabaseAlways<HuiDBContext>());
        }

        /// <summary>
        /// Construct a new context instance using the given string as the name or connection string 
        /// for the database to whitch connection will be used.
        /// </summary>
        /// <param name="dbName">nameOrConnectionString</param>
        public HuiDBContext(string dbName)
            : base(dbName)
        {
        }

        /// <summary>
        /// 如果contextOwnsContext属性为true，那么当DbContext子类Dispose的时候，就会把共用的DbConnection实例也Dispose掉。
        /// 如果这个值是false，则数据库连接实例就需要由程序员自己写程序释放。
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="contextOwnsConnection"></param>
        public HuiDBContext(DbConnection connection, bool contextOwnsConnection)
            : base(connection, contextOwnsConnection)
        {

        }

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            modelBuilder.Configurations.Add(new HUserMap());
            modelBuilder.Configurations.Add(new HRoleMap());
            modelBuilder.Configurations.Add(new HStoreMap());
            modelBuilder.Configurations.Add(new HResourceMap());
            modelBuilder.Configurations.Add(new HRecommendRelationMap());

            modelBuilder.Configurations.Add(new PositionConfigMap());
            modelBuilder.Configurations.Add(new PositionItemMap());

            //modelBuilder.Configurations.Add(new HUserCountMap());
            //modelBuilder.Configurations.Add(new HSalerIncomeMap());
            //modelBuilder.Configurations.Add(new HSalerIncomeHistoryMap());
            //modelBuilder.Configurations.Add(new HSalerIncomeCashApplyMap());
            //modelBuilder.Configurations.Add(new HSalerIncomeCashTransferMap());
            modelBuilder.Configurations.Add(new HOutsiteUserMap());


        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<RoleEntity> Roles { get; set; }
        public DbSet<StoreEntity> Stores { get; set; }
        public DbSet<RecommendRelationEntity> RecommendRelation { get; set; }
        public DbSet<OutsiteUserEntity> OutsiteUser { get; set; }
        public DbSet<ResourceEntity> Resource { get; set; }
        public DbSet<PositionConfigEntity> PositionConfig { get; set; }
        public DbSet<PositionItemEntity> PositionItem { get; set; }


        //public DbSet<UserCountEntity> UserCount { get; set; }
        //public DbSet<SalerIncomeEntity> SalerIncome { get; set; }
        //public DbSet<SalerIncomeHistoryEntity> SalerIncomeHistory { get; set; }
        //public DbSet<SalerIncomeCashApplyEntity> SalerIncomeCashApply { get; set; }
        //public DbSet<SalerIncomeCashTransferEntity> SalerIncomeCashTransfer { get; set; }


    }
}
