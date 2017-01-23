using System;
namespace Intuit.Ipp.LinqExtender
{

    /// <summary>
    /// Represents the relational query operator equavalent.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public enum BinaryOperator
    {
        /// <summary>
        /// Eqavalent of "=="
        /// </summary>
        Equal = 0,
        /// <summary>
        /// Eqavalent of ">"
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Eqavalent of <![CDATA[ < ]]>
        /// </summary>
        LessThan,
        /// <summary>
        /// Eqavalent of ">="
        /// </summary>
        GreaterThanEqual,
        /// <summary>
        /// Eqavalent of <![CDATA[<=]]>
        /// </summary>
        LessThanEqual,
        /// <summary>
        /// Eqavalent of "!="
        /// </summary>
        NotEqual,
        /// <summary>
        /// Defines the Contains operation in expression.
        /// </summary>
        Contains,
        Not,
        /// <summary>
        /// Default value for first where clause item
        /// </summary>
        NotApplicable

    }
}