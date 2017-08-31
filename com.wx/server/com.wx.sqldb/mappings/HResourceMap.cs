using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.mappings
{
   public class HResourceMap : EntityTypeConfiguration<ResourceEntity>
    {
        public HResourceMap()
        {
            this.ToTable("H_Resource");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            //表列名、not null、长度20
            this.Property(h => h.Name).HasColumnName("Name").IsRequired().HasMaxLength(200);
            this.Property(h => h.Domain).HasColumnName("Domain");
            this.Property(h => h.SourceId).HasColumnName("SourceId");
            this.Property(h => h.SourceType).HasColumnName("SourceType");

            this.Property(h => h.SortOrder).HasColumnName("SortOrder").IsRequired();
            this.Property(h => h.ExtName).HasColumnName("ExtName");
            this.Property(h => h.ExtId).HasColumnName("ExtId").IsRequired();
            this.Property(h => h.IsDefault).HasColumnName("IsDefault").IsRequired();

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }

}
