// <copyright file="ChangableObjectTests.cs" company="Jeremy Regnerus">
// Copyright (c) Jeremy Regnerus. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using JSR.BaseClassLibrary.Tests.Mocks;
using JSR.TestAsserts;
using JSR.Utilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace JSR.BaseClassLibrary.Tests
{
    /// <summary>
    /// Tests ChangableObjects.
    /// </summary>
    [TestClass]
    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1600:ElementsMustBeDocumented", Justification = "Unit Test.")]
    public class ChangableObjectTests
    {
        /// <summary>
        /// Tests that ChangableObjects initialize properly.
        /// </summary>
        [TestMethod]
        public void Initializes()
        {
            Assert.IsFalse(new MockChangableObjectWithChildren().IsChanged);
        }

        [TestMethod]
        public void SerializesAndDeserializes()
        {
            SerializationAssert.IChangeTrackingSerializationTests<MockChangableMessagingObjectWithChildren>();
        }

        [TestMethod]
        public void ChangesValues()
        {
            PropertyChangeAssert.ChangesValues<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesPropertyChanges()
        {
            PropertyNotificationAssert.NotifiesPropertiesChanged<MockChangableObjectWithChildren>();
        }

        [TestMethod]
        public void ChangesOnPropertiesChanged()
        {
            ChangeTrackingAssert.IsChangedWhenChanged<MockChangableMessagingObjectWithChildren>();
        }

        [TestMethod]
        public void NotifiesIsChanged()
        {
            ChangableObjectAssert.NotifiesIsChangedWhenChanged<MockChangableMessagingObjectWithChildren>();
        }

        [TestMethod]
        public void AcceptsChanges()
        {
            ChangeTrackingAssert.AcceptsChanges<MockChangableObjectWithChildren>();
        }
    }
}
