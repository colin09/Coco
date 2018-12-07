using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
//using MySQL.Data.EntityFrameworkCore.Extensions;
using cap.db.entity;
using Microsoft.Extensions.Configuration;

namespace cap.db {

    public class HuiContext : DbContext {

        private readonly IConfiguration _config;
        public HuiContext (IConfiguration config) {
            this._config = config;
        }

         
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            var ConnectionString = _config.GetConnectionString ("HuiConnectionString");
            System.Console.WriteLine($"------------------> connectionString : {ConnectionString}");
            ConnectionString="Server=127.0.0.1;Character Set=utf8;Database=hui;Uid=sa;Pwd=sa-1234;";
            
            optionsBuilder.UseMySql(ConnectionString);
        }

        //public DbSet<UserEntity> Users { get; set; }
        public DbSet<TestEntity> Test { get; set; }

        /*
        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);

            //modelBuilder.Entity<UserEntity> (entity => UserEntity.buildAction (entity));
            //UserEntity.BuildMapping (modelBuilder);
        }*/
        
    }
}