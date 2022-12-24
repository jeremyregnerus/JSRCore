// <copyright file="RandomUtilitiesTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using JSR.Utilities.Tests.Mocks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Utilities.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:Elements should be documented", Justification = "Unit test")]
    [SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores", Justification = "Unit test")]
    public class RandomUtilitiesTests
    {
        [TestMethod]
        [DataRow(typeof(bool))]
        [DataRow(typeof(char))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(double))]
        [DataRow(typeof(int))]
        [DataRow(typeof(string))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(EnumMock))]
        public void GetRandom_Type_CreatesRandomValueOfType(Type type)
        {
            bool isRandom = false;
            var randomValue = RandomUtilities.GetRandom(type);

            for (int i = 0; i < 100; i++)
            {
                // var newRandom = RandomUtilities.GetRandom(type);
                isRandom = randomValue != RandomUtilities.GetRandom(type);

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        [DataRow(typeof(bool))]
        [DataRow(typeof(char))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(double))]
        [DataRow(typeof(int))]
        [DataRow(typeof(string))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(EnumMock))]
        public void GetRandom_Generic_CreatesRandomValueOfType(Type type)
        {
            bool isRandom = false;
            var randomValue = typeof(RandomUtilities).GetMethod(nameof(RandomUtilities.GetRandom), Type.EmptyTypes).MakeGenericMethod(type).Invoke(null, null);

            for (int i = 0; i < 100; i++)
            {
                isRandom = randomValue != typeof(RandomUtilities).GetMethod(nameof(RandomUtilities.GetRandom), Type.EmptyTypes).MakeGenericMethod(type).Invoke(null, null);

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        [DataRow(typeof(bool))]
        [DataRow(typeof(char))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(double))]
        [DataRow(typeof(int))]
        [DataRow(typeof(string))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(EnumMock))]
        public void GetRandom_Type_CreatesDifferentValue(Type type)
        {
            var random = RandomUtilities.GetRandom(type);
            Assert.AreNotEqual(random, RandomUtilities.GetRandom(type, random));
            Assert.AreNotEqual(random, RandomUtilities.GetRandom(random));
        }

        [TestMethod]
        public void GetRandomBoolean_CreatesRandomBool()
        {
            bool isRandom = false;

            bool random = RandomUtilities.GetRandomBoolean();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomBoolean();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomBoolean_CreatesDifferentBool()
        {
            bool random = RandomUtilities.GetRandomBoolean();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomBoolean(random));
        }

        [TestMethod]
        public void GetRandomChar_CreatesRandomChar()
        {
            bool isRandom = false;
            char random = RandomUtilities.GetRandomChar();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomChar();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomChar_CreatesDifferentChar()
        {
            char random = RandomUtilities.GetRandomChar();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomChar(random));
        }

        [TestMethod]
        public void GetRandomDateTime_CreatesRandomDateTime()
        {
            bool isRandom = false;
            DateTime random = RandomUtilities.GetRandomDateTime();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomDateTime();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomDateTime_CreatesDifferentDateTime()
        {
            DateTime random = RandomUtilities.GetRandomDateTime();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomDateTime(random));
        }

        [TestMethod]
        public void GetRandomDecimal_CreatesRandomDecimal()
        {
            bool isRandom = false;
            decimal random = RandomUtilities.GetRandomDecimal();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomDecimal();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomDecimal_CreatesDifferentDecimal()
        {
            decimal random = RandomUtilities.GetRandomDecimal();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomDecimal(random));
        }

        [TestMethod]
        public void GetRandomDouble_CreatesRandomDouble()
        {
            bool isRandom = false;
            double random = RandomUtilities.GetRandomDouble();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomDouble();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomDouble_CreatesDifferentDouble()
        {
            for (int i = 0; i < new Random().Next(5, 20); i++)
            {
                double v1 = RandomUtilities.GetRandomDouble();
                double v2 = RandomUtilities.GetRandomDouble(v1);
                Assert.AreNotEqual(v1, v2);
            }
        }

        [TestMethod]
        public void GetRandomEnum_CreatesRandomEnum()
        {
            bool isRandom = false;
            EnumMock randomEnum = RandomUtilities.GetRandomEnum(typeof(EnumMock));

            for (int i = 0; i < 100; i++)
            {
                isRandom = randomEnum != RandomUtilities.GetRandomEnum(typeof(EnumMock));

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomEnum_CreatesDifferentEnum()
        {
            for (int i = 0; i < 20; i++)
            {
                EnumMock v1 = RandomUtilities.GetRandomEnum<EnumMock>();
                EnumMock v2 = RandomUtilities.GetRandomEnum(v1);
                Assert.AreNotEqual(v1, v2);
            }
        }

        [TestMethod]
        public void GetRandomInteger_CreatesRandomInteger()
        {
            bool isRandom = false;
            int random = RandomUtilities.GetRandomInteger();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomInteger();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomInteger_CreatesDifferentInteger()
        {
            int random = RandomUtilities.GetRandomInteger();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomInteger(random));
        }

        [TestMethod]
        public void GetRandomString_CreatesRandomString()
        {
            bool isRandom = false;
            string random = RandomUtilities.GetRandomString();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != RandomUtilities.GetRandomString();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void GetRandomString_CreatesDifferentString()
        {
            string random = RandomUtilities.GetRandomString();
            Assert.AreNotEqual(random, RandomUtilities.GetRandomString(random));
        }

        [TestMethod]
        public void NextBool_CreatesRandomBool()
        {
            bool isRandom = false;
            bool random = new Random().NextBool();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextBool();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextDateTime_CreatesRandomDateTime()
        {
            bool isRandom = false;
            DateTime random = new Random().NextDateTime();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextDateTime();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextDecimal_CreatesRandomDecimal()
        {
            bool isRandom = false;
            decimal random = new Random().NextDecimal();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextDecimal();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextInt32_CreatesRandomInteger()
        {
            bool isRandom = false;
            int random = new Random().NextInt32();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextInt32();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextString_CreatesRandomString()
        {
            bool isRandom = false;
            string random = new Random().NextString();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextString();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextString_CreatesSpecificNumberOfCharacters()
        {
            int count = new Random().Next(5, 30);
            Assert.AreEqual(count, new Random().NextString(count).Length);
        }
    }
}
