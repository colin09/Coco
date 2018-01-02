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
   public class HOutsiteUserMap : EntityTypeConfiguration<OutsiteUserEntity>
    {
        public HOutsiteUserMap()
        {
            this.ToTable("H_OutsiteUser");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            //表列名、not null、长度20
            this.Property(h => h.UserId).HasColumnName("UserId").IsRequired();
            this.Property(h => h.OpenId).HasColumnName("OpenId").IsRequired();
            this.Property(h => h.OutSiteType).HasColumnName("OutSiteType").IsRequired();
            this.Property(h => h.AccountNO).HasColumnName("AccountNO").IsRequired();
            this.Property(h => h.IsOauthed).HasColumnName("IsOauthed").IsRequired();

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }

}
