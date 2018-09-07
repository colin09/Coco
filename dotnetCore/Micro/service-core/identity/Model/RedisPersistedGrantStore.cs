using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Foundatio.Caching;
using IdentityServer4.Models;
using IdentityServer4.Stores;
using Microsoft.Extensions.Configuration;

namespace identity.Model {

    public class RedisPersistedGrantStore : IPersistedGrantStore {

        private readonly ICacheClient _cacheClient;
        private readonly IConfiguration _configuration;

        public RedisPersistedGrantStore (ICacheClient cacheClient, IConfiguration configuration) {
            _cacheClient = cacheClient;
            _configuration = configuration;
        }

        public Task StoreAsync (PersistedGrant grant) {
            var accessTokenLifetime = double.Parse (_configuration.GetConnectionString ("accessTokenLifetime"));
            var timeSpan = TimeSpan.FromSeconds (accessTokenLifetime);
            _cacheClient?.SetAsync (grant.Key, grant, timeSpan);
            return Task.CompletedTask;
        }

        public Task<PersistedGrant> GetAsync (string key) {
            if (_cacheClient.ExistsAsync (key).Result) {
                var ss = _cacheClient.GetAsync<PersistedGrant> (key).Result;
                return Task.FromResult<PersistedGrant> (_cacheClient.GetAsync<PersistedGrant> (key).Result.Value);
            }
            return Task.FromResult<PersistedGrant> ((PersistedGrant) null);
        }

        public Task<IEnumerable<PersistedGrant>> GetAllAsync (string subjectId) {
            var persistedGrants = _cacheClient.GetAllAsync<PersistedGrant> ().Result.Values;
            return Task.FromResult<IEnumerable<PersistedGrant>> (persistedGrants
                .Where (x => x.Value.SubjectId == subjectId).Select (x => x.Value));
        }

        public Task RemoveAsync (string key) {
            _cacheClient?.RemoveAsync (key);
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync (string subjectId, string clientId) {
            _cacheClient.RemoveAllAsync ();
            return Task.CompletedTask;
        }

        public Task RemoveAllAsync (string subjectId, string clientId, string type) {
            var persistedGrants = _cacheClient.GetAllAsync<PersistedGrant> ().Result.Values
                .Where (x => x.Value.SubjectId == subjectId && x.Value.ClientId == clientId &&
                    x.Value.Type == type).Select (x => x.Value);
            foreach (var item in persistedGrants) {
                _cacheClient?.RemoveAsync (item.Key);
            }
            return Task.CompletedTask;
        }

    }
}