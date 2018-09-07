using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
//using Microsoft.Extensions.Configuration;
using ERP.Common.Infrastructure.Configs;

namespace ERP.Domain.Context {
    public class DbConnectionFactory {

        //private readonly IConfiguration _config;
        private const string ERPContextName = "ERPContext";
        private static string _connectionString = null;
        private static string _connectionString_Query = null;

        // public DbConnectionFactory (IConfiguration config) {
        //     _config = config;
        //     _config.GetConnectionString("MyConnectionString")
        // }

        public static string ConnectionString {
            get {
                if (string.IsNullOrWhiteSpace (_connectionString)) {
                    /* 
                    var setting = ConfigurationManager.ConnectionStrings[ERPContextName];
                    if (setting != null)
                        _connectionString = setting.ConnectionString;
                    */
                    _connectionString = AppsettingManager.Get (ERPContextName);
                }

                return _connectionString;
            }
        }

        public static string ConnectionString_Query {
            get {
                if (string.IsNullOrWhiteSpace (_connectionString_Query)) {
                    /*
                    var setting = ConfigurationManager.ConnectionStrings[ERPContextName + "_Query"];
                    if (setting != null)
                        _connectionString_Query = setting.ConnectionString;
                    */
                    _connectionString_Query = AppsettingManager.Get (ERPContextName + "_Query");
                }

                return _connectionString_Query;
            }
        }

        public static IDbConnection CreateDbConnection () {
            return new SqlConnection (ConnectionString);
        }

        public static IDbConnection CreateDbQueryConnection () {
            return new SqlConnection (ConnectionString_Query);
        }
    }
}