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
        /// Tests both Serialization and Change Tracking.
        /// </summary>
        /// <param name="type">Type that implements <see cref="IChangeTracking"/>.</param>
        public static void IChangeTrackingSerializationTests(Type type)
        {
            IChangeTrackingSerializationTests(CreateIChangeTrackingInstance(type));
        }

        /// <summary>
        /// Tests both Serialization and Change Tracking.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        public static void IChangeTrackingSerializationTests<T>() where T : IChangeTracking
        {
            IChangeTrackingSerializationTests(ObjectUtilities.CreateInstanceWithRandomValues<T>());
        }

        /// <summary>
        /// Tests both Serialization and Change Tracking.
        /// </summary>
        /// <typeparam name="T">Type that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="obj">Object that implements <see cref="IChangeTracking"/>.</param>
        public static void IChangeTrackingSerializationTests<T>(T obj) where T : IChangeTracking
        {
            SerializesAndDeserializes(obj);
            IsNotChangedAfterDeserialized(obj);
        }

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <param name="type">Type of object to test deserialization.</param>
        public static void SerializesAndDeserializes(Type type)
        {
            SerializesAndDeserializes(ObjectUtilities.CreateInstanceWithRandomValues(type));
        }

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
        /// <param name="obj">Object to test serialization.</param>
        public static void SerializesAndDeserializes<T>(T obj)
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(obj);

            Assert.AreNotSame(copy, obj);
            Assert.IsTrue(EquivalencyAssert.CheckAreEquivalent(obj, copy));
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <param name="type">Type of object that implements <see cref="IChangeTracking"/>.
        /// If the type specified does not implement <see cref="IChangeTracking"/> and exception will occur.</param>
        public static void IsNotChangedAfterDeserialized(Type type)
        {
            IsNotChangedAfterDeserialized(CreateIChangeTrackingInstance(type));
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
        /// <param name="obj">Object to test that implements <see cref="IChangeTracking"/>.</param>
        public static void IsNotChangedAfterDeserialized<T>(T obj) where T : IChangeTracking
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(obj);
            Assert.IsFalse(copy.IsChanged);
        }

        /// <summary>
        /// Checks that the type implements <see cref="IChangeTracking"/> and creates a new instance of the type.
        /// </summary>
        /// <param name="type">Type to test and create.</param>
        /// <returns>A new instance of the type that implements <see cref="IChangeTracking"/>.</returns>
        private static IChangeTracking CreateIChangeTrackingInstance(Type type)
        {
            Assert.IsTrue(typeof(IChangeTracking).IsAssignableFrom(type));

            return (IChangeTracking)Activator.CreateInstance(type);
        }
    }
}
