using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IDSLinq
{
    public class QueryResult<T> : IOrderedQueryable<T>
    {
        public Expression Expression
        {
            get;
            private set;
        }

        public IQueryProvider Provider
        {
            get;
            private set;
        }
        
        public QueryResult()
        {
            Provider = new QueryProvider();
            Expression = Expression.Constant(this);
        }

        public QueryResult(QueryProvider provider, Expression expression)
        {
            if (provider == null)
            {
                throw new ArgumentNullException("provider");
            }

            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if (!typeof(IQueryable<T>).IsAssignableFrom(expression.Type))
            {
                throw new ArgumentOutOfRangeException("expression");
            }

            Provider = provider;
            Expression = expression;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (Provider.Execute<IEnumerable<T>>(Expression)).GetEnumerator();            
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public Type ElementType
        {
            get { throw new NotImplementedException(); }
        }
    }

    //public class QueryResultExtn
    //{
    //    public static IEnumerable<T> Query(this QueryResult<T> exp)
    //    {
    //        return null;
    //    }
    //}
}
