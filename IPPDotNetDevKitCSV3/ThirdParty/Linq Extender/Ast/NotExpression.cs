// -----------------------------------------------------------------------
// <copyright file="NotExpression.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------
using System;

namespace Intuit.Ipp.LinqExtender.Ast
{
  
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class NotExpression : Ast.Expression
    {
        public string PropertyName { get; set; }

        /// <summary>
        /// Member NotExpression
        /// </summary>
        public NotExpression(string propName)
        {
            this.PropertyName = propName;
        }

        /// <summary>
        /// Override Member CodeType
        /// </summary>
        public override CodeType CodeType
        {
            get { return CodeType.NotExpression; }
        }
    }
}
