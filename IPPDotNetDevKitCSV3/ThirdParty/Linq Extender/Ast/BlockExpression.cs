using System;
using System.Collections.Generic;

namespace Intuit.Ipp.LinqExtender.Ast
{
  
    /// <summary>
    /// Combines a number expressions sequentially
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class BlockExpression : Ast.Expression
    {
        internal BlockExpression()
        {
            expressions = new List<Expression>();
        }

        /// <summary>
        /// Gets the list of expression associated with the current query.
        /// </summary>
        public IList<Expression> Expressions
        {
            get
            {
                return expressions;
            }
        }

        /// <summary>
        /// Gets a value indicating the type of expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.BlockExpression; }
        }

        private IList<Expression> expressions;
    }
}
