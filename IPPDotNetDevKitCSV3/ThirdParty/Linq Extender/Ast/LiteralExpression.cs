using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Intuit.Ipp.LinqExtender.Ast
{
    /// <summary>
    /// Represents the extracted value of a query item.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class LiteralExpression : Expression
    {
        
        internal LiteralExpression(Type type, object value)
        {
            this.type = new TypeReference(type);
            this.value = value;
        }

        /// <summary>
        /// Gets the target type.
        /// </summary>
        public TypeReference Type
        {
            get
            {
                return type;
            }
        }

        /// <summary>
        /// Gets the value that is evaulated from linq query.
        /// </summary>
        public object Value
        {
            get
            {
                return value;
            }
        }

        /// <summary>
        /// Gets the type of the expression.
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.LiteralExpression; }
        }

        private object value;
        private TypeReference type;
    }
}
