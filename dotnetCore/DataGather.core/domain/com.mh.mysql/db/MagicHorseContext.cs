using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

using com.mh.common.configuration;
using com.mh.model.mysql.entity;

namespace com.mh.mysql.db
{
    public class MagicHorseContext : DbContext
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnStr = ConfigManager.MagicHorseConnStr;
            optionsBuilder.UseMySQL(dbConnStr);
        }

        public DbSet<GroupEntity> Group { get; set; }
        public DbSet<StoreEntity> Store { get; set; }
        public DbSet<SectionEntity> Section { get; set; }
        public DbSet<OPC_OrgInfoEntity> OPC_OrgInfo { get; set; }
        public DbSet<IMS_OperatorEntity> IMS_Operator { get; set; }

        public DbSet<Fifteen_Mins_CouponEntity> Fifteen_Mins_Coupon { get; set; }
        public DbSet<Fifteen_Mins_Privilege_CouponEntity> Fifteen_Mins_Privilege_Coupon { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            GroupEntity.BuildMapping(modelBuilder);
            StoreEntity.BuildMapping(modelBuilder);
            SectionEntity.BuildMapping(modelBuilder);
            OPC_OrgInfoEntity.BuildMapping(modelBuilder);
            IMS_OperatorEntity.BuildMapping(modelBuilder);


            Fifteen_Mins_CouponEntity.BuildMapping(modelBuilder);
            Fifteen_Mins_Privilege_CouponEntity.BuildMapping(modelBuilder);
            //modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }
    }

}