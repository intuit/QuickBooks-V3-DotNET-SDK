using System;

namespace Intuit.Ipp.LinqExtender.Ast
{
    
    /// <summary>
    /// Defines lambda expression.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LambdaExpression : Expression 
    {
        /// <summary>
        /// Initailizes a new instance of the <see cref="LambdaExpression"/> class.
        /// </summary>
        /// <param name="type">Target object type.</param>
        public LambdaExpression(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the underlying type of the expression.
        /// </summary>
        public Type Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the body of the expression.
        /// </summary>
        public Expression Body
        {
            get
            {
                return body;
            }
            internal set
            {
                body = value;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LambdaExpresion; }
        }

        Type type;
        Expression body;
     
    }
}
