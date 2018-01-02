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
   public class HSalerIncomeHistoryMap : EntityTypeConfiguration<SalerIncomeHistoryEntity>
    {
        public HSalerIncomeHistoryMap()
        {
            this.ToTable("H_SalerIncomeHistory");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            this.Property(h => h.SalerUserId).HasColumnName("SalerUserId").IsRequired();
            this.Property(h => h.SourceType).HasColumnName("SourceType").IsRequired();
            this.Property(h => h.SourceNO).HasColumnName("SourceNO");
            this.Property(h => h.IncomeAmount).HasColumnName("IncomeAmount").IsRequired();
            this.Property(h => h.IncomeState).HasColumnName("IncomeState").IsRequired();

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}

