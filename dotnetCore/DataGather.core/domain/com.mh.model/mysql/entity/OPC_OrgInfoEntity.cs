using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace com.mh.model.mysql.entity
{
    public partial class OPC_OrgInfoEntity : BaseEntity
    {
        //public int Id { get; set; }
        public string OrgID { get; set; }
        public string OrgName { get; set; }
        public string ParentID { get; set; }
        public int? StoreOrSectionID { get; set; }
        public string StoreOrSectionName { get; set; }
        public int? OrgType { get; set; }
        public bool? IsDel { get; set; }
        public int? OrgConfigId { get; set; }

        //public DataStatus Status { get; set; }

        #region Overrides of BaseEntity

        /// <summary>
        /// KeyMemberId
        /// </summary>
        // public override object EntityId
        // {
        //     get { return Id; }
        // }

        #endregion



        
        public static void BuildMapping(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OPC_OrgInfoEntity>().ToTable("opc_orginfo");
            modelBuilder.Entity<OPC_OrgInfoEntity>(entity =>
            {
                entity.Property(e => e.OrgID).IsRequired();
                entity.HasKey(e => e.Id);
                
                entity.Ignore(e=>e.CreateUser);
                entity.Ignore(e=>e.CreateDate);
                entity.Ignore(e=>e.UpdateUser);
                entity.Ignore(e=>e.UpdateDate);
                //entity.Ignore(e=>e.Status);
            });
        }
    }
}