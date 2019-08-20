// <copyright file="ChangableMessengerBaseClassTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableMessengerBaseClassTests
    {
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new ChangableMessengerWithChildrenMock().IsChanged);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.SerializesAndDeserializes<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void IsNotChangedAfterDeserialized()
        {
            SerializationAssert.IsNotChangedAfterDeserialized<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void ChangesValues()
        {
            PropertyValueChangeAssert.ChangesValues<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void NotifiesPropertiesChange()
        {
            NotifyPropertyChangedAssert.NotifiesPropertiesChanged(ObjectUtilities.GetSerializedCopyOfObject(ObjectUtilities.CreateInstanceWithRandomValues<ChangableMessengerWithChildrenMock>()));
        }

        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableAssert.NotifiesIsChangedWhenChanged<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<ChangableMessengerWithChildrenMock>();
        }

        [TestMethod]
        public void NotifiesIsChangedOnAcceptChanges()
        {
            ChangableAssert.NotifiesIsChangedOnAcceptChanges<ChangableMessengerWithChildrenMock>();
        }
    }
}
