// <copyright file="EquivalencyAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Tests and checks for equivalency.
    /// </summary>
    public static class EquivalencyAssert
    {
        /// <summary>
        /// Tests if two objects have the same values for their properties.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of objects to compare.</typeparam>
        /// <param name="expected"><see cref="object"/> containing the expected values.</param>
        /// <param name="actual"><see cref="object"/> to compare.</param>
        public static void AreEquivalent<T>(T expected, T actual)
        {
            if (expected == null && actual == null)
            {
                return;
            }
            else if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                throw new AssertFailedException($"{nameof(expected)} is not equivalent to {nameof(actual)}");
            }

            Type objectType = expected.GetType();

            if (typeof(IList).IsAssignableFrom(objectType))
            {
                AreListsEquivalent((IList)expected, (IList)actual);
                return;
            }

            foreach (PropertyInfo property in objectType.GetRuntimeProperties())
            {
                if (typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
                {
                    AreListsEquivalent((IList)property.GetValue(expected), (IList)property.GetValue(actual));
                }
                else if (property.SetMethod != null && property.SetMethod.IsPublic)
                {
                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(int) || property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(double) || property.PropertyType == typeof(long) || property.PropertyType == typeof(decimal) || property.PropertyType == typeof(float))
                    {
                        var expectedValue = property.GetValue(expected);
                        var actualValue = property.GetValue(actual);

                        Assert.AreEqual(expectedValue, actualValue);
                    }
                    else if (property.PropertyType.IsClass)
                    {
                        AreEquivalent(property.GetValue(expected), property.GetValue(actual));
                    }
                    else if (property.PropertyType.IsValueType)
                    {
                        Assert.AreEqual(property.GetValue(expected), property.GetValue(actual));
                    }
                }
            }
        }

        /// <summary>
        /// Tests if two objects do not have the same values for their properties.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of objects to compare.</typeparam>
        /// <param name="expected"><see cref="object"/> containing the expected values.</param>
        /// <param name="actual"><see cref="object"/> to compare.</param>
        public static void AreNotEquivalent<T>(T expected, T actual)
        {
            Assert.IsFalse(CheckAreEquivalent(expected, actual));
        }

        /// <summary>
        /// Test if two lists have the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IList"/> to compare.</typeparam>
        /// <param name="expected"><see cref="IList"/> that contains expected values.</param>
        /// <param name="actual"><see cref="IList"/> that to compare.</param>
        public static void AreListsEquivalent<T>(T expected, T actual) where T : IList
        {
            Assert.AreEqual(expected.Count, actual.Count);

            for (int i = 0; i < expected.Count; i++)
            {
                AreEquivalent(expected[i], actual[i]);
            }
        }

        /// <summary>
        /// Test if two lists do not have the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IList"/> to compare.</typeparam>
        /// <param name="expected"><see cref="IList"/> that contains expected values.</param>
        /// <param name="actual"><see cref="IList"/> to compare.</param>
        public static void AreListsNotEquivalent<T>(T expected, T actual) where T : IList
        {
            Assert.IsFalse(CheckAreListsEquivalent(expected, actual));
        }

        /// <summary>
        /// Checks if two values contain the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="object"/> to compare.</typeparam>
        /// <param name="expected"><see cref="object"/> containing the expected values to compare to.</param>
        /// <param name="actual"><see cref="object"/> to compare.</param>
        /// <returns>True if the objects contain the same values for their properties; otherwise false.</returns>
        public static bool CheckAreEquivalent<T>(T expected, T actual)
        {
            if (expected == null && actual == null)
            {
                return true;
            }
            else if ((expected == null && actual != null) || (expected != null && actual == null))
            {
                return false;
            }

            Type objectType = expected?.GetType() ?? actual?.GetType();

            if (typeof(IList).IsAssignableFrom(objectType))
            {
                return CheckAreListsEquivalent((IList)expected, (IList)actual);
            }

            foreach (PropertyInfo property in objectType.GetRuntimeProperties())
            {
                if (typeof(IList).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string))
                {
                    if (!CheckAreListsEquivalent((IList)property.GetValue(expected), (IList)property.GetValue(actual)))
                    {
                        return false;
                    }
                }
                else if (property.SetMethod != null && property.SetMethod.IsPublic)
                {
                    if (property.PropertyType == typeof(bool) || property.PropertyType == typeof(int) || property.PropertyType == typeof(string) || property.PropertyType == typeof(DateTime) || property.PropertyType == typeof(double) || property.PropertyType == typeof(long))
                    {
                        if (property.SetMethod != null && property.SetMethod.IsPublic)
                        {
                            var expectedValue = property.GetValue(expected);
                            var actualValue = property.GetValue(actual);

                            if ((expectedValue != null && !expectedValue.Equals(actualValue)) || (actualValue != null && !actualValue.Equals(expectedValue)))
                            {
                                return false;
                            }
                        }
                    }
                    else if (property.PropertyType.IsClass || property.PropertyType.IsValueType)
                    {
                        if (!CheckAreEquivalent(property.GetValue(expected), property.GetValue(actual)))
                        {
                            return false;
                        }
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Checks if two lists contain the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IList"/> to check for equal values.</typeparam>
        /// <param name="expected"><see cref="IList"/> containing expected values to compare to.</param>
        /// <param name="actual"><see cref="IList"/> to compare to.</param>
        /// <returns>True if the lists contain the same values; otherwise false.</returns>
        public static bool CheckAreListsEquivalent<T>(T expected, T actual) where T : IList
        {
            if (expected.Count != actual.Count)
            {
                return false;
            }

            for (int i = 0; i < expected.Count; i++)
            {
                if (!CheckAreEquivalent(expected[i], actual[i]))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
