using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Text;
using ERP.Domain.Model;


namespace ERP.Domain.Context
{
    public static class IHiddenExtensions
    {
        /* 
        public static IQueryable<T> WhereNotHidden<T>(this IQueryable<T> source)
            where T : class, IHidden
        {
            return source.Where(i => !i.IsHidden);
        }

        public static IQueryable<T> WhereNotHidden<T>(this DbQuery<T> source)
            where T : class,IHidden
        {
            return source.Where(i => !i.IsHidden);
        }

        public static IQueryable<T> WhereHidden<T>(this IQueryable<T> source)
            where T : class, IHidden
        {
            return source.Where(i => i.IsHidden);
        }

        public static IQueryable<T> WhereHidden<T>(this DbQuery<T> source)
            where T : class,IHidden
        {
            return source.Where(i => i.IsHidden);
        }*/
    }
}
