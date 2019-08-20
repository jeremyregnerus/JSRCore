// <copyright file="NotifyPropertyChangeBaseClassTests.cs" company="Jeremy Regnerus">
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
    /// Tests the <see cref="NotifyPropertyChangeBaseClass"/> base class.
    /// </summary>
    [TestClass]
    public class NotifyPropertyChangeBaseClassTests
    {
        /// <summary>
        /// Tests that the <see cref="NotifyPropertyChangeBaseClass"/> serialized and deserializes.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<NotifyPropertyChangeMock>();
        }

        /// <summary>
        /// Tests that <see cref="NotifyPropertyChangeBaseClass"/> updated the values of its properties.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues(ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<NotifyPropertyChangeMock>()));
        }

        /// <summary>
        /// Tests that <see cref="NotifyPropertyChangeBaseClass"/> raises the <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged(ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<NotifyPropertyChangeMock>()));
        }
    }
}
