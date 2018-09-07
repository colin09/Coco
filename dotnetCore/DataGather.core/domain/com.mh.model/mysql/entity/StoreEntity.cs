using System;
using System.ComponentModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

using com.mh.model.enums;


namespace com.mh.model.mysql.entity
{
    public partial class StoreEntity : BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Tel { get; set; }

        public int CreatedUser { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public int Group_Id { get; set; }
        //public DataStatus Status { get; set; }
        public Nullable<decimal> GpsLat { get; set; }
        public Nullable<decimal> GpsLng { get; set; }
        public Nullable<decimal> GpsAlt { get; set; }
        //public Nullable<int> ExStoreId { get; set; }
        public string RMAAddress { get; set; }
        public string RMAZipCode { get; set; }
        public string RMAPerson { get; set; }
        public string RMAPhone { get; set; }
        //public Nullable<int> IsOnLine { get; set; }
        public StorePaymentType PaymentMethodType { get; set; }

        public StorePaymentForBaseAccount PaymentForBaseAccount { get; set; }

        public TimeSpan OpenDoorTime { get; set; }
        public TimeSpan CloseDoorTime { get; set; }

        public string ServiceDesc { get; set; }

        /// <summary>
        /// 是否开启自动如收银
        /// </summary>
        public int IsAutoCash { get; set; }

        /// <summary>
        /// 是否开启自动退款
        /// </summary>
        public int IsAutoRefunds { get; set; }

        /// <summary>
        /// 自动入收银类型
        /// </summary>
        public AutoCashStoreType AutoCashStoreType { get; set; }

        /// <summary>
        /// 门店编码
        /// </summary>
        public string ShopNo { get; set; }

        /// <summary>
        /// 门店渠道编码
        /// </summary>
        public string VTWEG { get; set; }

        /// <summary>
        /// 省Id
        /// </summary>
        public int ProvinceId { get; set; }

        /// <summary>
        /// 市Id
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// 区Id
        /// </summary>
        public int DistrictId { get; set; }

        /// <summary>
        /// 省名称
        /// </summary>
        public string ProvinceName { get; set; }

        /// <summary>
        /// 市名称
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// 区名称
        /// </summary>
        public string DistrictName { get; set; }

        /// <summary>
        /// 门店类型
        /// </summary>
        public UserLevel UserLevel { get; set; }


        /// <summary>
        /// 门店添加logo字段
        /// </summary>
        public string Logo { get; set; }

        /// <summary>
        /// 门店背景图
        /// </summary>
        public string BgLogo { get; set; }


        /// <summary>
        /// 门店排序值  用于显示门店在首页的显示顺序   正序排序  1 排在最前面
        /// </summary>
        public int SortOrder { get; set; }

        #region Overrides of BaseEntity
        /// <summary>
        /// KeyMemberId
        /// </summary>


        #endregion




        public static void BuildMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreEntity>().ToTable("Store");
            modelBuilder.Entity<StoreEntity>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.HasKey(e => e.Id);
                
                entity.Property(e => e.CreateDate).HasColumnName("CreatedDate");
                entity.Property(e => e.CreateUser).HasColumnName("CreatedUser");
                entity.Property(e => e.UpdateDate).HasColumnName("UpdatedDate");
                entity.Property(e => e.UpdateUser).HasColumnName("UpdatedUser");
            });
        }



        public static EntityTypeBuilder<StoreEntity> buildAction(EntityTypeBuilder<StoreEntity> entity)
        {
            // Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<StoreEntity> entity
            // var entity = new EntityTypeBuilder<StoreEntity>();
            //entity.ToTable("Store");

            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
            entity.Ignore("CreateUser");
            entity.Ignore("CreateDate");
            entity.Ignore("UpdateUser");
            entity.Ignore("UpdateDate");
            entity.Property("CreateUser").HasColumnName("CreatedUser");
            entity.Property(e => e.CreateDate).HasColumnName("CreatedDate");
            entity.Property(e => e.CreateUser).HasColumnName("CreatedUser");
            entity.Property(e => e.UpdateDate).HasColumnName("UpdatedDate");
            entity.Property(e => e.UpdateUser).HasColumnName("UpdatedUser");

            return entity;
        }


    }


    public enum StorePaymentType
    {
        /// <summary>
        /// 门店自己收
        /// </summary>
        PaymentByStore = 0,

        /// <summary>
        /// 悦吧代收
        /// </summary>
        PaymentByShopping = 1,
    }

    public enum StorePaymentForBaseAccount
    {
        /// <summary>
        /// 悦吧代收时，帐入其分账户
        /// </summary>
        PaymentBySelfAccount = 0,

        /// <summary>
        /// 悦吧代收时，其帐入Shopping账户
        /// </summary>
        PaymentByShoppingAccount = 1,
    }

    public enum AutoCashStoreType
    {

        /// <summary>
        /// 默认类型
        /// </summary>
        [Description("不设置自动入收银")]
        Void = 0,
        /// <summary>
        /// 王府井自动入收银
        /// </summary>
        [Description("王府井自动入收银")]
        WangFuJing = 1,
        /// <summary>
        /// 金鹰自动入收银
        /// </summary>
        [Description("金鹰自动入收银")]
        JinYing = 2,
        /// <summary>
        /// 金华自动入收银
        /// </summary>
        [Description("金华自动入收银")]
        JinHua = 3
    }

    [Flags]
    public enum UserLevel
    {
        /// <summary>
        /// 啥都不是 - 0
        /// </summary>
        [Description("默认")]
        None = 0,

        /// <summary>
        /// 普通用户 - 1
        /// </summary>
        [Description("普通用户")]
        User = 1,

        /// <summary>
        /// 达人 - 2 
        /// </summary>
        [Description("达人")]
        Daren = 2,

        /// <summary>
        /// 专柜买手 - 4
        /// </summary>
        [Description("导购")]
        DaoGou = 4,

        /// <summary>
        /// 认证买手 - 8
        /// </summary>
        [Description("认证买手")]
        RenZhengDaoGou = 8,

        /// <summary>
        /// 品牌买手 - 16
        /// </summary>
        [Description("品牌商买手")]
        BrandDaoGou = 16,


        [Description("管理人员")]
        Manager = 1024,

    }
}