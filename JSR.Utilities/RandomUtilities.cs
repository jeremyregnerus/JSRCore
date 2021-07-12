// <copyright file="RandomUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Linq;

namespace JSR.Utilities
{
    /// <summary>
    /// Generates and manipulates random values.
    /// </summary>
    public static class RandomUtilities
    {
        /// <summary>
        /// Generates a random value of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <returns>A random value of the specified type.</returns>
        public static T GetRandom<T>()
        {
            return GetRandom(typeof(T), null);
        }

        /// <summary>
        /// Generates a random value of the specified type.
        /// </summary>
        /// <typeparam name="T">Type of value to generate.</typeparam>
        /// <param name="currentValue">Current value to not match.</param>
        /// <returns>A random value of the specified type.</returns>
        public static T GetRandom<T>(T currentValue)
        {
            switch (typeof(T))
            {
                case Type t when t == typeof(string):
                    return (T)(object)GetRandomString((string)(object)currentValue);
                case Type t when t.IsEnum:
                    return (T)(object)GetRandomEnum((dynamic)currentValue);
                case Type t when t == typeof(bool):
                    return (T)(object)GetRandomBoolean((bool)(object)currentValue);
                case Type t when t == typeof(int):
                    return (T)(object)GetRandomInteger((int)(object)currentValue);
                case Type t when t == typeof(DateTime):
                    return (T)(object)GetRandomDateTime((DateTime)(object)currentValue);
                case Type t when t == typeof(double):
                    return (T)(object)GetRandomDouble((double)(object)currentValue);
                case Type t when t == typeof(decimal):
                    return (T)(object)GetRandomDecimal((decimal)(object)currentValue);
                default:
                    throw new ArgumentException($"The type {currentValue.GetType()} is an unsupported value type for the {nameof(GetRandom)} method.", nameof(currentValue));
            }
        }

        /// <summary>
        /// Generates a random value of the specified type.
        /// </summary>
        /// <param name="type">Type of value to generate.</param>
        /// <returns>A random value of the specified type.</returns>
        public static dynamic GetRandom(Type type)
        {
            return GetRandom(type, null);
        }

        /// <summary>
        /// Generates a random value of the provided type.
        /// </summary>
        /// <param name="type">Type of value to generate.</param>
        /// <param name="currentValue">Current value to not reproduce.</param>
        /// <returns>A random Value of the specified type.</returns>
        public static dynamic GetRandom(Type type, dynamic currentValue)
        {
            if (currentValue != null && type != currentValue.GetType())
            {
                throw new ArgumentOutOfRangeException(nameof(currentValue), $"The type {type} does not match the type {currentValue.GetType()} of {nameof(currentValue)}.");
            }

            switch (type)
            {
                case Type t when t == typeof(string):
                    return currentValue == null ? (dynamic)GetRandomString() : GetRandomString(currentValue);
                case Type t when t.IsEnum:
                    return currentValue == null ? GetRandomEnum(type) : GetRandomEnum(currentValue);
                case Type t when t == typeof(bool):
                    return currentValue == null ? (dynamic)GetRandomBoolean() : GetRandomBoolean(currentValue);
                case Type t when t == typeof(int):
                    return currentValue == null ? (dynamic)GetRandomInteger() : GetRandomInteger(currentValue);
                case Type t when t == typeof(DateTime):
                    return currentValue == null ? (dynamic)GetRandomDateTime() : GetRandomDateTime(currentValue);
                case Type t when t == typeof(double):
                    return currentValue == null ? (dynamic)GetRandomDouble() : GetRandomDouble(currentValue);
                case Type t when t == typeof(decimal):
                    return currentValue == null ? (dynamic)GetRandomDecimal() : GetRandomDecimal(currentValue);
                default:
                    throw new ArgumentException($"The type {type} is an unsupported value type for the {nameof(GetRandom)} method.", nameof(type));
            }
        }

        /// <summary>
        /// Generates a random <see cref="bool"/>.
        /// </summary>
        /// <returns>A random <see cref="bool"/>.</returns>
        public static bool GetRandomBoolean()
        {
            return new Random().NextBool();
        }

        /// <summary>
        /// Generates a random <see cref="bool"/> different than the provided value.
        /// </summary>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>Random <see cref="bool"/>.</returns>
        public static bool GetRandomBoolean(bool? currentValue)
        {
            do
            {
                bool newVal = GetRandomBoolean();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="DateTime"/>.
        /// </summary>
        /// <returns>A random <see cref="DateTime"/>.</returns>
        public static DateTime GetRandomDateTime()
        {
            return new Random().NextDateTime();
        }

        /// <summary>
        /// Generates a random <see cref="DateTime"/> different than the provided value.
        /// </summary>
        /// <param name="currentValue">Current value to not match.</param>
        /// <returns>Random <see cref="DateTime"/>.</returns>
        public static DateTime GetRandomDateTime(DateTime? currentValue)
        {
            do
            {
                DateTime newVal = new Random().NextDateTime();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="decimal"/>.
        /// </summary>
        /// <returns>A random <see cref="decimal"/>.</returns>
        public static decimal GetRandomDecimal()
        {
            return new Random().NextDecimal();
        }

        /// <summary>
        /// Generates a random <see cref="decimal"/> different than the provided value.
        /// </summary>
        /// <param name="currentValue">Current value to not match.</param>
        /// <returns>Random <see cref="decimal"/>.</returns>
        public static decimal GetRandomDecimal(decimal currentValue)
        {
            do
            {
                decimal newVal = new Random().NextDecimal();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="double"/>.
        /// </summary>
        /// <returns>A random <see cref="double"/>.</returns>
        public static double GetRandomDouble()
        {
            return new Random().NextDouble();
        }

        /// <summary>
        /// Generates a random <see cref="double"/> different than the provided value.
        /// </summary>
        /// <param name="currentValue">Current value to not match.</param>
        /// <returns>A random <see cref="double"/>.</returns>
        public static double GetRandomDouble(double currentValue)
        {
            do
            {
                double newVal = new Random().NextDouble();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random Enum value.
        /// </summary>
        /// <typeparam name="T">Type of enum value to generate.</typeparam>
        /// <returns>A random Enum value.</returns>
        public static T GetRandomEnum<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length));
        }

        /// <summary>
        /// Generates a random Enum value.
        /// </summary>
        /// <param name="enumType">Type of enum to generate.</param>
        /// <returns>A random enum.</returns>
        public static dynamic GetRandomEnum(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException(nameof(enumType));
            }

            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumType)} is not an enum.", nameof(enumType));
            }

            Array values = Enum.GetValues(enumType);
            return values.GetValue(new Random().Next(values.Length));
        }

        /// <summary>
        /// Generates a random Enum value different than the one specified.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Enum"/> to generate a random value for.</typeparam>
        /// <param name="currentValue">Current value not to match.</param>
        /// <returns>A random <see cref="Enum"/> value.</returns>
        public static T GetRandomEnum<T>(T currentValue) where T : Enum
        {
            if (typeof(T) != currentValue.GetType())
            {
                throw new ArgumentException($"The generic type {typeof(T)} does not match the object {nameof(currentValue)} type of {currentValue.GetType()}.");
            }

            do
            {
                T newVal = GetRandomEnum<T>();

                if (!newVal.Equals(currentValue))
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="int"/>.
        /// </summary>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger()
        {
            return new Random().Next();
        }

        /// <summary>
        /// Generates a random <see cref="int"/> different from the provided value.
        /// </summary>
        /// <param name="currentValue">Current value not to match.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue)
        {
            do
            {
                int newVal = new Random().NextInt32();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="int"/> different from the specified value, for a specific range.
        /// </summary>
        /// <param name="currentValue">Current value to not match.</param>
        /// <param name="maxValue">Range the integer should fall within.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue, int maxValue)
        {
            do
            {
                int newVal = new Random().Next(maxValue);

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="int"/> different from the specified value, for a specific range.
        /// </summary>
        /// <param name="currentValue">Current value to not match.</param>
        /// <param name="minValue">Minimum value of the range.</param>
        /// <param name="maxValue">Maximum value of the range.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue, int minValue, int maxValue)
        {
            do
            {
                int newVal = new Random().Next(minValue, maxValue);

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Generates a random <see cref="string"/>.
        /// </summary>
        /// <returns>A random <see cref="string"/> from 4 to 20 characters long.</returns>
        public static string GetRandomString()
        {
            return new Random().NextString();
        }

        /// <summary>
        /// Generates a random string different from the provided value.
        /// </summary>
        /// <param name="currentValue">Current value not to match.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string GetRandomString(string currentValue)
        {
            do
            {
                string newVal = new Random().NextString();

                if (newVal != currentValue)
                {
                    return newVal;
                }
            } while (true);
        }

        /// <summary>
        /// Gernates a random <see cref="string"/>.
        /// </summary>
        /// <param name="length">Number of characters / letters the string should contain.</param>
        /// <returns>A random <see cref="string"/> with the specified number of characters.</returns>
        public static string GetRandomString(int length)
        {
            return new Random().NextString(length);
        }

        /// <summary>
        /// Returns a random <see cref="bool"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="bool"/>.</returns>
        public static bool NextBool(this Random random)
        {
            return random.Next(2) == 0;
        }

        /// <summary>
        /// Returns a random <see cref="DateTime"/> value.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="DateTime"/>.</returns>
        public static DateTime NextDateTime(this Random random)
        {
            DateTime from = new DateTime(1900, 1, 1);
            DateTime to = new DateTime(2050, 12, 31);

            TimeSpan range = to - from;

            TimeSpan randomRange = new TimeSpan((long)(random.NextDouble() * range.Ticks));

            return from + randomRange;
        }

        /// <summary>
        /// Returns a random <see cref="decimal"/> value.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="decimal"/>.</returns>
        public static decimal NextDecimal(this Random random)
        {
            byte scale = (byte)random.Next(29);
            bool sign = random.Next(2) == 1;

            return new decimal(random.NextInt32(), random.NextInt32(), random.NextInt32(), sign, scale);
        }

        /// <summary>
        /// Returns a random 32bit <see cref="int"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random 32 bit <see cref="int"/>.</returns>
        public static int NextInt32(this Random random)
        {
            unchecked
            {
                int firstBit = random.Next(0, 1 << 4) << 28;
                int lastBit = random.Next(0, 1 << 28);

                return firstBit | lastBit;
            }
        }

        /// <summary>
        /// Returns a random <see cref="string"/> between 4 and 20 characters long.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="string"/> between 4 and 20 characters long.</returns>
        public static string NextString(this Random random)
        {
            return random.NextString(random.Next(4, 20));
        }

        /// <summary>
        /// Returns a random <see cref="string"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="length">Length of the string in characters.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string NextString(this Random random, int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
