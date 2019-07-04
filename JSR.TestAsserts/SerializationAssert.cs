// <copyright file="SerializationAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.ComponentModel;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.TestAsserts
{
    /// <summary>
    /// Performs Assers on Serialization operations.
    /// </summary>
    public static class SerializationAssert
    {
        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <typeparam name="T">Type of object to test serialization.</typeparam>
        public static void SerializesAndDeserializes<T>()
        {
            SerializesAndDeserializes(ObjectUtilities.CreateInstanceWithRandomValues<T>());
        }

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <typeparam name="T">Type of object to test serialization.</typeparam>
        /// <param name="objectToTest">Object to test serialization.</param>
        public static void SerializesAndDeserializes<T>(T objectToTest)
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(objectToTest);

            Assert.AreNotSame(copy, objectToTest);
            Assert.IsTrue(EquivalencyAssert.CheckAreEquivalent(objectToTest, copy));
        }

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <param name="typeToTest">Type of object to test deserialization.</param>
        public static void SerializesAndDeserializes(Type typeToTest)
        {
            SerializesAndDeserializes(ObjectUtilities.CreateInstanceWithRandomValues(typeToTest));
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <typeparam name="T">Type of object that implements <see cref="IChangeTracking"/>.</typeparam>
        public static void IsNotChangedAfterDeserialized<T>() where T : IChangeTracking
        {
            IsNotChangedAfterDeserialized(ObjectUtilities.CreateInstanceWithRandomValues<T>());
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <typeparam name="T">Type of object that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="objectToTest">Object to test that implements <see cref="IChangeTracking"/>.</param>
        public static void IsNotChangedAfterDeserialized<T>(T objectToTest) where T : IChangeTracking
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(objectToTest);
            Assert.IsFalse(copy.IsChanged);
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <param name="typeToTest">Type of object that implements <see cref="IChangeTracking"/>.
        /// If the type specified does not implement <see cref="IChangeTracking"/> and exception will occur.</param>
        public static void IsNotChangedAfterDeserialized(Type typeToTest)
        {
            if (!typeof(IChangeTracking).IsAssignableFrom(typeToTest))
            {
                throw new ArgumentException($"The parameter {nameof(typeToTest)} type of {typeToTest} does not implement the IChangeTracking interface.", nameof(typeToTest));
            }

            IsNotChangedAfterDeserialized((IChangeTracking)ObjectUtilities.CreateInstanceWithRandomValues(typeToTest));
        }
    }
}
