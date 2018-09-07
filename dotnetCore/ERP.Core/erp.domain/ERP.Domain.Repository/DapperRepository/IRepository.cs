using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Domain.Model;
using System;
using System.Collections.Generic;
using ERP.Common.Infrastructure.Data;
// using System.Linq;
// using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ERP.Common.Infrastructure.Dapper.AggrQueryObject;

namespace ERP.Domain.Repository.DapperRepository
{
    public interface IRepository<TEntity> : IRepository, IDisposable where TEntity : BaseEntity
    {
        TEntity FirstOrDefault(string id);

        TEntity FirstOrDefault(Query query, Sort sort = null);

        int Count(string sql, object parameters = null);

        /// <summary>
        /// TEntity count
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        int Count(Query query);

        bool Exists(string id);

        IEnumerable<TEntity> Query(Query query, Sort sort = null);

        IEnumerable<TEntity> Query(string query, object parameters = null);

        IEnumerable<TEntity> QueryPaged(Query query, Pager pager, Sort sort);

        IEnumerable<TEntity> QueryPaged(string countSql, object countParameters, string query, object parameters, out int totalCount);
        IEnumerable<TAny> Query<TAny>(string selectSql, Query query, Sort sort = null);
        IEnumerable<TAny> QueryPaged<TAny>(string selectSql, Query query, Pager pager, Sort sort, AggrQuery aggrQuery = null);
        IEnumerable<TAny> QueryPaged<TAny>(string selectSql, Query query, Pager pager, Sort sort, string andWhereSql, Dictionary<string, object> parameters, AggrQuery aggrQuery = null);
        IEnumerable<TAny> Query<TAny>(string query, object parameters = null);
        IEnumerable<TAny> QueryPaged<TAny>(string countSql, object countParameters, string query, object parameters, out int totalCount) where TAny : class;

        void Insert(TEntity entity);

        void InsertBatch(IEnumerable<TEntity> entities);
        int Execute(string query, object parameters = null);

        void Delete(string id);

        void Delete(TEntity entity);

        void DeleteBatch(params string[] ids);

        void DeleteBatch(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void UpdateBatch(IEnumerable<TEntity> entities);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string split = null);
        IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn>(string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string split = null);
   
    }
}
