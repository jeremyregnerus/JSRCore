// <copyright file="EquivalencyAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Collections;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Asserts and checks for equivalency.
    /// </summary>
    public static class EquivalencyAssert
    {
        /// <summary>
        /// Asserts if two objects have the same values for their properties.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of objects to compare.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="expected"><see cref="object"/> containing the expected values.</param>
        /// <param name="actual"><see cref="object"/> to compare.</param>
        public static void ObjectsAreEquivalent<T>(this Assert assert, T expected, T actual)
        {
            // if both objects are null, they are equivalent
            if (expected == null && actual == null)
            {
                return;
            }

            // if one value is null, and the other is not, they are not equivalent
            if ((expected == null && actual != null) || (actual == null && expected != null))
            {
                throw new AssertFailedException($"The expected object is {(expected != null ? "not " : string.Empty)}, while the actual object is {(actual != null ? "not " : string.Empty)} null.");
            }

            // assert both objects are the same type
            Assert.AreEqual(expected!.GetType(), actual!.GetType());

            // if the objects are a value type or a string, assert they are equal and return
            if (typeof(T).IsValueType || typeof(T) == typeof(string))
            {
                Assert.AreEqual(expected, actual);
                return;
            }

            // if the objects are lists, assert they are equivalent lists and return
            if (typeof(IList).IsAssignableFrom(typeof(T)))
            {
                assert.ListsAreEquivalent((IList)expected, (IList)actual);
                return;
            }

            // for each property in the objects, assert those objects are equivalent
            foreach (PropertyInfo property in typeof(T).GetRuntimeProperties())
            {
                assert.ObjectsAreEquivalent(property.GetValue(expected), property.GetValue(actual));
            }
        }

        /// <summary>
        /// Asserts if two objects do not have the same values for their properties.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of objects to compare.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="expected"><see cref="object"/> containing the expected values.</param>
        /// <param name="actual"><see cref="object"/> to compare.</param>
        public static void ObjectsAreNotEquivalent<T>(this Assert assert, T expected, T actual)
        {
            try
            {
                assert.ObjectsAreEquivalent(expected, actual);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(AssertFailedException))
                {
                    return;
                }

                throw;
            }

            throw new AssertFailedException($"Both objects are equivalent.");
        }

        /// <summary>
        /// Test if two lists have the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IList"/> to compare.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="expected"><see cref="IList"/> that contains expected values.</param>
        /// <param name="actual"><see cref="IList"/> that to compare.</param>
        public static void ListsAreEquivalent<T>(this Assert assert, T expected, T actual) where T : IList
        {
            // if the number of items in each list doesn't match, the lists are not equivalent
            if (expected.Count != actual.Count)
            {
                throw new AssertFailedException($"The number of items in the expected list is {expected.Count}, the number of items in the actual list is {actual.Count}.");
            }

            // check for equivalency for each item
            for (int i = 0; i < expected.Count; i++)
            {
                assert.ObjectsAreEquivalent(expected[i], actual[i]);
            }
        }

        /// <summary>
        /// Test if two lists do not have the same values.
        /// </summary>
        /// <typeparam name="T"><see cref="Type"/> of <see cref="IList"/> to compare.</typeparam>
        /// <param name="assert">Assert extension.</param>
        /// <param name="expected"><see cref="IList"/> that contains expected values.</param>
        /// <param name="actual"><see cref="IList"/> to compare.</param>
        public static void ListsAreNotEquivalent<T>(this Assert assert, T expected, T actual) where T : IList
        {
            try
            {
                assert.ListsAreEquivalent(expected, actual);
            }
            catch (Exception ex)
            {
                if (ex.GetType() == typeof(AssertFailedException))
                {
                    return;
                }

                throw;
            }

            throw new AssertFailedException($"Both lists are equivalent");
        }
    }
}
