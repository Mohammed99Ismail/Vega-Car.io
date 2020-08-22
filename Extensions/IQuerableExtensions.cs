using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using vega.Models;

namespace vega.vscode.Extensions
{
    public static class IQuerableExtensions
    {
        public static IQueryable<T> ApplyingOrdering<T>(this IQueryable<T> query,IQueryObject QueryObj,Dictionary<string,Expression<Func<T,object>>> ColumsMap){
            if(QueryObj.IsSortAsending)
                return query.OrderBy(ColumsMap[QueryObj.SortBy]);
            else
                return query.OrderByDescending(ColumsMap[QueryObj.SortBy]);
        }
        public static IQueryable<T> ApplyingPaging<T>(this IQueryable<T> query,IQueryObject QueryObj){
            if(QueryObj.Page<=0)
            QueryObj.Page=1;
            if(QueryObj.PageSize<=0)
            QueryObj.PageSize=10;
            return query.Skip((QueryObj.Page-1)*QueryObj.PageSize).Take(QueryObj.PageSize);
        }
    }
}