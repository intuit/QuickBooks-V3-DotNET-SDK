using System;
namespace Intuit.Ipp.LinqExtender
{
   
    /// <summary>
    /// Defines Different types of code entries
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public enum CodeType
    {
        /// <summary>
        /// Binary expression.
        /// </summary>
        BinaryExpression,

        /// <summary>
        /// Logical expression
        /// </summary>
        LogicalExpression,
        
        /// <summary>
        /// Simple expression
        /// </summary>
        TypeExpression,

        /// <summary>
        /// Member expression
        /// </summary>
        MemberExpression,

        /// <summary>
        /// Literal expression
        /// </summary>
        LiteralExpression,

        /// <summary>
        /// Lambda expression
        /// </summary>
        LambdaExpresion,

        /// <summary>
        /// Block expression
        /// </summary>
        BlockExpression,

        /// <summary>
        /// Orderby expression.
        /// </summary>
        OrderbyExpression,

        /// <summary>
        /// MethodCall Expression
        /// </summary>
        MethodCallExpression,

        SelectExpression,

        NotExpression
    }
}
