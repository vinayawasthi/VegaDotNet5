
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Vega.Web.Models;

namespace Vega.Web.Extentions
{
    public static class IQueryableExtentions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, ISortObject sortObject, Dictionary<string, Expression<Func<T, object>>> columnMap)
        {
            if (string.IsNullOrWhiteSpace(sortObject.SortBy) || !columnMap.ContainsKey(sortObject.SortBy))
                return query;

            if (sortObject.IsSortAscending)
                return query.OrderBy(columnMap[sortObject.SortBy]);
            else
                return query.OrderByDescending(columnMap[sortObject.SortBy]);
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, int pageIndex, int pageSize)
        {
            // if(pageIndex <= 0 )
            //     throw new Exception("page index should be greater than or equal to 1");
            // if(pageSize <= 0 )
            //     throw new Exception("page size should be greater than or equal to 1");
            
            if (pageIndex > 0 && pageSize > 0)
            {
                return query.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            }else{
                return query.Take(100);
            }
        }
    }
}