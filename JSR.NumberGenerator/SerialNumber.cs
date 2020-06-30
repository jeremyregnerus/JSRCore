// <copyright file="SerialNumber.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;

namespace JSR.NumberGenerator
{
    /// <summary>
    /// This class generates serial numbers based on <see cref="DateTime"/>.
    /// </summary>
    public static class SerialNumber
    {
        /// <summary>
        /// Number of seconds in a minute.
        /// </summary>
        public const int SecondsInAMinute = 60;

        /// <summary>
        /// Number of minutes in an hour.
        /// </summary>
        public const int MinutesInAnHour = 60;

        /// <summary>
        /// Number of seconds in an hour.
        /// </summary>
        public const int SecondsInAnHour = SecondsInAMinute * MinutesInAnHour;

        /// <summary>
        /// Number of hours in a day.
        /// </summary>
        public const int HoursInADay = 24;

        /// <summary>
        /// Number of seconds in a day.
        /// </summary>
        public const int SecondsInADay = HoursInADay * SecondsInAnHour;

        /// <summary>
        /// Generate a serial number for the current <see cref="DateTime"/>.
        /// </summary>
        /// <returns>A 12 digit serial number seperated by a hyphen (XXXXXX-XXXXXX).</returns>
        public static string GetSerialNumber()
        {
            return GetSerialNumber(DateTime.UtcNow);
        }

        /// <summary>
        /// Generate a serial number for the provided <see cref="DateTime"/>.
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to generate a serial number from.</param>
        /// <returns>A 12 digit serial number, separated by a hyphen (XXXXXX-XXXXXX).</returns>
        public static string GetSerialNumber(DateTime date)
        {
            long year = (date.Year % 1000) * 100000000;
            long day = (date.DayOfYear - 1) * SecondsInADay;
            long hour = date.Hour * SecondsInAnHour;
            long minute = date.Minute * SecondsInAMinute;
            long second = date.Second;

            long secondOfYear = year + day + hour + minute + second;

            string serialNumber = secondOfYear.ToString();

            return serialNumber.Insert(serialNumber.Length - 5, "-");
        }
    }
}
