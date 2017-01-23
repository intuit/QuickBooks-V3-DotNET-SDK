////*********************************************************
// <copyright file="DateHelper.cs" company="Intuit">
//     Copyright (c) Intuit. All rights reserved.
// </copyright>
// <summary>This file contains Helper functions for date-related problems.</summary>
////*********************************************************
namespace Intuit.Ipp.Utility
{
    using System;
    using Intuit.Ipp.Utility.Properties;

    /// <summary>   
    /// Helper functions for date-related problems.
    /// </summary>
    public static class DateHelper
    {
        /// <summary>
        /// Defined as January 1st, 1970 UTC. Used in various ways as a reference point for date arithmetic.
        /// </summary>
        /// <see>GetMillisecondsSince01011970UTC</see>
        private static readonly DateTime EpochJanFirst1970Utc = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        /// <summary>
        /// Assuming the value represents a date or date/time returned by a query, parses the timestamp and converts it to a local date/time.
        /// </summary>
        /// <param name="value">the time stamp returned by the query</param>
        /// <returns>the local time or date represented by the time stamp, or DateTime.MinValue if not a valid time stamp</returns>
        public static DateTime ParseDateTimeField(string value)
        {
            long dateValue;
            if (long.TryParse(value, out dateValue))
            {
                return GetLocalDateFromQuickBaseDate(dateValue);
            }

            return DateTime.MinValue;
        }

        /// <summary>
        /// A so-called "QuickBaseDate" is a date used by Intuit QuickBase (and WorkPlace, if the app uses QuickBase as the underlying data store),
        /// which is stored as the number of milliseconds since 1/1/1970 00:00:00 UTC.
        /// This function returns the local equivalent of that date.
        /// </summary>
        /// <param name="quickBaseDate">a date returned from QuickBase as part of a query</param>
        /// <returns>the local equivalent of that date as a DateTime object</returns>
        private static DateTime GetLocalDateFromQuickBaseDate(long quickBaseDate)
        {
            return EpochJanFirst1970Utc.AddMilliseconds(quickBaseDate).ToLocalTime();
        }
    }
}
