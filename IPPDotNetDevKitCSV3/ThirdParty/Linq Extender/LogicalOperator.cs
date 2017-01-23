using System;
namespace Intuit.Ipp.LinqExtender
{
    
    /// <summary>
    /// Type of operator used in where clause.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public enum LogicalOperator
    {
        /// <summary>
        /// Used for first item in where entry
        /// </summary>
        None,
        /// <summary>
        /// Used for <![CDATA[ && ]]>
        /// </summary>
        And,
        /// <summary>
        /// Used for  ||
        /// </summary>
        Or
    }
}