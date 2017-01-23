using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Collections;
using Intuit.Ipp.LinqExtender.Abstraction;
using Intuit.Ipp.LinqExtender.Collection;

namespace Intuit.Ipp.LinqExtender
{
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    internal class ProjectedQuery<T, S> : ReadOnlyQueryCollection<S>, IQueryProvider, IQueryable<S>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectedQuery{T,S}"/> class.
        /// </summary>
        /// <param name="expression">The expression.</param>
        /// <param name="parent">The query.</param>
        public ProjectedQuery(Expression expression, Query<T> parent)
        {
            this.expression = expression;
            this.parent = parent;
        }
       
        #region IEnumerable<TS> Members

        /// <summary>
        /// Returns an enumerator that iterates through the collection.
        /// </summary>
        /// <returns>
        /// A <see cref="T:System.Collections.Generic.IEnumerator`1"/> that can be used to iterate through the collection.
        /// </returns>
        public IEnumerator<S> GetEnumerator()
        {
            this.ExectuteParent();
            return Items.GetEnumerator();
        }

        public override Expression VisitMethodCall(MethodCallExpression expression)
        {
            foreach (var exp in expression.Arguments)
                  this.Visit(exp);
       
            return expression;
        }

        public override Expression VisitLamda(LambdaExpression lambdaExpression)
        {
            lambda = lambdaExpression;

            return expression;
        }

        private void ExectuteParent()
        {
            Items.Clear();

            this.Visit(this.expression);

            try
            {
                ((Expression<Func<T, S>>)lambda).Compile();
            }
            catch
            {
                // do nothing for now.
            }

            var result = parent.Select(((Expression<Func<T, S>>)lambda).Compile());
                
            Items.AddRange(result);
        }

        #endregion

        #region IEnumerable Members

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (this as IEnumerable<S>).GetEnumerator();
        }


        #endregion

        #region IQueryable Members

        /// <summary>
        /// Gets the type of the element(s) that are returned when the expression tree associated with this instance of <see cref="T:System.Linq.IQueryable"/> is executed.
        /// </summary>
        /// <value></value>
        /// <returns>A <see cref="T:System.Type"/> that represents the type of the element(s) that are returned when the expression tree associated with this object is executed.</returns>
        public Type ElementType
        {
            get { return typeof(S); }
        }

        /// <summary>
        /// Gets the expression tree that is associated with the instance of <see cref="T:System.Linq.IQueryable"/>.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Linq.Expressions.Expression"/> that is associated with this instance of <see cref="T:System.Linq.IQueryable"/>.</returns>
        public Expression Expression
        {
            get { return expression; }
        }

        /// <summary>
        /// Gets the query provider that is associated with this data source.
        /// </summary>
        /// <value></value>
        /// <returns>The <see cref="T:System.Linq.IQueryProvider"/> that is associated with this data source.</returns>
        public IQueryProvider Provider
        {
            get { return this; }
        }

        #endregion

        #region IQueryProvider Members

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return parent.CreateQuery<TElement>(expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return parent.CreateQuery<S>(expression);
        }

        public TResult Execute<TResult>(Expression expression)
        {
            return (TResult)this.ExecuteNonGeneric<TResult>(expression);
        }

        public object Execute(Expression expression)
        {
            if (expression is MethodCallExpression)
            {
                var mCallExp = (MethodCallExpression)expression;
                // when first , last or single is called 
                string methodName = mCallExp.Method.Name;

                if (methodName.Equals("ToIdsQuery", StringComparison.OrdinalIgnoreCase))
                {
                    return (string)this.ExecuteNonGeneric<string>(expression);
                }
            }

            return (S)this.ExecuteNonGeneric<S>(expression);
        }

        public object ExecuteNonGeneric<TResult>(Expression expression)
        {
            parent.isToIdsQueryMethod = false;

            if (expression is MethodCallExpression)
            {
                var mCallExp = (MethodCallExpression)expression;
                string methodName = mCallExp.Method.Name;
                if (methodName.Equals("ToIdsQuery", StringComparison.OrdinalIgnoreCase))
                {
                    parent.isToIdsQueryMethod = true;
                }
                else
                {
                    parent.isToIdsQueryMethod = false;
                }
            }

            ExectuteParent();

            if (expression is MethodCallExpression)
            {
                var mCallExp = (MethodCallExpression)expression;
                // when first , last or single is called 
                string methodName = mCallExp.Method.Name;

                if (methodName.Equals("ToIdsQuery", StringComparison.OrdinalIgnoreCase))
                {
                    return parent.idsQuery;
                }

                /* Try for Generics Results */
                Type itemType = typeof(IMethodCall<TResult>);

                object obj = Utility.InvokeMethod(methodName, itemType, this);

                /* Try for Non Generics Result */
                if (obj == null)
                {
                    itemType = typeof(IMethodCall);
                    obj = Utility.InvokeMethod(methodName, itemType, this);
                }
                return obj;

            }
            return null;
        }


        #endregion

        private readonly Query<T> parent;
        private LambdaExpression lambda;
        private readonly Expression expression;
    }
}

