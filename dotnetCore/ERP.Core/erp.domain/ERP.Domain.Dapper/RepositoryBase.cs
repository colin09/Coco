using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
// using System.Linq.Expressions;
using System.Text;
using Dapper;
using ERP.Common.Infrastructure.Dapper.AggrQueryObject;
using ERP.Common.Infrastructure.Dapper.QueryObject;
using ERP.Common.Infrastructure.Data;
using ERP.Common.Infrastructure.Models;
using ERP.Common.Infrastructure.Utility;
using ERP.Domain.Context;
using ERP.Domain.Model;
using ERP.Domain.Repository.DapperRepository;

namespace ERP.Domain.Dapper {
    public abstract class RepositoryBase<TEntity> : IRepository<TEntity>, IDisposable where TEntity : BaseEntity {
        protected const int Default_CommandTimeout = 360;
        private IERPContext _context;
        public RepositoryBase (IERPContext context) {
            _context = context;
        }

        protected IDbConnection Conn { get { return _context.Connection; } }
        protected IDbTransaction Tran { get { return _context.Transaction; } }

        #region FirstOrDefault

        public TEntity FirstOrDefault (string id) {
            var query = Common.Infrastructure.Dapper.QueryObject.Query.Create ()
                .Add (Criterion.Create ("Id", id, CriteriaOperator.Equal, this.TableAliasName));

            if (typeof (IDeleted).IsAssignableFrom (typeof (TEntity))) {
                query.Add (Criterion.Create ("IsDeleted", false, CriteriaOperator.Equal, this.TableAliasName));
            }
            return FirstOrDefault (query);
        }

        public virtual TEntity FirstOrDefault (Query query, Sort sort = null) {
            object param;
            var whereSql = GetWhereSql (query, out param);
            return this.Query<TEntity> (SqlUtility.CreateTop1SelectSql (SelectSql, whereSql,
                sort == null ? null : sort.Translate ()), param).FirstOrDefault ();
        }

        #endregion

        #region Count
        public virtual int Count (string sql, object parameters = null) {
            return Conn.ExecuteScalar<int> (sql, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual int Count (Query query) {
            object param;
            string whereSql = GetWhereSql (query, out param);
            return this.Count (CombineCountSql (this.SelectSql, whereSql), param);
        }

        #endregion

        #region Exists

        public virtual bool Exists (string id) {
            var query = Common.Infrastructure.Dapper.QueryObject.Query.Create ().Add (Criterion.Create ("Id", id, CriteriaOperator.Equal, this.TableAliasName));
            if (typeof (IDeleted).IsAssignableFrom (typeof (TEntity))) {
                query.Add (Criterion.Create ("IsDeleted", false, CriteriaOperator.Equal));
            }
            object param;
            var whereSql = GetWhereSql (query, out param);
            return this.Count (CombineCountSql (SelectSql, whereSql), param) > 0;
        }
        #endregion

        #region Query TEntity
        public IEnumerable<TEntity> Query (string query, object parameters = null) {
            return Conn.Query<TEntity> (query, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual IEnumerable<TEntity> Query (Query query, Sort sort = null) {
            object param;
            string sql = GetQuerySql (query, sort, out param);
            return Conn.Query<TEntity> (sql, param, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual IEnumerable<TEntity> QueryPaged (Query query, Pager pager, Sort sort) {
            object param;
            string whereSql = GetWhereSql (query, out param);
            param = AddPagedParams (param, pager);
            if (pager.IsGetTotalCount) {
                pager.TotalCount = this.Count (CombineCountSql (SelectSql, whereSql), param);
            }
            return Conn.Query<TEntity> (CombinePagedSql (SelectSql, whereSql, sort.Translate ()), param, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual IEnumerable<TEntity> QueryPaged (string countSql, object countParameters, string query, object parameters, out int totalCount) {
            totalCount = this.Count (countSql, countParameters);
            return Conn.Query<TEntity> (query, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        protected string GetQuerySql (Query query, Sort sort, out object param) {
            return CombineSql (this.SelectSql, GetWhereSql (query, out param), sort == null ? null : sort.Translate ());
        }

        protected string GetWhereSql (Query query, out object param) {
            param = null;
            if (query == null) return null;
            var result = query.Translate ();
            if (result == null) return null;
            param = result.Parameter;
            return " where " + result.Sql;
        }

        protected string CombineSql (string selectSql, string whereSql, string orderBy = null) {
            string sql = selectSql;
            if (!string.IsNullOrWhiteSpace (whereSql))
                sql += whereSql;
            if (!string.IsNullOrWhiteSpace (orderBy))
                sql += orderBy;
            return sql;
        }

        protected string CombineCountSql (string selectSql, string whereSql) {
            return string.Format ("{0} {1}", SqlUtility.CreateCountSqlBySelectSql (selectSql), whereSql);
        }

        protected string CombinePagedSql (string selectSql, string whereSql, string sortSql) {
            return string.Format ("{0} {1} {2} {3}", selectSql, whereSql, sortSql, "OFFSET (@PageIndex-1)*@PageSize ROWS FETCH NEXT @PageSize ROWS ONLY");
        }

        protected object AddPagedParams (object param, Pager pager) {
            if (param == null) {
                IDictionary<string, object> d = new Dictionary<string, object> ();
                d.Add ("PageIndex", pager.PageIndex);
                d.Add ("PageSize", pager.PageSize);
                return d;
            }
            IDictionary<string, object> dic = param as IDictionary<string, object>;
            if (dic != null) {
                dic.Add ("PageIndex", pager.PageIndex);
                dic.Add ("PageSize", pager.PageSize);
            }
            return param;
        }

        protected string GetAggrSelectSql (string selectSql, AggrQuery query) {
            return SqlUtility.ReplaceFieldSql (selectSql, query.Translate ());
        }

        protected void SetAggr (string sql, AggrQuery aggrQuery, object param) {
            aggrQuery.SetResult (Conn.Query (GetAggrSelectSql (sql, aggrQuery), param, Tran, commandTimeout : Default_CommandTimeout).FirstOrDefault ());
        }

        #endregion

        #region Query TAny
        public virtual IEnumerable<TAny> Query<TAny> (string selectSql, Query query, Sort sort = null) {
            object param;
            string whereSql = GetWhereSql (query, out param);
            return Conn.Query<TAny> (CombineSql (selectSql, whereSql, sort == null ? null : sort.Translate ()), param, Tran, commandTimeout : Default_CommandTimeout);
        }
        public virtual IEnumerable<TAny> QueryPaged<TAny> (string selectSql, Query query, Pager pager, Sort sort, AggrQuery aggrQuery = null) {
            object param;
            string whereSql = GetWhereSql (query, out param);
            param = AddPagedParams (param, pager);
            if (pager.IsGetTotalCount) {
                pager.TotalCount = this.Count (CombineCountSql (selectSql, whereSql), param);
            }
            if (aggrQuery != null) {
                var sql = CombineSql (selectSql, whereSql);
                SetAggr (sql, aggrQuery, param);
            }
            return Conn.Query<TAny> (CombinePagedSql (selectSql, whereSql, sort.Translate ()), param, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual IEnumerable<TAny> Query<TAny> (string query, object parameters = null) {
            return Conn.Query<TAny> (query, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual IEnumerable<TAny> QueryPaged<TAny> (string countSql, object countParameters, string query, object parameters, out int totalCount) where TAny : class {
            totalCount = this.Count (countSql, parameters);
            return Conn.Query<TAny> (query, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        #endregion

        #region Insert

        public virtual void Insert (TEntity entity) {
            Conn.Execute (InsertSql, entity, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual void InsertBatch (IEnumerable<TEntity> entities) {
            foreach (var item in entities) {
                Insert (item);
            }
        }

        #endregion

        #region Execute
        public virtual int Execute (string sql, object parameters = null) {
            return Conn.Execute (sql, parameters, Tran, commandTimeout : Default_CommandTimeout);
        }

        #endregion

        #region Update
        public virtual void Update (TEntity entity) {
            Conn.Execute (UpdateSql, entity, Tran, commandTimeout : Default_CommandTimeout);
        }

        public virtual void UpdateBatch (IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                this.Update (entity);
            }
        }
        #endregion

        #region Delete
        public virtual void Delete (TEntity entity) {
            Execute (DeleteSql, new { Id = entity.Id });
        }
        public virtual void Delete (string id) {
            Execute (DeleteSql, new { Id = id });
        }
        public virtual void DeleteBatch (params string[] ids) {
            foreach (var id in ids) {
                this.Delete (id);
            }
        }
        public virtual void DeleteBatch (IEnumerable<TEntity> entities) {
            foreach (var entity in entities) {
                this.Delete (entity);
            }
        }
        #endregion

        #region IDisposed

        private bool disposed = false;

        protected virtual void Dispose (bool disposing) {
            if (!this.disposed) {
                if (disposing) {
                    _context.Dispose ();
                }
            }
            this.disposed = true;
        }

        public void Dispose () {
            Dispose (true);
            GC.SuppressFinalize (this);
        }
        #endregion

        #region Sql
        public virtual string TableAliasName { get { return ""; } }
        public abstract string SelectSql { get; }
        public abstract string InsertSql { get; }
        public abstract string UpdateSql { get; }
        public abstract string DeleteSql { get; }
        #endregion

        public IEnumerable<TAny> QueryPaged<TAny> (string selectSql, Query query, Pager pager, Sort sort, string andWhereSql, Dictionary<string, object> parameters, AggrQuery aggrQuery = null) {
            object param;
            string whereSql = GetWhereSql (query, out param);
            //string.IsNullOrWhiteSpace(whereSql)
            if (string.IsNullOrWhiteSpace (whereSql)) {
                whereSql = " where 1=1 " + andWhereSql;
            } else {
                whereSql += " " + andWhereSql;
            }
            param = AddPagedParams (param, pager);
            IDictionary<string, object> dic = param as IDictionary<string, object>;
            if (dic != null) {
                if (parameters.Any ()) {
                    foreach (var item in parameters) {
                        dic.Add (item);
                    }

                }
            }
            if (pager.IsGetTotalCount) {
                pager.TotalCount = this.Count (CombineCountSql (selectSql, whereSql), dic);
            }
            return Conn.Query<TAny> (CombinePagedSql (selectSql, whereSql, sort.Translate ()), dic, Tran, commandTimeout : Default_CommandTimeout);
        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TReturn> (string sql, Func<TFirst, TSecond, TThird, TFourth, TReturn> map, object param = null, string split = null) {
            return Conn.Query<TFirst, TSecond, TThird, TFourth, TReturn> (sql, map, param, splitOn : split == null ? "Id" : split, commandTimeout : Default_CommandTimeout);

        }

        public IEnumerable<TReturn> Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> (string sql, Func<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> map, object param = null, string split = null) {
            return Conn.Query<TFirst, TSecond, TThird, TFourth, TFifth, TReturn> (sql, map, param, splitOn : split == null ? "Id" : split, commandTimeout : Default_CommandTimeout);

        }
    }
}