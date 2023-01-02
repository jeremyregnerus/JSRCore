// <copyright file="RandomUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;

namespace JSR.Utilities
{
    /// <summary>
    /// Generates and manipulates random values.
    /// </summary>
    public static class RandomUtilities
    {
        private const string CHARSTRING = "$%#@!*abcdefghijklmnopqrstuvwxyz1234567890?;:ABCDEFGHIJKLMNOPQRSTUVWXYZ^&";
        private static readonly char[] CHARS = CHARSTRING.ToCharArray();

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
        public static T GetRandom<T>([DisallowNull] T currentValue)
        {
            return GetRandom(currentValue.GetType(), currentValue);
        }

        /// <summary>
        /// Generates a random value of the specified type.
        /// </summary>
        /// <param name="type">Type of value.</param>
        /// <returns>A random value of the specified type.</returns>
        public static dynamic GetRandom(Type type)
        {
            return GetRandom(type, null);
        }

        /// <summary>
        /// Generates a random value of the provided type.
        /// </summary>
        /// <param name="type">Type of value.</param>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>A random Value of the specified type.</returns>
        public static dynamic GetRandom(Type type, dynamic? currentValue)
        {
            return type switch
            {
                Type t when t == typeof(bool) => currentValue == null ? GetRandomBoolean() : GetRandomBoolean(currentValue),
                Type t when t == typeof(char) => currentValue == null ? GetRandomChar() : GetRandomChar(currentValue),
                Type t when t == typeof(DateTime) => currentValue == null ? GetRandomDateTime() : GetRandomDateTime(currentValue),
                Type t when t == typeof(decimal) => currentValue == null ? GetRandomDecimal() : GetRandomDecimal(currentValue),
                Type t when t == typeof(double) => currentValue == null ? GetRandomDouble() : GetRandomDouble(currentValue),
                Type t when t == typeof(int) => currentValue == null ? GetRandomInteger() : GetRandomInteger(currentValue),
                Type t when t == typeof(string) => currentValue == null ? GetRandomString() : GetRandomString(currentValue),
                Type t when t.IsEnum => currentValue == null ? GetRandomEnum(type) : GetRandomEnum(currentValue),
                Type t when t.IsClass => ObjectUtilities.CreateInstanceWithRandomValues(type),
                Type t when t.IsValueType && !t.IsEnum => ObjectUtilities.CreateInstanceWithRandomValues(type),
                _ => throw new ArgumentException($"The type {type} is an unsupported value type for the {nameof(GetRandom)} method.", nameof(type)),
            };
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
        /// Generates a random <see cref="bool"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>Random <see cref="bool"/>.</returns>
        public static bool GetRandomBoolean(bool currentValue)
        {
            return new Random().NextBool(currentValue);
        }

        /// <summary>
        /// Generates a random <see cref="char"/>.
        /// </summary>
        /// <returns>Random <see cref="char"/>.</returns>
        public static char GetRandomChar()
        {
            return new Random().NextChar();
        }

        /// <summary>
        /// Generates a random <see cref="char"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>Random <see cref="char"/>.</returns>
        public static char GetRandomChar(char currentValue)
        {
            return new Random().NextChar(currentValue);
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
        /// Generates a random <see cref="DateTime"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>Random <see cref="DateTime"/>.</returns>
        public static DateTime GetRandomDateTime(DateTime currentValue)
        {
            return new Random().NextDateTime(currentValue);
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
        /// Generates a random <see cref="decimal"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>Random <see cref="decimal"/>.</returns>
        public static decimal GetRandomDecimal(decimal currentValue)
        {
            return new Random().NextDecimal(currentValue);
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
        /// Generates a random <see cref="double"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>A random <see cref="double"/>.</returns>
        public static double GetRandomDouble(double currentValue)
        {
            return new Random().NextDouble(currentValue);
        }

        /// <summary>
        /// Generates a random <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Enum"/> value to generate.</typeparam>
        /// <returns>A random <see cref="Enum"/> value.</returns>
        public static T GetRandomEnum<T>() where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));
            return (T)values.GetValue(new Random().Next(values.Length))!;
        }

        /// <summary>
        /// Generates a random <see cref="Enum"/> value.
        /// </summary>
        /// <param name="enumType">Value to not duplicate.</param>
        /// <returns>A random <see cref="Enum"/> value.</returns>
        public static dynamic GetRandomEnum(Type enumType)
        {
            if (!enumType.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumType)} is not an enum.", nameof(enumType));
            }

            Array values = Enum.GetValues(enumType);
            return values.GetValue(new Random().Next(values.Length))!;
        }

        /// <summary>
        /// Generates a random <see cref="Enum"/> value.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Enum"/> to generate a random value for.</typeparam>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>A random <see cref="Enum"/> value.</returns>
        public static T GetRandomEnum<T>(T currentValue) where T : Enum
        {
            if (typeof(T) != currentValue.GetType())
            {
                throw new ArgumentException($"The generic type {typeof(T)} does not match the object {nameof(currentValue)} type of {currentValue.GetType()}.");
            }

            do
            {
                T v = GetRandomEnum<T>();

                if (!v.Equals(currentValue))
                {
                    return v;
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
        /// Generates a random <see cref="int"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue)
        {
            return new Random().NextInt(currentValue);
        }

        /// <summary>
        /// Generates a random <see cref="int"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <param name="maxValue">Range of the value.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue, int maxValue)
        {
            return new Random().NextInt(currentValue, maxValue);
        }

        /// <summary>
        /// Generates a random <see cref="int"/>.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate..</param>
        /// <param name="minValue">Minimum value of the range.</param>
        /// <param name="maxValue">Maximum value of the range.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int GetRandomInteger(int currentValue, int minValue, int maxValue)
        {
            return new Random().NextInt(currentValue, minValue, maxValue);
        }

        /// <summary>
        /// Generates a random <see cref="string"/>.
        /// </summary>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string GetRandomString()
        {
            return new Random().NextString();
        }

        /// <summary>
        /// Generates a random string different from the provided value.
        /// </summary>
        /// <param name="currentValue">Value to not duplicate.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string GetRandomString(string currentValue)
        {
            return new Random().NextString(currentValue);
        }

        /// <summary>
        /// Generates a random <see cref="string"/>.
        /// </summary>
        /// <param name="length">Number of characters the string should contain.</param>
        /// <returns>A random <see cref="string"/>.</returns>
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
        /// Returns a random <see cref="bool"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="bool"/>.</returns>
        public static bool NextBool(this Random random, bool currentValue)
        {
            do
            {
                bool v = random.NextBool();

                if (v != currentValue)
                {
                    return v;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="char"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="char"/>.</returns>
        public static char NextChar(this Random random)
        {
            return CHARS[random.Next(0, CHARS.Length)];
        }

        /// <summary>
        /// Returns a random <see cref="char"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="char"/>.</returns>
        public static char NextChar(this Random random, char currentValue)
        {
            do
            {
                char v = random.NextChar();

                if (v != currentValue)
                {
                    return v;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="DateTime"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="DateTime"/>.</returns>
        public static DateTime NextDateTime(this Random random)
        {
            DateTime from = new(1900, 1, 1);
            DateTime to = new(2050, 12, 31);

            TimeSpan range = to - from;

            TimeSpan randomRange = new((long)(random.NextDouble() * range.Ticks));

            return from + randomRange;
        }

        /// <summary>
        /// Returns a random <see cref="DateTime"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="DateTime"/>.</returns>
        public static DateTime NextDateTime(this Random random, DateTime currentValue)
        {
            do
            {
                DateTime v = random.NextDateTime();

                if (v != currentValue)
                {
                    return v;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="decimal"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="decimal"/>.</returns>
        public static decimal NextDecimal(this Random random)
        {
            byte scale = (byte)random.Next(29);
            bool sign = random.Next(2) == 1;

            return new decimal(random.DoubleSeed(), random.DoubleSeed(), random.DoubleSeed(), sign, scale);
        }

        /// <summary>
        /// Returns a random <see cref="decimal"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="decimal"/>.</returns>
        public static decimal NextDecimal(this Random random, decimal currentValue)
        {
            do
            {
                decimal v = random.NextDecimal();

                if (v != currentValue)
                {
                    return v;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="double"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="double"/>.</returns>
        public static double NextDouble(this Random random, double currentValue)
        {
            do
            {
                double v = random.NextDouble();

                if (v != currentValue)
                {
                    return v;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="int"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int NextInt(this Random random, int currentValue)
        {
            do
            {
                int result = random.Next();

                if (result != currentValue)
                {
                    return result;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="int"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>A random <see cref="int"/>.</returns>
        public static int NextInt(this Random random, int currentValue, int maxValue)
        {
            do
            {
                int result = random.Next(maxValue);

                if (result != currentValue)
                {
                    return result;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="int"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <param name="minValue">Minimum value.</param>
        /// <param name="maxValue">Maximum value.</param>
        /// <returns>A random <see cref="int"/> between the <paramref name="maxValue"/> and the <paramref name="maxValue"/>.</returns>
        public static int NextInt(this Random random, int currentValue, int minValue, int maxValue)
        {
            do
            {
                int result = random.Next(minValue, maxValue);

                if (result != currentValue)
                {
                    return result;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="string"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string NextString(this Random random)
        {
            return random.NextString(random.Next(4, 20));
        }

        /// <summary>
        /// Returns a random <see cref="string"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="length">Number of characters in the string.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string NextString(this Random random, int length)
        {
            return new string(Enumerable.Repeat(CHARSTRING, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Returns a random <see cref="string"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string NextString(this Random random, string currentValue)
        {
            do
            {
                string result = random.NextString();

                if (result != currentValue)
                {
                    return result;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random <see cref="string"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <param name="length">Number of characters in the string.</param>
        /// <param name="currentValue">Value to not match.</param>
        /// <returns>A random <see cref="string"/>.</returns>
        public static string NextString(this Random random, int length, string currentValue)
        {
            do
            {
                string result = random.NextString(length);

                if (result != currentValue)
                {
                    return result;
                }
            } while (true);
        }

        /// <summary>
        /// Returns a random 32bit <see cref="int"/>.
        /// </summary>
        /// <param name="random"><see cref="Random"/> to add extension.</param>
        /// <returns>A random 32 bit <see cref="int"/>.</returns>
        private static int DoubleSeed(this Random random)
        {
            unchecked
            {
                int firstBit = random.Next(0, 1 << 4) << 28;
                int lastBit = random.Next(0, 1 << 28);

                return firstBit | lastBit;
            }
        }
    }
}
