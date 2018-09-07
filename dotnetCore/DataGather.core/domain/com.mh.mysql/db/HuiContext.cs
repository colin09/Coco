using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;

using com.mh.common.configuration;
using com.mh.model.mysql.entity;

namespace com.mh.mysql.db
{
    public class HuiContext : DbContext
    {

        /*/
        private string dbConnStr = "server=192.168.1.182;database=myCore;user=sa;password=Sa123@456;";
        public HuiContext() { }
        public HuiContext(string connStrName)
        {
            switch (connStrName)
            {
                case "Hui": dbConnStr = ConfigManager.HuiConnStr; break;
                case "MagicHorse": dbConnStr = ConfigManager.MagicHorseConnStr; break;
                default: break;
            }
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbConnStr = ConfigManager.HuiConnStr;
            optionsBuilder.UseMySQL(dbConnStr);
        }

        public DbSet<StoreEntity> Store { get; set; }
        public DbSet<SectionEntity> Section { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StoreEntity>(entity => StoreEntity.buildAction(entity));
        }
    }

}