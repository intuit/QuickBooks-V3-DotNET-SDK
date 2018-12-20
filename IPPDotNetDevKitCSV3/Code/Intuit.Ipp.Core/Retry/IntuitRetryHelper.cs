////*********************************************************
// <copyright file="IntuitRetryHelper.cs" company="Intuit">
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
 * See the License for the specific language governing permiC:\Users\nshrivastava\Documents\Git\QuickBooks-V3-DotNET-SDK\IPPDotNetDevKitCSV3\Code\Intuit.Ipp.Core\Retry\IntuitRetryHelper.csssions and
 * limitations under the License.
 *******************************************************************************/
// <summary>This file contains helper methods for Retry.</summary>
////*********************************************************
//namespace Intuit.Ipp.Retry  
namespace Intuit.Ipp.Core
{
    using System;
    using System.Globalization;
    //using Intuit.Ipp.Retry.Properties; 
    using Intuit.Ipp.Core.Properties;

    /// <summary>
    /// Class contains Helper Methods for Services.
    /// </summary>
    internal static class IntuitRetryHelper
    {
        /// <summary>
        /// Checks an argument to ensure it isn't null.
        /// </summary>
        /// <param name="argumentValue">The argument value to check.</param>
        /// <param name="argumentName">The name of the argument.</param>
        /// /// <returns>The return value should be ignored. It is intended to be used only when validating arguments during instance creation (e.g. when calling base constructor).</returns>
        public static bool IsArgumentNull(object argumentValue, string argumentName)
        {
            if (argumentValue == null)
            {
                throw new ArgumentNullException(argumentName);
            }

            return true;
        }

        /// <summary>
        /// Checks an argument to ensure that its value is not negative.
        /// </summary>
        /// <param name="argumentValue">The <see cref="System.Int32"/> value of the argument.</param>
        /// <param name="argumentName">The name of the argument for diagnostic purposes.</param>
        public static void ArgumentNotNegativeValue(int argumentValue, string argumentName)
        {
            if (argumentValue < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, string.Format(CultureInfo.CurrentCulture, Resources.ArgumentCannotBeNegative, argumentName));
            }
        }

        /// <summary>
        /// Checks an argument to ensure that its value is not negative.
        /// </summary>
        /// <param name="argumentValue">The <see cref="System.Int64"/> value of the argument.</param>
        /// <param name="argumentName">The name of the argument for diagnostic purposes.</param>
        public static void ArgumentNotNegativeValue(long argumentValue, string argumentName)
        {
            if (argumentValue < 0)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, string.Format(CultureInfo.CurrentCulture, Resources.ArgumentCannotBeNegative, argumentName));
            }
        }

        /// <summary>
        /// Checks an argument to ensure that its value doesn't exceed the specified ceiling baseline.
        /// </summary>
        /// <param name="argumentValue">The <see cref="System.Double"/> value of the argument.</param>
        /// <param name="ceilingValue">The <see cref="System.Double"/> ceiling value of the argument.</param>
        /// <param name="argumentName">The name of the argument for diagnostic purposes.</param>
        public static void ArgumentNotGreaterThan(double argumentValue, double ceilingValue, string argumentName)
        {
            if (argumentValue > ceilingValue)
            {
                throw new ArgumentOutOfRangeException(argumentName, argumentValue, string.Format(CultureInfo.CurrentCulture, Resources.ArgumentCannotBeGreaterThanBaseline, argumentName, ceilingValue));
            }
        }
    }
}
