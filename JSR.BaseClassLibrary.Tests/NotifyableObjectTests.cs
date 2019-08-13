// <copyright file="NotifyableObjectTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    /// <summary>
    /// Tests the <see cref="NotifyableObject"/> base class.
    /// </summary>
    [TestClass]
    public class NotifyableObjectTests
    {
        /// <summary>
        /// Tests that an object that implements <see cref="NotifyableObject"/> can serialize and deserialize.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<MockNotifyableObject>();
        }

        /// <summary>
        /// Tests that <see cref="NotifyableObject"/> updated the values of its properties.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyChangeAssert.ChangesValues<MockNotifyableObject>();
        }

        /// <summary>
        /// Tests that <see cref="NotifyableObject"/> raises the <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyNotificationAssert.NotifiesChanges<MockNotifyableObject>();
        }

        /// <summary>
        /// Tests that <see cref="NotifyableObject"/> does not raise <see cref="PropertyChangedEventHandler"/> when properties get set with the same value they previously had.
        /// </summary>
        [TestMethod]
        public void DoesNotNotifyOnSameValues()
        {
            MockNotifyableObject obj = ObjectUtilities.CreateInstanceWithRandomValues<MockNotifyableObject>();
            MockNotifyableObject objCopy = ObjectUtilities.GetSerializedCopyOfObject(obj);

            List<string> propertiesChanged = new List<string>();
            obj.PropertyChanged += (sender, args) => propertiesChanged.Add(args.PropertyName);

            int count = new Random().Next(5, 20);

            for (int i = 0; i < count; i++)
            {
                obj.DateTimeProperty = objCopy.DateTimeProperty;
                obj.IntegerProperty = objCopy.IntegerProperty;
                obj.StringProperty = objCopy.StringProperty;

                Assert.AreEqual(0, propertiesChanged.Count);

                ObjectUtilities.PopulateObjectWithRandomValues(obj);
                objCopy = ObjectUtilities.GetSerializedCopyOfObject(obj);

                propertiesChanged.Clear();
            }
        }
    }
}
