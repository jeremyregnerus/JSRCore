// <copyright file="SerializationAssert.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.Asserts
{
    /// <summary>
    /// Performs Assers on Serialization operations.
    /// </summary>
    public static class SerializationAssert
    {
        #region SerializesAndDeserializes

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <param name="assert">Assert extension.</param>
        /// <param name="type">Type of object to test deserialization.</param>
        public static void SerializesAndDeserializes(this Assert assert, Type type)
        {
            SerializesAndDeserializes(assert, ObjectUtilities.CreateInstanceWithRandomValues(type));
        }

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <typeparam name="T">Type of object to test serialization.</typeparam>
        /// <param name="assert">Assert extension.</param>
        public static void SerializesAndDeserializes<T>(this Assert assert)
        {
            SerializesAndDeserializes(assert, ObjectUtilities.CreateInstanceWithRandomValues<T>());
        }

        /// <summary>
        /// Tests that an object serializes and retains its values when it is deserialized.
        /// </summary>
        /// <typeparam name="T">Type of object to test serialization.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object to test serialization.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Assert Extension")]
        public static void SerializesAndDeserializes<T>(this Assert assert, T obj)
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(obj);

            Assert.AreNotSame(copy, obj);
            Assert.That.ObjectsAreEquivalent(obj, copy);
        }

        #endregion

        #region IsNotChangedAfterDeserialized

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="type">Type of object that implements <see cref="IChangeTracking"/>.
        /// If the type specified does not implement <see cref="IChangeTracking"/> and exception will occur.</param>
        public static void IsNotChangedAfterDeserialized(this Assert assert, Type type)
        {
            Assert.IsTrue(typeof(IChangeTracking).IsAssignableFrom(type));
            IsNotChangedAfterDeserialized(assert, ObjectUtilities.CreateInstanceWithRandomValues(type));
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <typeparam name="T">Type of object that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        public static void IsNotChangedAfterDeserialized<T>(this Assert assert) where T : IChangeTracking
        {
            IsNotChangedAfterDeserialized(assert, ObjectUtilities.CreateInstanceWithRandomValues<T>());
        }

        /// <summary>
        /// Tests that an <see cref="IChangeTracking"/> object is not changed after deserialization.
        /// </summary>
        /// <typeparam name="T">Type of object that implements <see cref="IChangeTracking"/>.</typeparam>
        /// <param name="assert">Assertion extension.</param>
        /// <param name="obj">Object to test that implements <see cref="IChangeTracking"/>.</param>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "Assert extension")]
        public static void IsNotChangedAfterDeserialized<T>(this Assert assert, T obj) where T : IChangeTracking
        {
            T copy = ObjectUtilities.GetSerializedCopyOfObject(obj);
            Assert.IsFalse(copy.IsChanged);
        }

        #endregion
    }
}
