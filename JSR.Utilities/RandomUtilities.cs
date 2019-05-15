// <copyright file="RandomUtilities.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Linq;
using System.Reflection;

namespace JSR.Utilities
{
    /// <summary>
    /// Generates and manipulates random values.
    /// </summary>
    public static class RandomUtilities
    {
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
        /// Generates a random Enum value different than the one specified.
        /// </summary>
        /// <typeparam name="T">Type of <see cref="Enum"/> to generate a random value for.</typeparam>
        /// <param name="currentValue">Current value not to match.</param>
        /// <returns>A random <see cref="Enum"/> value.</returns>
        public static T GetRandomEnum<T>(T currentValue) where T : Enum
        {
            Array values = Enum.GetValues(typeof(T));

            do
            {
                T newVal = (T)values.GetValue(new Random().Next(values.Length));

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

        /////// <summary>
        /////// Sets a random <see cref="bool"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="propertyName">Name of property to set.</param>
        ////public static void SetRandomBoolean<T>(T objectWithPropertyToSet, string propertyName)
        ////{
        ////    SetRandomBoolean(objectWithPropertyToSet, typeof(T).GetRuntimeProperty(propertyName));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="bool"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomBoolean<T>(T objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        throw new ArgumentException($"The property {property.Name} does not have a public set method", nameof(property));
        ////    }

        ////    property.SetValue(objectWithPropertyToSet, GetRandomBoolean((bool)property.GetValue(objectWithPropertyToSet)));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="DateTime"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="propertyName">Name of property to set.</param>
        ////public static void SetRandomDateTime<T>(T objectWithPropertyToSet, string propertyName)
        ////{
        ////    SetRandomDateTime(objectWithPropertyToSet, typeof(T).GetRuntimeProperty(propertyName));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="DateTime"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomDateTime<T>(T objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        throw new ArgumentException($"The property {property.Name} does not have a public set method", nameof(property));
        ////    }

        ////    property.SetValue(objectWithPropertyToSet, GetRandomDateTime((DateTime)property.GetValue(objectWithPropertyToSet)));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="decimal"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type to set property.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object to set property.</param>
        /////// <param name="propertyName">Name of property.</param>
        ////public static void SetRandomDecimal<T>(T objectWithPropertyToSet, string propertyName)
        ////{
        ////    SetRandomDecimal(objectWithPropertyToSet, typeof(T).GetRuntimeProperty(propertyName));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="decimal"/> value to an <see cref="object"/> property.
        /////// </summary>
        /////// <typeparam name="T">Typw with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomDecimal<T>(T objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        throw new ArgumentException($"The property {property.Name} does not have a public set method", nameof(property));
        ////    }

        ////    property.SetValue(objectWithPropertyToSet, GetRandomDecimal((decimal)property.GetValue(objectWithPropertyToSet)));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="double"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with properties to set.</param>
        /////// <param name="propertyName">Name of property to set.</param>
        ////public static void SetRandomDouble<T>(T objectWithPropertyToSet, string propertyName)
        ////{
        ////    SetRandomDouble(objectWithPropertyToSet, typeof(T).GetRuntimeProperty(propertyName));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="double"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomDouble<T>(T objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        throw new ArgumentException($"The property {property.Name} does not have a public set method", nameof(property));
        ////    }

        ////    property.SetValue(objectWithPropertyToSet, GetRandomDouble((double)property.GetValue(objectWithPropertyToSet)));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="Enum"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="propertyName">Name of property to set.</param>
        ////public static void SetRandomEnum<T>(T objectWithPropertyToSet, string propertyName)
        ////{
        ////    SetRandomEnum(objectWithPropertyToSet, typeof(T).GetRuntimeProperty(propertyName));
        ////}

        /////// <summary>
        /////// Sets a random <see cref="Enum"/> value to a property.
        /////// </summary>
        /////// <typeparam name="T">Type with property to set.</typeparam>
        /////// <param name="objectWithPropertyToSet">Object with property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomEnum<T>(T objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        throw new ArgumentException($"The property {property.Name} does not have a public set method", nameof(property));
        ////    }

        ////    do
        ////    {
        ////        int newVal = new Random().Next(Enum.GetNames(property.PropertyType).Length);

        ////        if (newVal != (int)property.GetValue(objectWithPropertyToSet))
        ////        {
        ////            property.SetValue(objectWithPropertyToSet, newVal);
        ////            return;
        ////        }
        ////    } while (true);
        ////}

        /////// <summary>
        /////// Sets a random <see cref="int"/> value to an <see cref="object"/> property.
        /////// </summary>
        /////// <param name="objectWithPropertyToSet"><see cref="object"/> that contains the <see cref="int"/> property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomInteger(object objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        return;
        ////    }

        ////    int oldValue = (int)property.GetValue(objectWithPropertyToSet);
        ////    int newValue;

        ////    do
        ////    {
        ////        newValue = new Random().Next();
        ////    } while (newValue.Equals(oldValue));

        ////    property.SetValue(objectWithPropertyToSet, newValue);
        ////}

        /////// <summary>
        /////// Sets a random <see cref="string"/> value to an <see cref="object"/> property.
        /////// </summary>
        /////// <param name="objectWithPropertyToSet"><see cref="object"/> that contains the <see cref="string"/> property to set.</param>
        /////// <param name="property">Property to set.</param>
        ////public static void SetRandomString(object objectWithPropertyToSet, PropertyInfo property)
        ////{
        ////    if (!ObjectUtilities.CheckIfPropertyHasPublicSetMethod(property))
        ////    {
        ////        return;
        ////    }

        ////    string oldValue = (string)property.GetValue(objectWithPropertyToSet);
        ////    string newValue;

        ////    do
        ////    {
        ////        newValue = new Random().NextString(new Random().Next(5, 20));
        ////    } while (newValue.Equals(oldValue, StringComparison.CurrentCulture));

        ////    property.SetValue(objectWithPropertyToSet, newValue);
        ////}

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
