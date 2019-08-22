// <copyright file="ChangableMessengerBaseClassTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    /// <summary>
    /// Tests the <see cref="ChangableMessengerBaseClass"/> and <see cref="ChangableMessengerCollection{T}"/> abstract classes.
    /// </summary>
    [TestClass]
    public class ChangableMessengerBaseClassTests
    {
        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> initializes without setting <see cref="IChangeTracking.IsChanged"/> to true.
        /// </summary>
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new ChangableMessengerWithChildrenMock().IsChanged);
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableBaseClass"/> initializes without a <see cref="ChangableMessengerBaseClass.Message"/> value.
        /// </summary>
        [TestMethod]
        public void InitializesWithoutMessageValue()
        {
            ChangableMessengerWithChildrenMock obj = new ChangableMessengerWithChildrenMock();
            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> serializes and deserializes properly.
        /// </summary>
        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> does not set <see cref="IChangeTracking.IsChanged"/> to true after being deserialized.
        /// </summary>
        [TestMethod]
        public void IsNotChangedAfterDeserialized()
        {
            SerializationAssert.IsNotChangedAfterDeserialized<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClassTests"/> deserializes without a <see cref="ChangableMessengerBaseClass.Message"/> value.
        /// </summary>
        [TestMethod]
        public void SerializesWithoutMessage()
        {
            ChangableMessengerWithChildrenMock obj = ObjectUtilities.CreateInstanceWithRandomValues<ChangableMessengerWithChildrenMock>();

            obj.RaiseMessage(RandomUtilities.GetRandomString());

            obj = ObjectUtilities.GetSerializedCopyOfObject(obj);

            Assert.IsTrue(string.IsNullOrEmpty(obj.Message));
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> can change property values.
        /// </summary>
        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> raises <see cref="PropertyChangedEventHandler"/> when property values change.
        /// </summary>
        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged(ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<ChangableMessengerWithChildrenMock>()));
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> changes <see cref="IChangeTracking.IsChanged"/> to true when properties are changed.
        /// </summary>
        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> raises <see cref="OnChangedEventHandler"/> correctly when properties are changed.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedWhenChanged()
        {
            ChangableAssert.NotifiesIsChangedWhenChanged<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> sets <see cref="IChangeTracking.IsChanged"/> to false when executing <see cref="IChangeTracking.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<ChangableMessengerWithChildrenMock>();
        }

        /// <summary>
        /// Tests that an implementation of <see cref="ChangableMessengerBaseClass"/> raises <see cref="OnChangedEventHandler"/> once when executing <see cref="IChangeTracking.AcceptChanges"/>.
        /// </summary>
        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            ChangableAssert.NotifiesIsChangedOnAcceptChanges<ChangableMessengerWithChildrenMock>();
        }
    }
}
