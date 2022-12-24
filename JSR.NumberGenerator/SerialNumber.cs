// <copyright file="SerialNumber.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

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
        /// Generate a formatted serial number for the current <see cref="DateTime"/>.
        /// </summary>
        /// <returns>A 12 digit serial number seperated by a hyphen (XXXXXX-XXXXXX).</returns>
        public static string GetFormattedSerialNumber()
        {
            return GetFormattedSerialNumber(DateTime.UtcNow);
        }

        /// <summary>
        /// Generate a formatted serial number.
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to generate the serial number from.</param>
        /// <returns>A 12 digit serial number, separated by a hyphen (XXXXXX-XXXXXX).</returns>
        public static string GetFormattedSerialNumber(DateTime date)
        {
            return GetFormattedSerialNumber(GetSerialNumber(date));
        }

        /// <summary>
        /// Generate a formatted serial number.
        /// </summary>
        /// <param name="serialNumber">A 12 digit <see cref="long"/> value serial number to format.</param>
        /// <returns>A 12 digit serial number, separated by a hyphen (XXXXXX-XXXXXX).</returns>
        public static string GetFormattedSerialNumber(long serialNumber)
        {
            return serialNumber.ToString().Insert(serialNumber.ToString().Length - 5, "-");
        }

        /// <summary>
        /// Increments a serial number.
        /// </summary>
        /// <param name="serialNumber">A formatted (XXXXXX-XXXXXX) <see cref="string"/> as a serial number to increment.</param>
        /// <returns>A 12 digit serial number, separated by a hyphen (XXXXXX-XXXXXX) incremented a value of 1 from the provided value.</returns>
        public static string IncrementFormattedSerialNumber(string serialNumber)
        {
            return GetFormattedSerialNumber(IncrementSerialNumber(GetSerialNumber(serialNumber)));
        }

        /// <summary>
        /// Generate a serial number as a long value for the current DateTime.
        /// </summary>
        /// <returns>A 12 digit long value serial number.</returns>
        public static long GetSerialNumber()
        {
            return GetSerialNumber(DateTime.Now);
        }

        /// <summary>
        /// Generate a serial number as a long value.
        /// </summary>
        /// <param name="date"><see cref="DateTime"/> to gernate the serial number from.</param>
        /// <returns>12 digit long value serial number.</returns>
        public static long GetSerialNumber(DateTime date)
        {
            long year = (date.Year % 1000) * 100000000;
            long day = (date.DayOfYear - 1) * SecondsInADay;
            long hour = date.Hour * SecondsInAnHour;
            long minute = date.Minute * SecondsInAMinute;
            long second = date.Second;

            return year + day + hour + minute + second;
        }

        /// <summary>
        /// Gets a serial number long value from a formatted string.
        /// </summary>
        /// <param name="serialNumber">A formatted ("XXXXXX-XXXXXX") <see cref="string"/> value representing a serial number.</param>
        /// <returns>12 digit long value serial number.</returns>
        public static long GetSerialNumber(string serialNumber)
        {
            if (serialNumber.Length != 13 || !serialNumber.Contains('-'))
            {
                throw new ArgumentOutOfRangeException($"The value {serialNumber} is not a valid formatted serial number.");
            }

            serialNumber = serialNumber.Replace("-", string.Empty);

            if (long.TryParse(serialNumber, out long result))
            {
                return result;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"The value {serialNumber} is not a valid formatted serial number.");
            }
        }

        /// <summary>
        /// Increments a serial number by 1.
        /// </summary>
        /// <param name="serialNumber">A 12 digit serial number.</param>
        /// <returns>A 12 digit serial number incremented by 1.</returns>
        public static long IncrementSerialNumber(long serialNumber)
        {
            return serialNumber++;
        }
    }
}
