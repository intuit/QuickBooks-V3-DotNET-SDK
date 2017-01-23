////********************************************************************
// <copyright file="StringExtensions.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>Contains extension methods for the string data type.</summary>
////********************************************************************
using System;
namespace Intuit.Ipp.QueryFilter
{
    using System.Linq;


    /// <summary>
    /// Contains extension methods for the string data type.
    /// </summary>
    [Obsolete("Deprecated. Use QueryService->ExecuteIdsQuery")]
    public static class StringExtensions
    {
        /// <summary>
        /// Determines whether a sequence contains a specified element by using the default equality comparer.
        /// </summary>
        /// <param name="value">A sequence in which to locate a value.</param>
        /// <param name="values">The value to locate in the sequence.</param>
        /// <returns>True if the source sequence contains an element that has the specified value otherwise, false.</returns>
        public static bool In(this string value, string[] values)
        {
            if (values.Contains(value))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes unwanted whitespaces from the string.
        /// </summary>
        /// <param name="value">The value to remove the whitespaces from.</param>
        /// <returns>String with single whitespace between elements.</returns>
        public static string RemoveWhiteSpaces(this string value)
        {
            return System.Text.RegularExpressions.Regex.Replace(value, @"\s+", " ");
        }
    }
}
