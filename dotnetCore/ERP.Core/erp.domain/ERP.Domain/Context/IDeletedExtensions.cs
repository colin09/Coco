using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using ERP.Domain.Model;

namespace ERP.Domain.Context
{
    public static class IDeletedExtensions
    {
        /*
        public static IQueryable<T> WhereNotDeleted<T>(this IQueryable<T> source)
            where T : class, IDeleted
        {
            return source.Where(i => !i.IsDeleted);
        }

        public static IQueryable<T> WhereNotDeleted<T>(this DbQuery<T> source)
            where T : class,IDeleted
        {
            return source.Where(i => !i.IsDeleted);
        }

        public static IQueryable<T> WhereDeleted<T>(this IQueryable<T> source)
            where T : class, IDeleted
        {
            return source.Where(i => i.IsDeleted);
        }

        public static IQueryable<T> WhereDeleted<T>(this DbQuery<T> source)
            where T : class,IDeleted
        {
            return source.Where(i => i.IsDeleted);
        }*/
    }
}
