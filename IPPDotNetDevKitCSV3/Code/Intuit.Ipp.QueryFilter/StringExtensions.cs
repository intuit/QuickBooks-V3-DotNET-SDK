////********************************************************************
// <copyright file="StringExtensions.cs" company="Intuit">
/*******************************************************************************
 * Copyright 2016 Intuit
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *     http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 *******************************************************************************/
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
