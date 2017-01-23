using System;

namespace Intuit.Ipp.LinqExtender.Ast
{
    
    /// <summary>
    /// Reprensents Logical blocks.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LogicalExpression : Expression
    {
        /// <summary>
        /// Intializes a new instance of the <see cref="LogicalExpression"/> class.
        /// </summary>
        /// <param name="@operator">Logical operator</param>
        internal LogicalExpression(LogicalOperator @operator)
        {
            this.@operator = @operator;
        }

        /// <summary>
        /// Gets the left expression.
        /// </summary>
        public Expression Left
        {
            get
            {
                return left;
            }
            internal set
            {
                left = value;
            }
        }

        /// <summary>
        /// Gets the right exprssison.
        /// </summary>
        public Expression Right
        {
            get
            {
                return right;
            }
            internal set
            {
                right = value;
            }
        }

        /// <summary>
        /// Gets value that indicates that the current is a child expression.
        /// </summary>
        public bool IsChild
        {
            get
            {
                return isChild;
            }
            internal set
            {
                isChild = value;
            }
        }

        /// <summary>
        /// Gets the operator joining the left and right expression.
        /// </summary>
        public LogicalOperator Operator
        {
            get
            {
                return @operator;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LogicalExpression; }
        }

        private readonly LogicalOperator @operator;
        private bool isChild;
        private Expression left;
        private Expression right;
    }
}
