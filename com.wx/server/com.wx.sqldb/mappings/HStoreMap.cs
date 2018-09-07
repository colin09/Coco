﻿using com.wx.sqldb.data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.wx.sqldb.mappings
{
    public class HStoreMap : EntityTypeConfiguration<StoreEntity>
    {

        public HStoreMap()
        {
            this.ToTable("H_Store");

            //表列名、主键、自动增长
            this.HasKey(h => h.Id).Property(h => h.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(h => h.Id).HasColumnName("Id").IsRequired();

            //表列名、not null、长度20
            this.Property(h => h.Name).HasColumnName("Name").IsRequired().HasMaxLength(20);
            this.Property(h => h.EMail).HasColumnName("Address");
            this.Property(h => h.Contact).HasColumnName("Contact");
            this.Property(h => h.Mobile).HasColumnName("Mobile").HasMaxLength(20);
            this.Property(h => h.EMail).HasColumnName("EMail");
            this.Property(h => h.Logo).HasColumnName("Logo");

            this.Property(h => h.Description).HasColumnName("Description");

            this.Property(h => h.Status).HasColumnName("Status").IsRequired();
            this.Property(h => h.CreateDate).HasColumnName("CreateDate").IsRequired();
            this.Property(h => h.CreateUser).HasColumnName("CreateUser").IsRequired();
            this.Property(h => h.UpdateDate).HasColumnName("UpDateDate");
            this.Property(h => h.UpdateUser).HasColumnName("UpdateUser");
        }
    }
}
