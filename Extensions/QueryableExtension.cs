using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Pantokrator.Data.Sql.Extensions
{
    public static class QueryableExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string propertyName, bool asc)
        {
            var type = typeof(T);
            string methodName = asc ? "OrderBy" : "OrderByDescending";
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                return source;

            }
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExp = Expression.Lambda(propertyAccess, parameter);
            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExp));
            return source.Provider.CreateQuery<T>(resultExp);
        }

        public static IList<T> OrderBy<T>(this List<T> source, string propertyName, bool asc)
        {
            var type = typeof(T);
            string methodName = asc ? "OrderBy" : "OrderByDescending";
            var property = type.GetProperty(propertyName);
            if (property == null)
            {
                return source;
            }

            if (!asc)
                return source.OrderByDescending(o => o.GetType().GetProperty(propertyName).GetValue(o, null)).ToList();

            return source.OrderBy(o => o.GetType().GetProperty(propertyName).GetValue(o, null)).ToList();
        }


        public static IQueryable<T> Search<T>(this IQueryable<T> source, string propertyName, string searchText)
        {
            if (string.IsNullOrWhiteSpace(propertyName) || string.IsNullOrWhiteSpace(searchText))
            {
                return source;
            }
            var type = typeof(T);
            ParameterExpression parameter = Expression.Parameter(type, "p");
            MemberExpression property;
            if (propertyName.Contains(".")) propertyName = propertyName.Split('.')[0];
            property = Expression.Property(parameter, propertyName);
            var toString = Expression.Call(property, property.Type.GetMethod("ToString", new Type[] { }));
            var toLower = Expression.Call(toString, property.Type.GetMethod("ToLower", new Type[] { }));
            var condition = Expression.Call(toLower, typeof(string).GetMethod("Contains"), Expression.Constant(searchText.ToLower()));
            Expression<Func<T, bool>> lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);
            return source.Where(lambda);

        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);

        }

        public static IEnumerable<T> Paging<T>(this IList<T> source, int page, int pageSize)
        {
            return source.Skip((page - 1) * pageSize).Take(pageSize);

        }

        //public static T[] ToArrayNoLock<T>(this IQueryable<T> query, string methodName = "")
        //{
        //    using (var txn = new TransactionScope(TransactionScopeOption.RequiresNew,
        //        new TransactionOptions
        //        {
        //            IsolationLevel = IsolationLevel.ReadUncommitted
        //        }))
        //    {
        //        //Type thisType = query.GetType();
        //        //MethodInfo theMethod = thisType.GetMethod(methodName);
        //        //return  (ICollection<T>)theMethod.Invoke(query, null);
        //        return query.ToArray();
        //    }
        //}

        //public static int CountNoLock<T>(this IQueryable<T> query)
        //{
        //    using (var txn = new TransactionScope(TransactionScopeOption.RequiresNew,
        //        new TransactionOptions
        //        {
        //            IsolationLevel = IsolationLevel.ReadUncommitted
        //        }))
        //    {
        //        return query.Count();
        //    }
        //}
    }
}