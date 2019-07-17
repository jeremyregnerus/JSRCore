// <copyright file="RandomUtilitiesTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Utilities.Tests
{
    /// <summary>
    /// Enum for testing.
    /// </summary>
    public enum TestEnum
    {
        /// <summary>
        /// One
        /// </summary>
        One,

        /// <summary>
        /// Two
        /// </summary>
        Two,

        /// <summary>
        /// Three
        /// </summary>
        Three,

        /// <summary>
        /// Four
        /// </summary>
        Four,

        /// <summary>
        /// Five
        /// </summary>
        Five,
    }

    /// <summary>
    /// Random test to see if functionality works.
    /// </summary>
    [TestClass]
    public class RandomUtilitiesTests
    {
        /// <summary>
        /// Tests random string generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomString()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                string v1 = RandomUtilities.GetRandomString();
                Assert.IsFalse(string.IsNullOrEmpty(v1));

                string v2 = RandomUtilities.GetRandomString(v1);
                Assert.IsFalse(string.IsNullOrEmpty(v2));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<string>();
                Assert.IsFalse(string.IsNullOrEmpty(v1));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsFalse(string.IsNullOrEmpty(v2));
                Assert.IsInstanceOfType(v2, typeof(string));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(string));
                Assert.IsFalse(string.IsNullOrEmpty(v1));
                Assert.IsInstanceOfType(v1, typeof(string));

                v2 = RandomUtilities.GetRandom(typeof(string), v1);
                Assert.IsFalse(string.IsNullOrEmpty(v2));
                Assert.IsInstanceOfType(v2, typeof(string));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random enumerator generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomEnum()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                TestEnum v1 = RandomUtilities.GetRandomEnum<TestEnum>();
                Assert.IsInstanceOfType(v1, typeof(TestEnum));

                TestEnum v2 = RandomUtilities.GetRandomEnum(v1);
                Assert.IsInstanceOfType(v2, typeof(TestEnum));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandomEnum(typeof(TestEnum));
                Assert.IsInstanceOfType(v1, typeof(TestEnum));

                v1 = RandomUtilities.GetRandom(typeof(TestEnum));
                Assert.IsInstanceOfType(v1, typeof(TestEnum));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(TestEnum));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random boolean generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomBool()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                bool v1 = RandomUtilities.GetRandomBoolean();
                bool v2 = RandomUtilities.GetRandomBoolean(v1);
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<bool>();
                Assert.IsInstanceOfType(v1, typeof(bool));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(bool));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(bool));
                Assert.IsInstanceOfType(v1, typeof(bool));

                v2 = RandomUtilities.GetRandom(typeof(bool), v1);
                Assert.IsInstanceOfType(v2, typeof(bool));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random Integer generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomInt()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                int v1 = RandomUtilities.GetRandomInteger();
                int v2 = RandomUtilities.GetRandomInteger(v1);
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<int>();
                Assert.IsInstanceOfType(v1, typeof(int));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(int));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(int));
                Assert.IsInstanceOfType(v1, typeof(int));

                v2 = RandomUtilities.GetRandom(typeof(int), v1);
                Assert.IsInstanceOfType(v2, typeof(int));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random DateTime generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomDateTime()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                DateTime v1 = RandomUtilities.GetRandomDateTime();
                DateTime v2 = RandomUtilities.GetRandomDateTime(v1);
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<DateTime>();
                Assert.IsInstanceOfType(v1, typeof(DateTime));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(DateTime));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(DateTime));
                Assert.IsInstanceOfType(v1, typeof(DateTime));

                v2 = RandomUtilities.GetRandom(typeof(DateTime), v1);
                Assert.IsInstanceOfType(v2, typeof(DateTime));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random double generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomDouble()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                double v1 = RandomUtilities.GetRandomDouble();
                double v2 = RandomUtilities.GetRandomDouble(v1);
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<double>();
                Assert.IsInstanceOfType(v1, typeof(double));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(double));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(double));
                Assert.IsInstanceOfType(v1, typeof(double));

                v2 = RandomUtilities.GetRandom(typeof(double), v1);
                Assert.IsInstanceOfType(v2, typeof(double));
                Assert.AreNotEqual(v1, v2);
            }
        }

        /// <summary>
        /// Tests random decimal generation.
        /// </summary>
        [TestMethod]
        public void GeneratesRandomDecimal()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                decimal v1 = RandomUtilities.GetRandomDecimal();
                decimal v2 = RandomUtilities.GetRandomDecimal(v1);
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom<decimal>();
                Assert.IsInstanceOfType(v1, typeof(decimal));

                v2 = RandomUtilities.GetRandom(v1);
                Assert.IsInstanceOfType(v2, typeof(decimal));
                Assert.AreNotEqual(v1, v2);

                v1 = RandomUtilities.GetRandom(typeof(decimal));
                Assert.IsInstanceOfType(v1, typeof(decimal));

                v2 = RandomUtilities.GetRandom(typeof(decimal), v1);
                Assert.IsInstanceOfType(v2, typeof(decimal));
                Assert.AreNotEqual(v1, v2);
            }
        }
    }
}
