using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace com.mh.model.mysql.entity
{
    public partial class IMS_OperatorEntity : BaseEntity
    {
        /// <summary>
        /// 管理专柜编码
        /// </summary>
        public string ManagerSectionCode { get; set; }
        //public int Id { get; set; }
        public int SectionId { get; set; }
        public int StoreId { get; set; }
        public string Name { get; set; }
        public bool Gender { get; set; }
        public string MobilePhone { get; set; }
        public string OperatorCode { get; set; }
        public int AuthRight { get; set; }
        public int IsBinded { get; set; }
        public int UserId { get; set; }
        //public System.DateTime CreateDate { get; set; }
        public int CreateUserId { get; set; }
        //public System.DateTime UpdateDate { get; set; }
        public int UpdateUserId { get; set; }
        //public int Status { get; set; }

        public Nullable<System.DateTime> BindTime { get; set; }

        /// <summary>
        /// 是否是店长
        /// </summary>
        public bool IsStoreManager { get; set; }        
        
        /// <summary>
        /// 导入的买手级别
        /// </summary>
        public UserLevel UserLevel { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public DateTime? BirthDay { get; set; }


        public string Remarks { get; set; }
        #region Overrides of BaseEntity

        /// <summary>
        /// KeyMemberId
        /// </summary>
        // public override object EntityId
        // {       
        //         get { return Id; } 
        // }

        #endregion


        
        public static void BuildMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupEntity>().ToTable("group");
            modelBuilder.Entity<GroupEntity>(entity =>
            {
                entity.Property(e => e.Name).IsRequired();
                entity.HasKey(e => e.Id);
                
                // entity.Ignore(e=>e.CreateUser);
                // entity.Ignore(e=>e.CreateDate);
                // entity.Ignore(e=>e.UpdateUser);
                // entity.Ignore(e=>e.UpdateDate);
                //entity.Ignore(e=>e.Status);

                //entity.Property(e => e.CreateDate).HasColumnName("CreatedDate");
                entity.Property(e => e.CreateUser).HasColumnName("CreateUserId");
                //entity.Property(e => e.UpdateDate).HasColumnName("UpdatedDate");
                entity.Property(e => e.UpdateUser).HasColumnName("UpdateUserId");
            });
        }
    }
}
