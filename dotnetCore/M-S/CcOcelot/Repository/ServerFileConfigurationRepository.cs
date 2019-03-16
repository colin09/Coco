using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using CcOcelot.Data;
using Dapper;
using MySql.Data.MySqlClient;
using Ocelot.Configuration.File;
using Ocelot.Configuration.Repository;
using Ocelot.Responses;

namespace CcOcelot.Repository {
    public class ServerFileConfigurationRepository : IFileConfigurationRepository {

        private readonly string _connString;
        public ServerFileConfigurationRepository (CcOcelotConfig config) {
            _connString = config.DbConnectionStrings;
        }

        public async Task<Response<FileConfiguration>> Get () {
            var fileConfig = new FileConfiguration ();
            var ccConfig = await GetGlobalConfiguration ();
            var config = new FileGlobalConfiguration () {
                BaseUrl = ccConfig.BaseUrl,
                DownstreamScheme = ccConfig.DownstreamSchem,
                RequestIdKey = ccConfig.DownstreamSchem,
            };

            if (!ccConfig.HttpHandlerOptions.IsEmpty ()) config.HttpHandlerOptions = ccConfig.HttpHandlerOptions?.ToObject<FileHttpHandlerOptions> ();
            if (!ccConfig.LoadBalancerOptions.IsEmpty ()) config.LoadBalancerOptions = ccConfig.LoadBalancerOptions?.ToObject<FileLoadBalancerOptions> ();
            if (!ccConfig.QosOptions.IsEmpty ()) config.QoSOptions = ccConfig.QosOptions?.ToObject<FileQoSOptions> ();
            if (!ccConfig.ServiceDiscoveryProvider.IsEmpty ()) config.ServiceDiscoveryProvider = ccConfig.ServiceDiscoveryProvider?.ToObject<FileServiceDiscoveryProvider> ();

            fileConfig.GlobalConfiguration = config;

            var reRoutes = await GetReroute (ccConfig.Id);
            var reRouteList = new List<FileReRoute> ();
            reRoutes.ForEach (item => {
                var reRoute = new FileReRoute () {
                    UpstreamHost = item.UpstreamHost,
                    UpstreamPathTemplate = item.UpstreamPathTemplate,

                    DownstreamPathTemplate = item.DownstreamPathTemplate,
                    DownstreamScheme = item.DownstreamScheme,

                    Key = item.RequestIdKey,
                    Priority = item.Priority,
                    RequestIdKey = item.RequestIdKey,
                    ServiceName = item.ServiceName,
                };

                if (!item.UpstreamHttpMethod.IsEmpty ()) reRoute.UpstreamHttpMethod = item.UpstreamHttpMethod?.ToObject<List<string>> ();
                if (!item.DownstreamHostAndPorts.IsEmpty ()) reRoute.DownstreamHostAndPorts = item.DownstreamHostAndPorts?.ToObject<List<FileHostAndPort>> ();
                System.Console.WriteLine ($"DownstreamHostAndPort ： {item.DownstreamHostAndPorts} , DownstreamHostAndPorts : {reRoute.DownstreamHostAndPorts.ToJson()}");
                if (!item.AuthenticationOptions.IsEmpty ()) reRoute.AuthenticationOptions = item.AuthenticationOptions?.ToObject<FileAuthenticationOptions> ();
                if (!item.CacheOptions.IsEmpty ()) reRoute.FileCacheOptions = item.CacheOptions?.ToObject<FileCacheOptions> ();
                if (!item.DelegatingHandlers.IsEmpty ()) reRoute.DelegatingHandlers = item.DelegatingHandlers?.ToObject<List<string>> ();
                if (!item.LoadBalancerOptions.IsEmpty ()) reRoute.LoadBalancerOptions = item.LoadBalancerOptions?.ToObject<FileLoadBalancerOptions> ();
                if (!item.QosOptions.IsEmpty ()) reRoute.QoSOptions = item.QosOptions?.ToObject<FileQoSOptions> ();

                reRouteList.Add (reRoute);
            });
            fileConfig.ReRoutes = reRouteList;

            if (fileConfig.ReRoutes == null || fileConfig.ReRoutes.Count < 1)
                return new OkResponse<FileConfiguration> (null);
            return new OkResponse<FileConfiguration> (fileConfig);
        }

        public async Task<Response> Set (FileConfiguration fileConfiguration) {
            // throw new System.NotImplementedException();
            return new OkResponse ();
        }

        public async Task<CcGlobalConfiguration> GetGlobalConfiguration () {
            var sql = "select * from CcGlobalConfiguration WHERE IsDefault=1 and InfoStatus=1";
            using (var conn = new MySqlConnection (_connString)) {
                var result = await conn.QueryFirstOrDefaultAsync<CcGlobalConfiguration> (sql);
                return result;
            }
            //return new CcGlobalConfiguration ();
        }

        public async Task<List<CcReroute>> GetReroute (int routeId) {
            string sql = "select CcReroute.* from CcRerouteConfig inner join CcReroute on CcRerouteConfig.ReRouteId=CcReroute.Id WHERE CcRerouteConfig.RouteId=@RouteId AND CcReroute.InfoStatus=1";
            using (var conn = new MySqlConnection (_connString)) {
                System.Console.WriteLine ($"routeId : {routeId}");
                var result = await conn.QueryAsync<CcReroute> (sql, new { RouteId = routeId });
                return result.ToList ();
            }
            //return new List<CcReroute> ();
        }
    }
}