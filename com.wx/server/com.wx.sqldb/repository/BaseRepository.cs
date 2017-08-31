﻿using com.wx.sqldb.data;
using com.wx.sqldb.factory;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace com.wx.sqldb.repository
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


    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private DbContext context
        {
            get
            {
                return DbContextFactory.GetCurrentDbContext("");
            }
        }

        public IQueryable<TEntity> Entities
        {
            get { return context.Set<TEntity>(); }
        }

        public void CreateList(IEnumerable<TEntity> list)
        {
            context.Set<TEntity>().AddRange(list);
            //context.SaveChanges();
        }

        public void Create(TEntity t)
        {
            context.Set<TEntity>().Add(t);
            //context.SaveChanges();
        }

        public void Update(TEntity t)
        {
            //context.Set<TEntity>().Attach(t);
            context.Entry(t).State = EntityState.Modified;
            //return context.SaveChanges();
        }

        public int Delete(int id)
        {
            var t = ReadOne(id);
            if (t == null)
                return -1;
            t.Status = DataStatue.Deleted;
            t.UpdateDate = DateTime.Now;
            Update(t);
            return context.SaveChanges();
        }

        public int Delete(TEntity t)
        {
            t.Status = DataStatue.Deleted;
            t.UpdateDate = DateTime.Now;
            Update(t);
            return context.SaveChanges();
        }

        public TEntity ReadOne(int id)
        {
            return context.Set<TEntity>().Where(t => t.Id == id).FirstOrDefault();
        }

        public IQueryable<TEntity> Where(Expression<Func<TEntity, bool>> whereLambda)
        {
            return context.Set<TEntity>().Where(whereLambda).AsQueryable();
        }

        public IQueryable<TEntity> Page<S>(int pageSize, int pageIndex, out int total,
            Expression<Func<TEntity, bool>> whereLambda, Expression<Func<TEntity, S>> orderbyLambda, bool isAsc)
        {
            var result = context.Set<TEntity>().Where<TEntity>(whereLambda).AsQueryable();
            total = result.Count();
            if (isAsc)
                return result.OrderBy<TEntity, S>(orderbyLambda)
                   .Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                   .AsQueryable();
            else
                return result.OrderByDescending(orderbyLambda)
                       .Skip(pageSize * (pageIndex - 1)).Take(pageSize)
                       .AsQueryable();
        }





















        public void Dispose()
        {
            //throw new NotImplementedException();
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing) { context.Dispose(); }
            }
            this.disposed = true;
        }

    }
}
