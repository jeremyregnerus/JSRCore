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
        public void GetRandom_Generic_CreatesDifferentValue(Type type)
        {
            var random = RandomUtilities.GetRandom(type);
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

            int min = new Random().Next(10);
            int max = new Random().Next(11, 100);

            random = new Random().Next(max);
            Assert.AreNotEqual(random, RandomUtilities.GetRandomInteger(random, max));

            random = new Random().Next(min, max);
            Assert.AreNotEqual(random, RandomUtilities.GetRandomInteger(random, min, max));
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
        public void GetRandomString_CreatesStringOfLength()
        {
            int length = new Random().Next(5, 100);

            Assert.AreEqual(length, RandomUtilities.GetRandomString(length).Length);
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
        public void NextChar_CreatesRandomChar()
        {
            bool isRandom = false;
            char random = new Random().NextChar();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextChar();

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
        public void NextDouble_CreatesRandomDouble()
        {
            bool isRandom = false;
            double random = new Random().NextDouble();

            for (int i = 0; i < 100; i++)
            {
                isRandom = random != new Random().NextDouble();

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NextEnum_CreatesRandomEnumValue()
        {
            bool isRandom = false;
            EnumMock randomEnum = new Random().NextEnum(typeof(EnumMock));

            for (int i = 0; i < 100; i++)
            {
                isRandom = randomEnum != new Random().NextEnum(typeof(EnumMock));

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);

            isRandom = false;
            randomEnum = new Random().NextEnum<EnumMock>();

            for (int i = 0; i < 100; i++)
            {
                isRandom = randomEnum != new Random().NextEnum<EnumMock>();

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

        [TestMethod]
        [DataRow(typeof(bool))]
        [DataRow(typeof(char))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(double))]
        [DataRow(typeof(int))]
        [DataRow(typeof(string))]
        [DataRow(typeof(DateTime))]
        [DataRow(typeof(EnumMock))]
        public void NextTypeOf_Type_CreatesRandomValueOfType(Type type)
        {
            bool isRandom = false;
            var randomValue = new Random().NextTypeOf(type);

            for (int i = 0; i < 100; i++)
            {
                // var newRandom = RandomUtilities.GetRandom(type);
                isRandom = randomValue != new Random().NextTypeOf(type);

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
        public void NextTypeOf_Generic_CreatesRandomValueOfType(Type type)
        {
            bool isRandom = false;
            Random r = new();

            var randomValue = typeof(RandomUtilities).GetMethod(nameof(RandomUtilities.NextTypeOf), new Type[] { typeof(Random) }).MakeGenericMethod(type).Invoke(null, new[] { r });

            for (int i = 0; i < 100; i++)
            {
                isRandom = randomValue != typeof(RandomUtilities).GetMethod(nameof(RandomUtilities.NextTypeOf), new Type[] { typeof(Random) }).MakeGenericMethod(type).Invoke(null, new[] { r });

                if (isRandom)
                {
                    break;
                }
            }

            Assert.IsTrue(isRandom);
        }

        [TestMethod]
        public void NewBool_CreatesDifferentBool()
        {
            bool random = new Random().NextBool();
            Assert.AreNotEqual(random, new Random().NewBool(random));
        }

        [TestMethod]
        public void NewChar_CreatesDifferentChar()
        {
            char random = new Random().NextChar();
            Assert.AreNotEqual(random, new Random().NewChar(random));
        }

        [TestMethod]
        public void NewDateTime_CreatesDifferentDateTime()
        {
            DateTime random = new Random().NextDateTime();
            Assert.AreNotEqual(random, new Random().NewDateTime(random));
        }

        [TestMethod]
        public void NewDecimal_CreatesDifferentDecimal()
        {
            decimal random = new Random().NextDecimal();
            Assert.AreNotEqual(random, new Random().NewDecimal(random));
        }

        [TestMethod]
        public void NewDouble_CreatesDifferentDouble()
        {
            double random = new Random().NextDouble();
            Assert.AreNotEqual(random, new Random().NewDouble(random));
        }

        [TestMethod]
        public void NewEnum_CreatedDifferentEnum()
        {
            EnumMock random = new Random().NextEnum(typeof(EnumMock));
            Assert.AreNotEqual(random, new Random().NewEnum(typeof(EnumMock), random));

            random = new Random().NextEnum<EnumMock>();
            Assert.AreNotEqual(random, new Random().NewEnum(random));
        }

        [TestMethod]
        public void NewInt_CreatesDifferentInt()
        {
            int random = new Random().Next();
            Assert.AreNotEqual(random, new Random().NewInt(random));

            int min = new Random().Next(10);
            int max = new Random().Next(11, 100);

            random = new Random().Next(max);

            Assert.AreNotEqual(random, new Random().NewInt(random, max));

            random = new Random().Next(min, max);

            Assert.AreNotEqual(random, new Random().NewInt(random, min, max));
        }

        [TestMethod]
        public void NewString_CreatesDifferentString()
        {
            string random = new Random().NextString();
            Assert.AreNotEqual(random, new Random().NewString(random));
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
        public void NewTypeOf_Type_CreatesRandomValueOfType(Type type)
        {
            var random = new Random().NextTypeOf(type);
            Assert.AreNotEqual(random, RandomUtilities.NewTypeOf(new Random(), type, random));
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
        public void NewTypeOf_Generic_CreatesRandomValueOfType(Type type)
        {
            Random r = new();
            var random = typeof(RandomUtilities).GetMethod(nameof(RandomUtilities.NextTypeOf), new Type[] { typeof(Random) }).MakeGenericMethod(type).Invoke(null, new[] { r });

            var method = typeof(RandomUtilities).GetMethods().Single(m => m.Name == nameof(RandomUtilities.NewTypeOf) && m.IsGenericMethodDefinition);
            var genericMethod = method.MakeGenericMethod(type);

            var newVal = genericMethod.Invoke(null, new object[] { r, random });

            Assert.AreNotEqual(random, newVal);
        }

        [TestMethod]
        public void NewString_CreatesDifferentString_OfLength()
        {
            int length = new Random().Next(5, 30);
            string random = new Random().NextString(length);
            string newRandom = new Random().NewString(random, length);

            Assert.AreNotEqual(random, newRandom);
            Assert.AreEqual(length, newRandom.Length);
        }
    }
}
