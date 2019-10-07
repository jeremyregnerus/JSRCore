// <copyright file="NotifyPropertyChangeBaseClassTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
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
            SerializationAssert.SerializesAndDeserializes(GetSerializedNotifyPropertyChangeMock());
        }

        /// <summary>
        /// Tests that <see cref="NotifyPropertyChangeBaseClass"/> updated the values of its properties.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues<NotifyPropertyChangeMock>();
            PropertyValueChangeAssert.ChangesValues(GetSerializedNotifyPropertyChangeMock());
        }

        /// <summary>
        /// Tests that <see cref="NotifyPropertyChangeBaseClass"/> raises the <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged<NotifyPropertyChangeMock>();
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged(GetSerializedNotifyPropertyChangeMock());
        }

        private NotifyPropertyChangeMock GetSerializedNotifyPropertyChangeMock()
        {
            return ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<NotifyPropertyChangeMock>());
        }
    }
}
