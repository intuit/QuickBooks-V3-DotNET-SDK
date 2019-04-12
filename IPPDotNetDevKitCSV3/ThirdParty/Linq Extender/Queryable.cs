////********************************************************************
//https://github.com/mehfuzh/LinqExtender/blob/master/License.txt
//Copyright (c) 2007- 2010 LinqExtender Toolkit Project. 
//Project Modified by Intuit
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
// 
////********************************************************************

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace Intuit.Ipp.LinqExtender
{
    
    /// <summary>
    /// Defines various operations that extend the LINQ query.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public static class Queryable
    {
        /// <summary>
        /// Where clause for IQueryable
        /// </summary>
        public static IQueryable<TSource> Where<TSource>(this IQueryContext<TSource> source, Expression<Func<TSource, bool>> predicate)
        {
            MethodInfo currentMethod = (MethodInfo)MethodInfo.GetCurrentMethod();
            var args = new[] { typeof(TSource) };
            return CreateQuery<TSource, TSource, Func<TSource, bool>>(source, currentMethod, predicate, args);
        }

        /// <summary>
        /// OrderBy clause for IQueryable
        /// </summary>
        public static IQueryable<TSource> OrderBy<TSource, TKey>(this IQueryContext<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            MethodInfo currentMethod = (MethodInfo)MethodInfo.GetCurrentMethod();
            var args = new[] { typeof(TSource), typeof(TKey) };
            return CreateQuery<TSource, TSource, Func<TSource, TKey>>(source, currentMethod, keySelector, args);
        }

        /// <summary>
        /// OrderByDescending clause for IQueryable
        /// </summary>
        public static IQueryable<TSource> OrderByDescending<TSource, TKey>(this IQueryContext<TSource> source, Expression<Func<TSource, TKey>> keySelector)
        {
            var currentMethod = (MethodInfo)MethodInfo.GetCurrentMethod();
            var args = new[] { typeof(TSource), typeof(TKey) };
            return CreateQuery<TSource, TSource, Func<TSource, TKey>>(source, currentMethod, keySelector, args);
        }

        /// <summary>
        /// Join for IQueryable
        /// </summary>
        public static IEnumerable<TResult> Join<TOuter, TInner, TKey, TResult>(this IQueryContext<TOuter> outer,
            IQueryContext<TInner> inner,
            Expression<Func<TOuter, TKey>> outerKeySelector,
            Expression<Func<TInner, TKey>> innerKeySelector,
            Expression<Func<TOuter, TInner, TResult>> resultSelector)
        {

            throw new NotImplementedException("Not yet implemented");
        }

        /// <summary>
        /// CreateQuery for IQueryable
        /// </summary>
        private static IQueryable<TResult> CreateQuery<TSource, TResult, TDelegate>(IQueryContext<TSource> source,
            MethodInfo methodInfo,
            Expression<TDelegate> expression,
            Type[] genArgs)
        {
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            var methodGenArgs = methodInfo.GetGenericArguments();

            var targetMethod = typeof(Enumerable).GetMethods(flags)
                .Where(x => x.Name == methodInfo.Name)
                .Where(x => x.GetGenericArguments().All(arg =>
                {
                    return methodGenArgs.Any(y => y.Name == arg.Name);
                }))
                .First();

            targetMethod = targetMethod.MakeGenericMethod(genArgs);

            IList<TSource> list = new List<TSource>();
            Expression constant = Expression.Constant(list.AsQueryable(), typeof(IQueryable<TSource>));
            Expression call = Expression.Call(targetMethod, constant, expression);

            return new QueryProvider<TSource>(source).CreateQuery<TResult>(call);
        }

        /// <summary>
        /// Select for IQueryable
        /// </summary>
        public static IQueryable<TResult> Select<TSource, TResult>(this IQueryContext<TSource> source, Expression<Func<TSource, TResult>> selector)
        {
            MethodInfo currentMethod = (MethodInfo)MethodInfo.GetCurrentMethod();
            var args = new[] { typeof(TSource), typeof(TResult) };
            return CreateQuery<TSource, TResult, Func<TSource, TResult>>(source, currentMethod, selector, args) as IQueryable<TResult>;
        }

        /// <summary>
        /// Count for IQueryable
        /// </summary>
        public static int Count<TSource>(this IQueryContext<TSource> source)
        {
            MethodInfo methodInfo = (MethodInfo)MethodInfo.GetCurrentMethod();
            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            var methodGenArgs = methodInfo.GetGenericArguments();

            var targetMethod = typeof(Enumerable).GetMethods(flags)
                .Where(x => x.Name == methodInfo.Name)
                .Where(x => x.GetGenericArguments().All(arg =>
                {
                    return methodGenArgs.Any(y => y.Name == arg.Name);
                }))
                .First();

            targetMethod = targetMethod.MakeGenericMethod(new Type[] { typeof(TSource) });

            IList<TSource> list = new List<TSource>();
            Expression constant = Expression.Constant(list.AsQueryable(), typeof(IQueryable<TSource>));
            MethodCallExpression mce = Expression.Call(targetMethod, new Expression[] { constant });
            return Convert.ToInt32(new QueryProvider<TSource>(source).Execute(mce));
        }

        /// <summary>
        /// ToIdsQuery for IQueryable
        /// </summary>
        public static string ToIdsQuery<TSource>(this IQueryContext<TSource> source)
        {
            string query = string.Format("QUERY * FROM {0}", typeof(TSource).Name);
            return query;
        }

        /// <summary>
        /// ToIdsQuery for IQueryable
        /// </summary>
        public static string ToIdsQuery<TSource>(this IQueryable<TSource> source)
        {
            MethodInfo methodInfo = (MethodInfo)MethodInfo.GetCurrentMethod();
            BindingFlags flags = BindingFlags.Static | BindingFlags.NonPublic;

            var methodGenArgs = methodInfo.GetGenericArguments();

            var targetMethod = typeof(Queryable).GetMethods(flags)
                .Where(x => x.Name == methodInfo.Name)
                .Where(x => x.GetGenericArguments().All(arg =>
                {
                    return methodGenArgs.Any(y => y.Name == arg.Name);
                }))
                .First();

            MethodCallExpression expr = Expression.Call(null, targetMethod);
            return source.Provider.Execute(expr).ToString();
        }

        /// <summary>
        /// ToIdsQuery for IQueryable
        /// </summary>
        internal static string ToIdsQuery()
        {
            return string.Empty;
        }

        /// <summary>
        /// Skip for IQueryable
        /// </summary>
        public static IQueryable<TSource> Skip<TSource>(this IQueryContext<TSource> source, int count)
        {
            MethodInfo methodInfo = (MethodInfo)MethodInfo.GetCurrentMethod();
            var genArgs = new[] { typeof(TSource) };

            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            var methodGenArgs = methodInfo.GetGenericArguments();

            var targetMethod = typeof(Enumerable).GetMethods(flags)
                .Where(x => x.Name == methodInfo.Name)
                .Where(x => x.GetGenericArguments().All(arg =>
                {
                    return methodGenArgs.Any(y => y.Name == arg.Name);
                }))
                .First();

            targetMethod = targetMethod.MakeGenericMethod(genArgs);

            IList<TSource> list = new List<TSource>();
            Expression constant = Expression.Constant(list.AsQueryable(), typeof(IQueryable<TSource>));
            Expression call = Expression.Call(targetMethod, constant, Expression.Constant(count));

            return new QueryProvider<TSource>(source).CreateQuery<TSource>(call);
        }

        /// <summary>
        /// Take for IQueryable
        /// </summary>
        public static IQueryable<TSource> Take<TSource>(this IQueryContext<TSource> source, int count)
        {
            MethodInfo methodInfo = (MethodInfo)MethodInfo.GetCurrentMethod();
            var genArgs = new[] { typeof(TSource) };

            BindingFlags flags = BindingFlags.Static | BindingFlags.Public;

            var methodGenArgs = methodInfo.GetGenericArguments();

            var targetMethod = typeof(Enumerable).GetMethods(flags)
                .Where(x => x.Name == methodInfo.Name)
                .Where(x => x.GetGenericArguments().All(arg =>
                {
                    return methodGenArgs.Any(y => y.Name == arg.Name);
                }))
                .First();

            targetMethod = targetMethod.MakeGenericMethod(genArgs);

            IList<TSource> list = new List<TSource>();
            Expression constant = Expression.Constant(list.AsQueryable(), typeof(IQueryable<TSource>));
            Expression call = Expression.Call(targetMethod, constant, Expression.Constant(count));

            return new QueryProvider<TSource>(source).CreateQuery<TSource>(call);
        }
    }
}
