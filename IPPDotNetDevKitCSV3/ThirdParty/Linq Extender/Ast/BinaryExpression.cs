using System;
namespace Intuit.Ipp.LinqExtender.Ast
{
    
    /// <summary>
    /// Represents the binary operation.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class BinaryExpression : Expression
    {
        private BinaryOperator @operator;
        private Expression left;
        private Expression right;

        /// <summary>
        /// Initalizes a new instance of the <see cref="BinaryExpression"/> class.
        /// </summary>
        /// <param name="@operator">Target operator</param>
        internal BinaryExpression(BinaryOperator @operator)
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
        /// Gets the right expression
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
        /// Gets the binary operator.
        /// </summary>
        public BinaryOperator Operator
        {
            get
            {
                return @operator;
            }
        }

        /// <summary>
        /// Gets a value indicating the type of expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.BinaryExpression; }
        }
    }
}
