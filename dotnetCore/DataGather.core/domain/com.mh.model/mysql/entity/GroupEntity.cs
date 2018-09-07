using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace com.mh.model.mysql.entity
{
    public partial class GroupEntity : BaseEntity
    {
        //public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int PaymentMethodType { get; set; }
        public int CreatedUser { get; set; }
        public System.DateTime CreatedDate { get; set; }
        public int UpdatedUser { get; set; }
        public System.DateTime UpdatedDate { get; set; }
        //public DataStatus Status { get; set; }

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

                entity.Property(e => e.CreateDate).HasColumnName("CreatedDate");
                entity.Property(e => e.CreateUser).HasColumnName("CreatedUser");
                entity.Property(e => e.UpdateDate).HasColumnName("UpdatedDate");
                entity.Property(e => e.UpdateUser).HasColumnName("UpdatedUser");
            });
        }
    }
}