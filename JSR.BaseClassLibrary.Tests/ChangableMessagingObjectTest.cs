// <copyright file="ChangableMessagingObjectTest.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System.Diagnostics.CodeAnalysis;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableMessagingObjectTest
    {
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new MockChangableMessagingObject().IsChanged);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.IChangeTrackingSerializationTests<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void ChangesValues()
        {
            PropertyChangeAssert.ChangesValues<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyNotificationAssert.NotifiesPropertiesChanged<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableObjectAssert.NotifiesIsChangedWhenChanged<MockChangableMessagingObject>();
        }

        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<MockChangableMessagingObject>();
        }
    }
}
