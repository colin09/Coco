using System;
using System.Linq;
using System.Linq.Expressions;
using System.Collections.Generic;



namespace com.mh.mysql.iservice
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class //Entity
    {

        #region 属性
        IQueryable<TEntity> Entities { get; }
        #endregion

        #region  -- CRUD  --

        void Create(TEntity t);
        void CreateList(IEnumerable<TEntity> list);

        void Update(TEntity t);

        int Delete(int id);

        int Delete(TEntity t);

        TEntity ReadOne(int id);

        IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereLambda);

        IQueryable<TEntity> Page<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc);

        #endregion

    }

}