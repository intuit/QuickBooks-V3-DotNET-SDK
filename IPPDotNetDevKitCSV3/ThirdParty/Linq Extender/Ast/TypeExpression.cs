using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Ast
{
   
    /// <summary>
    /// Represents the target type.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class TypeExpression : Expression
    {
        /// <summary>
        /// Initalizes a new instance of the <see cref="TypeExpression"/> class.
        /// </summary>
        /// <param name="type">Target type</param>
        internal TypeExpression(Type type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the underlying type.
        /// </summary>
        public TypeReference Type
        {
            get
            {
                return new TypeReference(this.type);
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.TypeExpression; }
        }

        private Type type;
   
    }
}
