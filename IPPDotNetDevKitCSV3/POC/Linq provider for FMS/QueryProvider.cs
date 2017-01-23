using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Linq.Expressions;

namespace IDSLinq
{
    public class QueryProvider : IQueryProvider
    {
        IQueryable<TResult> IQueryProvider.CreateQuery<TResult>(Expression expression)
        {
            return new QueryResult<TResult>(this, expression);
        }


        public IQueryable CreateQuery(Expression expression)
        {
            throw new NotImplementedException();
        }

        public TResult Execute<TResult>(Expression expression)
        {
            bool IsEnumerable = (typeof(TResult).Name == "IEnumerable`1");

            return (TResult)QueryExecuter.Execute(expression, IsEnumerable);
        }

        public object Execute(Expression expression)
        {
            throw new NotImplementedException();
        }
    }
}
