// -----------------------------------------------------------------------
// <copyright file="SelectExpression.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace Intuit.Ipp.LinqExtender.Ast
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;


    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public class SelectExpression : Ast.Expression
    {
        public string PropertyName { get; set; }

        /// <summary>
        /// Sets PropertyName
        /// </summary>
        public SelectExpression(string propName)
        {
            this.PropertyName = propName;
        }

        public override CodeType CodeType
        {
            get { return CodeType.SelectExpression; }
        }
    }
}
