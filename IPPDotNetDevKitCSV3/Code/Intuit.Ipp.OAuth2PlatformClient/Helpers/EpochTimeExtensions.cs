// Copyright (c) Intuit All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using System;

namespace Intuit.Ipp.OAuth2PlatformClient
{
    /// <summary>
    /// Helper class for Epoch time conversions
    /// </summary>
    public static class EpochTimeExtensions
    {

        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        /// <param name="dateTime">dateTime</param>
        /// <returns>long</returns>        
        public static long ToEpochTime(this DateTime dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }

        /// <summary>
        /// Converts the given date value to epoch time.
        /// </summary>
        /// <param name="dateTime">dateTime Offset value</param>
        /// <returns>long</returns>  
        public static long ToEpochTime(this DateTimeOffset dateTime)
        {
            var date = dateTime.ToUniversalTime();
            var ticks = date.Ticks - new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).Ticks;
            var ts = ticks / TimeSpan.TicksPerSecond;
            return ts;
        }


        /// <summary>
        /// Converts the given epoch time to a <see cref="DateTime"/> with <see cref="DateTimeKind.Utc"/> kind.
        /// </summary>
        /// <param name="intDate"></param>
        /// <returns>DateTime</returns>
        public static DateTime ToDateTimeFromEpoch(this long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddTicks(timeInTicks);
        }

        /// <summary>
        /// Converts the given epoch time to a UTC <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="intDate"></param>
        /// <returns>DateTimeOffset</returns>
        public static DateTimeOffset ToDateTimeOffsetFromEpoch(this long intDate)
        {
            var timeInTicks = intDate * TimeSpan.TicksPerSecond;
            return new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero).AddTicks(timeInTicks);
        }
    }
}