﻿using System;
using System.Linq;
using System.Linq.Expressions;

namespace Skoruba.IdentityServer4.Admin.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> query, bool condition, Expression<Func<T, bool>> predicate)
        {
            return condition
                ? query.Where(predicate)
                : query;
        }

        public static IQueryable<T> PageBy<T, TKey>(this IQueryable<T> query, Expression<Func<T, TKey>> orderBy, int page, int pageSize, bool orderByDescending = true)
        {
            if (query == null)
            {
                throw new ArgumentNullException(nameof(query));
            }

            //it is necessary order before paging it - it is one of EF warrnings
            query = orderByDescending ? query.OrderByDescending(orderBy) : query.OrderBy(orderBy);

            //Pagining according page and page size
            return query.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}