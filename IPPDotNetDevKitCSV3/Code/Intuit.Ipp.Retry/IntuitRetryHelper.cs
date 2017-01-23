////*********************************************************
// <copyright file="IntuitRetryHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains helper methods for Retry.</summary>
////*********************************************************

namespace Intuit.Ipp.Retry
{
    using System;
    using System.Globalization;
    using Intuit.Ipp.Retry.Properties;

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
